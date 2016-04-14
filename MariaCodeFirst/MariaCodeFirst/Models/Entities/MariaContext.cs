namespace MariaCodeFirst.Models.Entities
{
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;

	[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
	public partial class MariaContext : DbContext
	{
		public MariaContext() : base("name=MariaContext")
		{
			Database.SetInitializer<MariaContext>(new MariaDbInitializer());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}