using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MariaCodeFirst.Models.Entities
{
	[Table("TestDB.Products")]
	public class Product
	{
		public int ProductId { get; set; }

		[Required]
		[StringLength(5)]
		public string ProductCode { get; set; }

		[Required]
		[StringLength(150)]
		public string Description { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}