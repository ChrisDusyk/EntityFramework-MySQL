using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MariaCodeFirst.Models.Entities
{
	[Table("OrderProducts")]
	public class OrderProduct
	{
		public int OrderProductId { get; set; }

		public int OrderId { get; set; }
		public virtual Order Order { get; set; }

		public int ProductId { get; set; }
		public virtual Product Product { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}