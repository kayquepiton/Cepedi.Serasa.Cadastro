﻿using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType
{
    public class UpdateTransactionTypeRequestHandler : IRequestHandler<UpdateTransactionTypeRequest, Result<UpdateTransactionTypeResponse>>
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly ILogger<UpdateTransactionTypeRequestHandler> _logger;

        public UpdateTransactionTypeRequestHandler(ITransactionTypeRepository transactionTypeRepository, ILogger<UpdateTransactionTypeRequestHandler> logger)
        {
            _transactionTypeRepository = transactionTypeRepository;
            _logger = logger;
        }

        public async Task<Result<UpdateTransactionTypeResponse>> Handle(UpdateTransactionTypeRequest request, CancellationToken cancellationToken)
        {
            var transactionTypeEntity = await _transactionTypeRepository.GetTransactionTypeAsync(request.Id);

            if (transactionTypeEntity is null)
            {
                return Result.Error<UpdateTransactionTypeResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalTransactionIdIdType));
            }

            transactionTypeEntity.TypeName = request.TypeName;

            await _transactionTypeRepository.UpdateTransactionTypeAsync(transactionTypeEntity);

            return Result.Success(new UpdateTransactionTypeResponse(transactionTypeEntity.Id, transactionTypeEntity.TypeName));
        }
    }
}
