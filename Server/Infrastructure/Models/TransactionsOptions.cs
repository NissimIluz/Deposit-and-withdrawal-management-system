namespace Infrastructure.Models
{
    public class TransactionsOptions
    {
        public Uri CreatetokenEndPoint {  get; set; }
        public string CreatetokenSecretId {  get; set; }
        public Uri DepositUri { get; set; }
        public Uri WithdrawalUri { get; set; }
        public string SuccessCode { get; set; }
    }
}
