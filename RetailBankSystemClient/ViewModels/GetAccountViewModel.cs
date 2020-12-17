using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBankSystemClient.ViewModels
{
    public class GetAccountViewModel
    {
        [Key]
        [Display(Name ="Account Id"),Required]
        public int AccountId { get; set; }
        [Display(Name = "Customer Id"), Required]

        public int CustomerId { get; set; }
        [Display(Name = "Account Type"), Required]

        public string AccountType { get; set; }
        [Display(Name = "Balance"), Required]

        public int Balance { get; set; }
        [Display(Name = "Min Balance"), Required]

        public int minBalance { get; set; }

    }
}
