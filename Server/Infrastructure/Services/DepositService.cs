using Application.Database;
using Infrastructure.Abstractions;
using Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class DepositService : BaseHttpTransactionsService
    {
        public DepositService(IBankingDbContext context, HttpClient httpClient, IOptionsSnapshot<TransactionsOptions> optionsSnapshot)
            : base(context, httpClient, optionsSnapshot) { }

        protected override Uri TransactionEndPoint { get => TransactionsOptions.WithdrawalUri;  }
    }
}
