﻿using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Pipelines
{
    public sealed class ExceptionPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<ExceptionPipeline<TRequest, TResponse>> _logger;

        public ExceptionPipeline(ILogger<ExceptionPipeline<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogInformation("Request handler {dados}", request);
                }

                var response = await next.Invoke();

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred during the request handling.");

                var error = Result.Error<TResponse>(e);

                var result = ModifyPropertyValueWithReflection<TResponse>(error.Value, e);
                return (TResponse)(object)result;
            }
        }

        private static object? ModifyPropertyValueWithReflection<T>(Result<T> error, Exception exception)
        {
            var valueProperty = typeof(Result<T>).GetProperty("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (valueProperty != null)
            {
                var value = valueProperty.GetValue(error);

                if (value != null)
                {
                    Type valueType = value.GetType();
                    var exceptionField = valueType.GetField("<Exception>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (exceptionField != null)
                    {
                        exceptionField.SetValue(value, exception);
                    }
                }

                return value;
            }

            return error;
        }
    }
}
