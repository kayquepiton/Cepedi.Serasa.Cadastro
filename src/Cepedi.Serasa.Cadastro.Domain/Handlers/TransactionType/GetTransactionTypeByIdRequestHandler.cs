﻿using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType
{
    public class GetTransactionTypeByIdRequestHandler : IRequestHandler<GetTransactionTypeByIdRequest, Result<GetTransactionTypeByIdResponse>>
    {
        private readonly ILogger<GetTransactionTypeByIdRequestHandler> _logger;
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        public GetTransactionTypeByIdRequestHandler(ILogger<GetTransactionTypeByIdRequestHandler> logger, ITransactionTypeRepository transactionTypeRepository)
        {
            _logger = logger;
            _transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<Result<GetTransactionTypeByIdResponse>> Handle(GetTransactionTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var transactionType = await _transactionTypeRepository.GetTransactionTypeAsync(request.Id);

            return transactionType is null
                ? Result.Error<GetTransactionTypeByIdResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalTransactionIdIdType))
                : Result.Success(new GetTransactionTypeByIdResponse(transactionType.Id, transactionType.TypeName));
        }
    }
}
