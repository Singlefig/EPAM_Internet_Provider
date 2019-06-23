using System.Data.Entity;
using EPAM_Internet_Provider.Domain.Models;

namespace EPAM_Internet_Provider.DAL.Repositories
{
    public class ProviderContext : DbContext
    {
        static ProviderContext()
        {
            Database.SetInitializer(new ProviderContextInitializer());
        }
        public ProviderContext() : base("ProviderDB")
        {

        }
        public ProviderContext(string ProviderContext) : base(ProviderContext)
        {
        }
        /// <summary>
        /// Users - table of users in Database
        /// Rates - table of rates in Database
        /// Servces - table of services in Database
        /// Subscribes - table of subscribes in Database
        /// </summary>
        public IDbSet<User> Users { get; set; }
        public IDbSet<Rate> Rates { get; set; }
        public IDbSet<Service> Services { get; set; }
        public IDbSet<Subscription> Subscriptions { get; set; }
    }
}
