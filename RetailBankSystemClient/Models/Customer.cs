using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailBankSystemClient.Models
{
	public class Customer
	{
		[Key]
		[Display(Name = "Customer Id")]
		public int CustomerId { get; set; }

		[Display(Name = "Customer Name"), Required]
		public string Name { get; set; }

		[Display(Name = "Address"), Required]
		public string Address { get; set; }

		[Display(Name = "Date of Birth"), Required]
		public DateTime DateOfBirth { get; set; }

		[Display(Name = "PAN Number"), Required]
		public string PANno { get; set; }
	}
}
