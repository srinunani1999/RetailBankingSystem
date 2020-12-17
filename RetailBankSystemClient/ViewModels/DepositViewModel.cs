using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBankSystemClient.ViewModels
{
    public class DepositViewModel
    {
        [Key]
        public int AccountId { get; set; }
        [Display(Name = "Amount"), Required]
        public int amount { get; set; }
    }
}
