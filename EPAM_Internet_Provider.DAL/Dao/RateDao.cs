using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM_Internet_Provider.DAL.Repositories;
using EPAM_Internet_Provider.Domain.Models;

namespace EPAM_Internet_Provider.DAL.Dao
{
    public interface IRateDao
    {
        Task<IEnumerable<Service>> GetAllServices();
        Task<ICollection<Rate>> GetRatesForService(int serviceId);
        Task<Service> GetService(int serviceId);
//        Task SubscribeUser(int userId, int serviceId, int rateId);
    }
//    public Task SubscribeUser(int userId, int serviceId, int rateId)
//    {
//
//    }

    public class RateDao: IRateDao
    {
        private readonly ProviderContext _context;

        public RateDao(ProviderContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Rate>> GetRatesForService(int serviceId)
        {
            var service=await _context.Services.Include(i => i.Rates).SingleOrDefaultAsync(i => i.ServiceId == serviceId);
            return service.Rates;
        }

        public Task<Service> GetService(int serviceId)
        {
            return _context.Services.Include(i=>i.Rates).SingleOrDefaultAsync(i => i.ServiceId == serviceId);
        }

        public Task<IEnumerable<Service>> GetAllServices()
        {
            return Task.FromResult(_context.Services.AsEnumerable());
        }

    }
}
