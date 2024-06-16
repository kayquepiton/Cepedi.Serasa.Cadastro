using Cepedi.Serasa.Cadastro.Shared.Requests.TransactionType;
using Cepedi.Serasa.Cadastro.Shared.Responses.TransactionType;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Cepedi.Serasa.Cadastro.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.TransactionType
{
    public class CreateTransactionTypeRequestHandler : IRequestHandler<CreateTransactionTypeRequest, Result<CreateTransactionTypeResponse>>
    {
        private readonly ILogger<CreateTransactionTypeRequestHandler> _logger;
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        public CreateTransactionTypeRequestHandler(ILogger<CreateTransactionTypeRequestHandler> logger, ITransactionTypeRepository transactionTypeRepository)
        {
            _logger = logger;
            _transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<Result<CreateTransactionTypeResponse>> Handle(CreateTransactionTypeRequest request, CancellationToken cancellationToken)
        {
            var transactionType = new TransactionTypeEntity()
            {
                TypeName = request.TypeName,
            };

            await _transactionTypeRepository.CreateTransactionTypeAsync(transactionType);

            return Result.Success(new CreateTransactionTypeResponse(transactionType.Id, transactionType.TypeName));
        }
    }
}
