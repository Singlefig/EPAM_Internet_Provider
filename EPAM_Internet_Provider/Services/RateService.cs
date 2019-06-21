﻿using System;
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

        public Task<IEnumerable<Service>> GetAllServices()
        {
            return _rateDao.GetAllServices();
        }

        public Task<Service> GetService(int serviceId)
        {
            return _rateDao.GetService(serviceId);
        }

        public Task<ICollection<Rate>> GetRatesForService(int serviceId)
        {
            return _rateDao.GetRatesForService(serviceId);
        }

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
                userSubscription = new Subscription {Service = await _rateDao.GetService(serviceId)};
                user.Subscributions.Add(userSubscription);
            }
//            var rates = await _rateDao.GetRatesForService(serviceId);
            userSubscription.SubscriptionRate = userSubscription.Service.Rates.FirstOrDefault(i => i.RateId == rateId);
            await _userDao.UpdateUser(user);
        }
    }
}