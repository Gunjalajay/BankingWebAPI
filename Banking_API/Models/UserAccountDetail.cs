using System;
using System.ComponentModel.DataAnnotations;

namespace Banking_API.Models
{
    public class UserAccountDetail
    {
        [Key]
        public  int? AccountId { get; set; }
        public int? CustomerId { get; set; }

        public decimal? Balance { get; set; }

        public DateTime? AccOpDate { get; set; }

        public bool? AccountStatus { get; set; }
    }
}
