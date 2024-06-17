﻿using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType
{
    public class DeleteTransactionTypeByIdRequestHandler : IRequestHandler<DeleteTransactionTypeByIdRequest, Result<DeleteTransactionTypeByIdResponse>>
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly ILogger<DeleteTransactionTypeByIdRequestHandler> _logger;

        public DeleteTransactionTypeByIdRequestHandler(ILogger<DeleteTransactionTypeByIdRequestHandler> logger, ITransactionTypeRepository transactionTypeRepository)
        {
            _logger = logger;
            _transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<Result<DeleteTransactionTypeByIdResponse>> Handle(DeleteTransactionTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var transactionTypeEntity = await _transactionTypeRepository.GetTransactionTypeAsync(request.Id);

            if (transactionTypeEntity is null)
            {
                return Result.Error<DeleteTransactionTypeByIdResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalTransactionIdIdType));
            }

            await _transactionTypeRepository.DeleteTransactionTypeAsync(transactionTypeEntity.Id);

            return Result.Success(new DeleteTransactionTypeByIdResponse(transactionTypeEntity.Id, transactionTypeEntity.TypeName));
        }
    }
}
