using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MariaCodeFirst.Models.Entities
{
	[Table("Products")]
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

		public DateTime LastModifiedDate { get; set; }
	}
}
}