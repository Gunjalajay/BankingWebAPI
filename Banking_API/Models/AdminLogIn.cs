using System.ComponentModel.DataAnnotations;

namespace Banking_API.Models
{
    public class AdminLogIn
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminName { get; set; }

        public string AdminPassword { get; set; }
    }
}
