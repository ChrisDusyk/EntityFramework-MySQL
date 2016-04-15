namespace MariaCodeFirst.Migrations
{
	using System.Data.Common;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Data.Entity.Migrations.History;
	internal sealed class Configuration : DbMigrationsConfiguration<MariaCodeFirst.Models.Entities.MariaDBContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;

			SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());

			SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
		}

		protected override void Seed(MariaCodeFirst.Models.Entities.MariaDBContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//
		}
	}

	public class MySqlHistoryContext : HistoryContext
	{
		public MySqlHistoryContext(DbConnection connection, string defaultSchema) : base(connection, defaultSchema)
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<HistoryRow>().Property(h => h.MigrationId)
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<HistoryRow>().Property(h => h.ContextKey)
				.HasMaxLength(200)
				.IsRequired();
		}
	}
}
