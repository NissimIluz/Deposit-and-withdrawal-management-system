using Application.Entities;
using Application.Models;
using Infrastructure.Abstractions;
using Infrastructure.CONSTANTS;

namespace Infrastructure.Services
{
    public class ProcessTransactionFactory
    {

        private readonly IServiceProvider _serviceProvider;

        public ProcessTransactionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        private Dictionary<string, Type> _dictionaryServices = new(StringComparer.OrdinalIgnoreCase)
        {
            { TRANSACTION_TYPES.DEPOSIT, typeof(DepositService) },
            { TRANSACTION_TYPES.WITHDRAWAL, typeof(WithdrawalService)  }
        };


        public async Task<Response<Transaction>> ProcessTransactionAsync(TransactionRequest request)
        {
            // Validate request type exists
            if (!_dictionaryServices.TryGetValue(request.TransactionType, out var serviceType))
            {
                throw new ArgumentException($"Invalid transaction type: {request.TransactionType}");
            }

            // Get the service instance from the DI container
            var service = _serviceProvider.GetService(serviceType) as BaseTransactionsService;

            if (service == null)
            {
                throw new InvalidOperationException($"Service not found for type {serviceType.Name}");
            }

            // Process transaction
            return await service.ProcessTransactionAsync(request);
        }
    }
}
