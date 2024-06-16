using Cepedi.Serasa.Cadastro.Shared.Enums;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<ActionResult> SendCommand(IRequest<Result> request, int statusCode = 200)
            => await _mediator.Send(request) switch
            {
                (true, _) => StatusCode(statusCode),
                (_, var error) => HandleError(error!)
            };

        protected async Task<ActionResult<T>> SendCommand<T>(IRequest<Result<T>> request, int statusCode = 200)
            => await _mediator.Send(request).ConfigureAwait(false) switch
            {
                (true, var result, _) => StatusCode(statusCode, result),
                (_, _, var error) => HandleError(error!)
            };

        protected ActionResult HandleError(Exception error) => error switch
        {
            NoResultException e => NoContent(),
            InvalidRequestException e => BadRequest(FormatErrorMessage(e.ErrorResult, e.Errors)),
            AppException e => BadRequest(FormatErrorMessage(e.ErrorResult)),
            _ => BadRequest(FormatErrorMessage(RegistrationErrors.Generic))
        };

        private ErrorResult FormatErrorMessage(ErrorResult errorResult, IEnumerable<string>? errors = null)
        {
            if (errors != null)
            {
                errorResult.Description = $"{errorResult.Description}: {string.Join("; ", errors!)}";
            }

            return errorResult;
        }
    }
}
