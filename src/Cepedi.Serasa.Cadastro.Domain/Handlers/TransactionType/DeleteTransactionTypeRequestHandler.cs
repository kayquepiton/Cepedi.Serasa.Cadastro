﻿using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType
{
    public class DeleteTransactionTypeRequestHandler : IRequestHandler<DeleteTransactionTypeRequest, Result<DeleteTransactionTypeResponse>>
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly ILogger<DeleteTransactionTypeRequestHandler> _logger;

        public DeleteTransactionTypeRequestHandler(ILogger<DeleteTransactionTypeRequestHandler> logger, ITransactionTypeRepository transactionTypeRepository)
        {
            _logger = logger;
            _transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<Result<DeleteTransactionTypeResponse>> Handle(DeleteTransactionTypeRequest request, CancellationToken cancellationToken)
        {
            var transactionTypeEntity = await _transactionTypeRepository.GetTransactionTypeAsync(request.Id);

            if (transactionTypeEntity == null)
            {
                return Result.Error<DeleteTransactionTypeResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalidTransactionTypeId));
            }

            await _transactionTypeRepository.DeleteTransactionTypeAsync(transactionTypeEntity.Id);

            return Result.Success(new DeleteTransactionTypeResponse(transactionTypeEntity.Id, transactionTypeEntity.TypeName));
        }
    }
}
