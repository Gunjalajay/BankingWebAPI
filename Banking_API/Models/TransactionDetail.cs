using System;
using System.ComponentModel.DataAnnotations;

namespace Banking_API.Models
{
    public class TransactionDetail
    {
        [Key]
        public  int TransactionId { get; set; }
        public int AccountId { get; set; }
        public int BeneficiaryAccount { get; set; }

        public string TransactionMode { get; set; }

        public string TransactionType { get; set; }

        public decimal Amount { get; set; }

        public DateTime? TransactionDate { get; set; }
        public string Remarks { get; set; }
    }
}
