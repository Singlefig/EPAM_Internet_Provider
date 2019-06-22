using System.Data.Entity;
using EPAM_Internet_Provider.Domain.Models;

namespace EPAM_Internet_Provider.DAL.Repositories
{
    public class ProviderContext : DbContext
    {
        static ProviderContext()
        {
//            Database.SetInitializer(new CreateDatabaseIfNotExists<ProviderContext>());
            Database.SetInitializer(new ProviderContextInitializer());
        }
        public ProviderContext() : base("ProviderDB")
        {

        }
        public ProviderContext(string ProviderContext) : base(ProviderContext)
        {
        }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Rate> Rates { get; set; }
        public IDbSet<Service> Services { get; set; }
        public IDbSet<Subscription> Subscriptions { get; set; }
    }
}
