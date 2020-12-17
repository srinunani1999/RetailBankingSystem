using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBankSystemClient.ViewModels
{
    public class TransactionHistoryViewModel
    {
       

        [Display(Name = "Transaction ID")]
        public int TransactionId { get; set; }
        [Display(Name = "Account No")]
        public int AccountId { get; set; }
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Display(Name = "Message")]
        public string message { get; set; }
        [Display(Name = "Source Balance")]
        public int source_balance { get; set; }
        [Display(Name = "Destination Balance")]
        public int destination_balance { get; set; }
        [Display(Name = "Transaction Date")]
        public DateTime DateOfTransaction { get; set; }
    }
}
