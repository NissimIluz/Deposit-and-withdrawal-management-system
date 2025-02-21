using Application.Database;
using Application.Models;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;


namespace Infrastructure.Abstractions
{
    public abstract class BaseHttpTransactionsService : BaseTransactionsService
    {
        protected abstract Uri TransactionEndPoint { get; }
        public BaseHttpTransactionsService(IBankingDbContext context, HttpClient httpClient, IOptionsSnapshot<TransactionsOptions> optionsSnapshot)
         : base(context, httpClient, optionsSnapshot) { }

        internal async Task<TransactionResponse> DoHttpTransactionAsync(TransactionRequest request, Uri uri)
        {
            var body = new { amount = request.Amount, bank = request.BankAccount };
            var transactionResponse = await HttpClient.PostAsJsonAsync(uri, body);
            return await transactionResponse.Content.ReadFromJsonAsync<TransactionResponse>();
        }

        internal override async Task<TransactionResponse> DoTransactionAsync(TransactionRequest request)
        {
            return new TransactionResponse() { Code = TransactionsOptions.SuccessCode, Data = "mock up" };
            return await DoHttpTransactionAsync(request, TransactionEndPoint);
        }
    }
}
