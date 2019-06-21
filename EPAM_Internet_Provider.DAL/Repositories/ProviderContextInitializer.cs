using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPAM_Internet_Provider.Domain;
using EPAM_Internet_Provider.Domain.Models;

namespace EPAM_Internet_Provider.DAL.Repositories
{
    public class ProviderContextInitializer : CreateDatabaseIfNotExists<ProviderContext>
    {
        public ProviderContextInitializer()
        {

        }

        private void SeedUsers(ProviderContext context)
        {
            var users = new List<User>()
            {
                new User
                {
                    Email = "kruchenkovoleh80@gmail.com",
                    Password = "1234rewq",
                    Role = "Admin",
                    Name="Admin"
                },
                new User
                {
                    Email = "kruchenkova@gmail.com",
                    Password = "1234rewqasdf",
                    Role = "Manager",
                    Name="MAnager"
                },
                new User
                {
                    Email = "fesenko@gmail.com",
                    Password = "1234rewqasdf",
                    Role = "Client",
                    Name="Client"
                }
            };
            users.Select(i =>
            {
                i.Password = Crypto.Hash(i.Password);
                return i;
            })
            .ToList()
            .ForEach(i=> context.Users.Add(i));
            context.SaveChanges();
        }

        protected override void Seed(ProviderContext context)
        {
            SeedUsers(context);

            var IPTV_Rates = new List<Rate>()
            {
                new Rate
                {
                    RateName = "Home",
                    RateCost = 19.99m
                },
                new Rate
                {
                    RateName = "Office",
                    RateCost = 29.99m
                },
                new Rate
                {
                    RateName = "All Include",
                    RateCost = 49.99m
                },
            };

            var Internet_Rates = new List<Rate>()
            {
                new Rate
                {
                    RateName = "Home",
                    RateCost = 19.99m
                },
                new Rate
                {
                    RateName = "Office",
                    RateCost = 29.99m
                },
                new Rate
                {
                    RateName = "All Include",
                    RateCost = 49.99m
                },
            };

            var CabelTV_Rates = new List<Rate>()
            {
                new Rate
                {
                    RateName = "Home",
                    RateCost = 19.99m
                },
                new Rate
                {
                    RateName = "Office",
                    RateCost = 29.99m
                },
                new Rate
                {
                    RateName = "All Include",
                    RateCost = 49.99m
                },
            };
            var Phone_Rates = new List<Rate>()
            {
                new Rate
                {
                    RateName = "Home",
                    RateCost = 19.99m
                },
                new Rate
                {
                    RateName = "Office",
                    RateCost = 29.99m
                },
                new Rate
                {
                    RateName = "All Include",
                    RateCost = 49.99m
                },
            };
            var Services = new List<Service>()
            {
                new Service
                {
                    ServiceName = "IP-TV",
                    ServiceType = ServicesType.IPTV,
                    Rates = IPTV_Rates,
                },
                new Service
                {
                    ServiceName = "Internet",
                    ServiceType = ServicesType.Internet,
                    Rates = Internet_Rates,
                },
                new Service
                {
                    ServiceName = "Cabel TV",
                    ServiceType = ServicesType.CabelTV,
                    Rates = CabelTV_Rates,
                },
                new Service
                {
                    ServiceName = "Mobile Phone",
                    ServiceType = ServicesType.MobilePhone,
                    Rates = Phone_Rates,
                }
            };

            foreach (var service in Services)
            {
                context.Services.Add(service);
            }
            context.SaveChanges();
        }
    }
}