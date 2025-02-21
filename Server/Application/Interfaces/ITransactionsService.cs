using Application.Entities;
using Application.Models;

namespace Application.Interfaces
{
    public interface ITransactionsService
    {
        Task<Response<Transaction>> ProcessTransactionAsync(TransactionRequest request);
        IAsyncEnumerable<Transaction> GetHistoryAsync(string userId);
    }
}
