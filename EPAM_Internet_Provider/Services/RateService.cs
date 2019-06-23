using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EPAM_Internet_Provider.DAL.Dao;
using EPAM_Internet_Provider.Domain.Models;

namespace EPAM_Internet_Provider.Services
{
    public interface IRateService
    {
        Task<IEnumerable<Service>> GetAllServices();

        Task<Service> GetService(int serviceId);

        Task<ICollection<Rate>> GetRatesForService(int serviceId);

        Task SubscribeUser(int userUserId, int serviceId, int rateId);
        Task<Rate> EditRateById(int rateId, string rateName, decimal rateCost);
        Task<Subscription> FindSubscribeBySubId(int subId);
        Task<Subscription> ChargeSubscribe(int subscriptionId,decimal balance);
    }

    public class RateService : IRateService
    {
        private readonly IRateDao _rateDao;
        private readonly IUserDao _userDao;

        public  RateService(IRateDao rateDao,IUserDao userDao)
        {
            _rateDao = rateDao;
            _userDao = userDao;
        }
        /// <summary>
        /// Method to get list of services by rateDao class
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Service>> GetAllServices()
        {
            return _rateDao.GetAllServices();
        }
        /// <summary>
        /// Method to get service by rateDao class
        /// </summary>
        /// <returns></returns>
        public Task<Service> GetService(int serviceId)
        {
            return _rateDao.GetService(serviceId);
        }
        /// <summary>
        /// Method to get list of rates for current service by rateDao class
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<Rate>> GetRatesForService(int serviceId)
        {
            return _rateDao.GetRatesForService(serviceId);
        }
        /// <summary>
        /// Method to subscribe user by rateDao class
        /// </summary>
        /// <returns></returns>
        public async Task SubscribeUser(int userId, int serviceId, int rateId)
        {
            var user=await _userDao.FindUserById(userId);
            if (user.Subscributions == null || user.Subscributions?.Count == 0)
            {
                user.Subscributions=new List<Subscription>();
            }
            var userSubscription = user.Subscributions.FirstOrDefault(i => i.Service.ServiceId == serviceId);
            if (userSubscription == null)
            {
                userSubscription = new Subscription {Service = await _rateDao.GetService(serviceId),IsBlocked = true};
                user.Subscributions.Add(userSubscription);
            }
            userSubscription.SubscriptionRate = userSubscription.Service.Rates.FirstOrDefault(i => i.RateId == rateId);
            await _userDao.UpdateUser(user);
        }
        /// <summary>
        /// Method to edit rate by id by rateDao class
        /// </summary>
        /// <returns></returns>
        public Task<Rate> EditRateById(int rateId, string rateName, decimal rateCost)
        {
            return _rateDao.EditRateById(rateId,rateName,rateCost);
        }
        /// <summary>
        /// Method to find subscribe by subId by rateDao class
        /// </summary>
        /// <returns></returns>
        public Task<Subscription> FindSubscribeBySubId(int subId)
        {
            return _rateDao.FindSubscribeByUserId(subId);
        }
        /// <summary>
        /// Method to charge subscribe by rateDao class
        /// </summary>
        /// <returns></returns>
        public Task<Subscription> ChargeSubscribe(int subscriptionId,decimal balance)
        {
            return _rateDao.ChargeSubscribe(subscriptionId,balance);
        }
    }
}