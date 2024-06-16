﻿using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType
{
    public class GetTransactionTypeRequestHandler : IRequestHandler<GetTransactionTypeRequest, Result<GetTransactionTypeResponse>>
    {
        private readonly ILogger<GetTransactionTypeRequestHandler> _logger;
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        public GetTransactionTypeRequestHandler(ILogger<GetTransactionTypeRequestHandler> logger, ITransactionTypeRepository transactionTypeRepository)
        {
            _logger = logger;
            _transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<Result<GetTransactionTypeResponse>> Handle(GetTransactionTypeRequest request, CancellationToken cancellationToken)
        {
            var transactionType = await _transactionTypeRepository.GetTransactionTypeAsync(request.Id);

            return transactionType is null
                ? Result.Error<GetTransactionTypeResponse>(new Shared.Exceptions.AppException(RegistrationErrors.InvalidTransactionTypeId))
                : Result.Success(new GetTransactionTypeResponse(transactionType.Id, transactionType.TypeName));
        }
    }
}
