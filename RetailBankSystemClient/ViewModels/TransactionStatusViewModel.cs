using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBankSystemClient.ViewModels
{
	public class TransactionStatusViewModel
	{
		
		[Display(Name = "Message")]
		public string message { get; set; }
		[Display(Name = "Source Balnace")]

		public int source_balance { get; set; }
		[Display(Name = "Destination Balance")]

		public int destination_balance { get; set; }
	}
}
