using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBankSystemClient.ViewModels
{
    public class AccountViewModel
    {
        [Key]
        public int Id { get; set; }
        public double Balance { get; set; }
    }
}
