﻿namespace API.Models
{
    public class TransactionDTO
    {
        public TransactionDTO(Application.Entities.Transaction transaction)
        {
            UserId = transaction.UserId;
            FullNameHebrew = transaction.FullNameHebrew;
            FullNameEnglish = transaction.FullNameEnglish;
            TransactionType = transaction.Type;
            Amount = transaction.Amount;
            BankAccount = transaction.BankAccount;
            Status = transaction.Status;
            Date = transaction.Date;
        }

        public string UserId { get; set; }
        public string FullNameHebrew { get; set; }
        public string FullNameEnglish { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string BankAccount { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
