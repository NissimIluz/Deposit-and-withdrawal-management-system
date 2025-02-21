namespace Application.Entities
{
     public class Transaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string BankAccount { get; set; }
        public string Status { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
