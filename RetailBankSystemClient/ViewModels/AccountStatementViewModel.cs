using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBankSystemClient.ViewModels
{
    public class AccountStatementViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "From Date")]
        public DateTime from_date { get; set; }
        [Display(Name = "To Date")]
        public DateTime to_date { get; set; }
    }
}
