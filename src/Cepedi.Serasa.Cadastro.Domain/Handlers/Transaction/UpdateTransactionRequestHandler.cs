using Cepedi.Serasa.Cadastro.Shared.Requests.Transaction;
using Cepedi.Serasa.Cadastro.Shared.Responses.Transaction;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Serasa.Cadastro.Shared.Exceptions;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Transaction
{
    public class UpdateTransactionRequestHandler : IRequestHandler<UpdateTransactionRequest, Result<UpdateTransactionResponse>>
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<UpdateTransactionRequestHandler> _logger;

        public UpdateTransactionRequestHandler(ITransactionTypeRepository transactionTypeRepository, ITransactionRepository transactionRepository, ILogger<UpdateTransactionRequestHandler> logger)
        {
            _transactionTypeRepository = transactionTypeRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        public async Task<Result<UpdateTransactionResponse>> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
        {
            var transactionType = await _transactionTypeRepository.GetTransactionTypeAsync(request.IdTransactionType);
            
            if (transactionType == null)
            {
                return Result.Error<UpdateTransactionResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalidTransactionTypeId));
            }

            var transactionEntity = await _transactionRepository.GetTransactionAsync(request.Id);
            
            if (transactionEntity == null)
            {
                return Result.Error<UpdateTransactionResponse>(
                    new Shared.Exceptions.AppException(RegistrationErrors.InvalidTransactionId));
            }

            transactionEntity.IdTransactionType = request.IdTransactionType;
            transactionEntity.DateTime = request.DateTime;
            transactionEntity.EstablishmentName = request.EstablishmentName;
            transactionEntity.Value = request.Value;

            await _transactionRepository.UpdateTransactionAsync(transactionEntity);

            var response = new UpdateTransactionResponse(
                transactionEntity.Id,
                transactionEntity.IdTransactionType,
                transactionEntity.IdPerson,
                transactionEntity.DateTime,
                transactionEntity.EstablishmentName,
                transactionEntity.Value
            );

            return Result.Success(response);
        }
    }
}
