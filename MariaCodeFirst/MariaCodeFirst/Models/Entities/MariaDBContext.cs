namespace MariaCodeFirst.Models.Entities
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class MariaDBContext : DbContext
	{
		public MariaDBContext()
			: base("name=MariaDBContext")
		{
		}

		public virtual DbSet<Customer> Customers { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customer>()
				.Property(e => e.CustomerName)
				.IsUnicode(false);
		}
	}
}
