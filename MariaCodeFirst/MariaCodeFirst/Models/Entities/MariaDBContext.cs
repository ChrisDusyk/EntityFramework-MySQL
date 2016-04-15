namespace MariaCodeFirst.Models.Entities
{
	using System.Data.Entity;

	[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
	public partial class MariaDBContext : DbContext
	{
		public MariaDBContext() : base("name=MariaDBContext")
		{
			Database.SetInitializer<MariaDBContext>(new MariaDbInitializer());
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
