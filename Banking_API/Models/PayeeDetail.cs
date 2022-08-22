using System.ComponentModel.DataAnnotations;

namespace Banking_API.Models
{
    public class PayeeDetail
    {
        [Key]
        public int PayeeId { get; set; }

        public int BeneficiaryAccount { get; set; }
        public string BeneficiaryName { get; set; }

        public string BeneficiaryBank { get; set; }
        public string BeneficiaryBranch { get; set; }
        public string BeneficiaryCity { get; set; }

        public string BeneficiaryIFSC { get; set; }

        public int AccountId { get; set; }
    }
}
