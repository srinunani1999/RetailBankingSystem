using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBankSystemClient.ViewModels
{
	public class TransferViewModel
	{
		
		[Display(Name = "Target Account"), Required]
		public int Target_AccountId { get; set; }
		[Display(Name = "Source Account"), Required]
		public int Source_AccountId { get; set; }
		[Display(Name = "Amount"), Required]
		public int amount { get; set; }
	}
}
