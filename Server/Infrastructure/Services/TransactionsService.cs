using Application.Database;
using Application.Entities;
using Application.Interfaces;
using Application.Models;
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

        public Task<Response<Transaction>> ProcessTransactionAsync(TransactionRequest request)
        {
            return _processTransactionFactory.ProcessTransactionAsync(request);
        }

        public IAsyncEnumerable<Transaction> GetHistoryAsync(string userId)
        {
            return _context.Transactions
                .Where(transaction => transaction.UserId == userId)
                .OrderByDescending(transaction => transaction.Date)
                .AsAsyncEnumerable();
        }
    }
}
