using System.Data.Entity;

namespace MariaCodeFirst.Models.Entities
{
	public class MariaDbInitializer : CreateDatabaseIfNotExists<MariaContext>
	{
		protected override void Seed(MariaContext context)
		{
			base.Seed(context);
		}
	}
}