using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MariaCodeFirst.Models.Entities
{
	[Table("Orders")]
	public class Order
	{
		public int OrderId { get; set; }

		[Required]
		public int CustomerId { get; set; }

		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; }

		public DateTime OrderDate { get; set; }
	}
}