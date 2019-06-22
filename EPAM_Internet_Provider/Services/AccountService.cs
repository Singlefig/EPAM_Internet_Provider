using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using EPAM_Internet_Provider.DAL.Dao;
using EPAM_Internet_Provider.Models;
using User = EPAM_Internet_Provider.Domain.Models.User;

namespace EPAM_Internet_Provider.Services
{
    public interface IAccountService
    {
        Task<User> AddUser(User user);
        Task<User> FindUserByEmail(string  email);
        Task<User> FindUserById(int userId);
        Task<bool> IsEmailExist(string email);
        Task<IEnumerable<User>> ViewUsersList();
        Task UnsubscribeUser(int subId);
    }

    public class AccountService : IAccountService
    {
        private readonly IUserDao _userDao;

        public AccountService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public Task<User> AddUser(User user)
        {
            return _userDao.AddUser(user);
        }

        public Task<User> FindUserByEmail(string email)
        {
            return _userDao.FindUserByEmail(email);
        }

        public Task<User> FindUserById(int userId)
        {
            return _userDao.FindUserById(userId);
        }

        public Task<bool> IsEmailExist(string email)
        {
            return _userDao.IsEmailExist(email);
        }

        public Task UnsubscribeUser(int subId)
        {
            return _userDao.UnsubscribeUser(subId);
        }

        public async Task<IEnumerable<User>> ViewUsersList()
        {
            return await _userDao.ViewUsersList();
        }
    }
}
