using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MariaCodeFirst.Models.Entities
{
	[Table("Orders")]
	public class Order
	{
		public int OrderId { get; set; }

		[Required]
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}