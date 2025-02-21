using Application.Database;
using Application.Entities;
using Application.Interfaces;
using Application.Models;
using Infrastructure.CONSTANTS;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class TransactionsService : ITransactionsService
    {

        private readonly ProcessTransactionFactory _processTransactionFactory;
        private readonly IBankingDbContext _context;

        public TransactionsService(ProcessTransactionFactory processTransactionFactory, IBankingDbContext bankingDbContext)
        {
            _processTransactionFactory = processTransactionFactory;
            _context = bankingDbContext;
        }


        private Dictionary<string, Type> _dictionaryServices = new()
        {
            { TRANSACTION_TYPES.DEPOSIT, typeof(DepositService) },
            { TRANSACTION_TYPES.WITHDRAWAL, typeof(WithdrawalService)  }
        };


        public Task<Response<Transaction>> ProcessTransactionAsync(TransactionRequest request)
        {
            return _processTransactionFactory.ProcessTransactionAsync(request);
          


        }

        public IAsyncEnumerable<Transaction> GetHistoryAsync(string userId)
        {
            return _context.Transactions
                .Where(transaction => transaction.UserId == userId)
                .AsAsyncEnumerable();
        }


    }
}
