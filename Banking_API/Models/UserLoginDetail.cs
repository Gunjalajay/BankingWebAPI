using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_API.Models
{
    public class UserLoginDetail
    {
        [Key]

        public int? LogId { get; set; }
        public string LogInPassword { get; set; }
        
        public string TransactionPassword { get; set; }
        public int? AccountId { get; set; }
    }
}
