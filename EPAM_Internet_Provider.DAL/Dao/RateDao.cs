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
        Task<Rate> EditRateById(int rateId,string rateName,decimal rateCost);
        Task<Subscription> FindSubscribeByUserId(int subId);
        Task<Subscription> ChargeSubscribe(int subscriptionId,decimal balance);
    }

    public class RateDao: IRateDao
    {
        private readonly ProviderContext _context;

        public RateDao(ProviderContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method to get rates from Database
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public async Task<ICollection<Rate>> GetRatesForService(int serviceId)
        {
            var service=await _context.Services.Include(i => i.Rates).SingleOrDefaultAsync(i => i.ServiceId == serviceId);
            return service.Rates;
        }
        /// <summary>
        /// Method to get service from Database
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public Task<Service> GetService(int serviceId)
        {
            return _context.Services.Include(i=>i.Rates).SingleOrDefaultAsync(i => i.ServiceId == serviceId);
        }
        /// <summary>
        /// Method to get services from Database
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public Task<IEnumerable<Service>> GetAllServices()
        {
            return Task.FromResult(_context.Services.AsEnumerable());
        }
        /// <summary>
        /// Method to edit rate by id from Database
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public Task<Rate> EditRateById(int rateId, string rateName, decimal rateCost)
        {
            var rate = _context.Rates.Find(rateId);
            rate.RateName = rateName;
            rate.RateCost = rateCost;
            _context.SaveChanges();
            return Task.FromResult(rate);
        }
        /// <summary>
        /// Method to find subscribe by subId from Database
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public Task<Subscription> FindSubscribeByUserId(int subId)
        {
            return Task.FromResult(_context.Subscriptions.Find(subId));
        }
        /// <summary>
        /// Method to charge subscribe from Database
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public async Task<Subscription> ChargeSubscribe(int subscriptionId,decimal balance)
        {
            var result = await FindSubscribeByUserId(subscriptionId);
            result.ServiceBalance += balance;
            result.ServiceBalance -= result.SubscriptionRate.RateCost;
            if(result.ServiceBalance >= 0)
            {
                result.IsBlocked = false;
            }
            else
            {
                result.IsBlocked = true;
            }
            _context.SaveChanges();
            return await Task.FromResult(result);
        }
    }
}
