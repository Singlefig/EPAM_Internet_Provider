using System.Data.Entity.Migrations;
using EPAM_Internet_Provider.DAL.Repositories;

namespace EPAM_Internet_Provider.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ProviderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProviderContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
