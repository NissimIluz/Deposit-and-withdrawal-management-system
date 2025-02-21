using Application.Database;
using Application.Entities;
using Application.Enums;
using Application.Models;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.Abstractions
{
    public abstract class BaseTransactionsService
    {

        protected readonly HttpClient HttpClient;
        protected readonly TransactionsOptions TransactionsOptions;

        private readonly IBankingDbContext _context;

        public BaseTransactionsService(
            IBankingDbContext context,
            HttpClient httpClient,
            IOptionsSnapshot<TransactionsOptions> optionsSnapshot)
        {
            _context = context;
            HttpClient = httpClient;
            TransactionsOptions = optionsSnapshot.Value;
        }
        public async Task<Response<Transaction>> ProcessTransactionAsync(TransactionRequest request)
        {
            var tokenResult = await CreateToken(request.UserId);

            if (tokenResult?.Code != TransactionsOptions.SuccessCode)
                return new(eCodes.Unauthorized);



            var transactionResponse = await DoTransactionAsync(request);

            if (transactionResponse?.Code != TransactionsOptions.SuccessCode)
                return new(eCodes.TransactionFailed);


            var transaction = await AddTransactionAsync(request, transactionResponse.Data);
            return new(transaction);
        }

        internal abstract Task<TransactionResponse> DoTransactionAsync(TransactionRequest request);


        internal virtual async Task<TokenResponse> CreateToken(string userId)
        {
            return new TokenResponse() { Code = TransactionsOptions.SuccessCode, Data = "Data" };
            var tokenResponse = await HttpClient.PostAsJsonAsync(
                TransactionsOptions.CreatetokenEndPoint,
                new
            {
                userId = userId,
                SecretId = TransactionsOptions.CreatetokenSecretId
            });
            var tokenResult = await tokenResponse.Content.ReadFromJsonAsync<TokenResponse>();
            return tokenResult;
        }

        protected async Task<Transaction> AddTransactionAsync(TransactionRequest request,string data)
        {
            var transaction = new Transaction()
            {
                UserId = request.UserId,
                FullNameHebrew = request.FullNameHebrew,
                FullNameEnglish = request.FullNameEnglish,
                Type = request.TransactionType,
                Amount = request.Amount,
                BankAccount = request.BankAccount,
                Status = data,
                BirthDate = request.BirthDate,
                Date = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return transaction;
        }
    }
}

