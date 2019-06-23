using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using EPAM_Internet_Provider.DAL.Dao;
using EPAM_Internet_Provider.Models;
using User = EPAM_Internet_Provider.Domain.Models.User;

namespace EPAM_Internet_Provider.Services
{
    /// <summary>
    /// Interface for bind AccountService in NinjectDependedncyResolver
    /// </summary>
    public interface IAccountService
    {
        Task<User> AddUser(User user);
        Task<User> FindUserByEmail(string  email);
        Task<User> FindUserById(int userId);
        Task<bool> IsEmailExist(string email);
        Task<IEnumerable<User>> ViewUsersList();
        Task UnsubscribeUser(int subId);
        Task BlockUserByAdminSkill(int userId);
        Task UnblockUserByAdminSkill(int userId);
    }

    public class AccountService : IAccountService
    {
        private readonly IUserDao _userDao;

        public AccountService(IUserDao userDao)
        {
            _userDao = userDao;
        }
        /// <summary>
        /// Method for adding user using userDao class
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<User> AddUser(User user)
        {
            return _userDao.AddUser(user);
        }
        /// <summary>
        /// Method for blocking user using userDao class by admin skill
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task BlockUserByAdminSkill(int userId)
        {
            return _userDao.BlockUserByAdminSkill(userId);
        }
        /// <summary>
        /// Method for finding user by email using userDao class
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<User> FindUserByEmail(string email)
        {
            return _userDao.FindUserByEmail(email);
        }
        /// <summary>
        /// Method for finding user by id using userDao class
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<User> FindUserById(int userId)
        {
            return _userDao.FindUserById(userId);
        }
        /// <summary>
        /// Method for checking is email exist in Database using userDao class
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> IsEmailExist(string email)
        {
            return _userDao.IsEmailExist(email);
        }
        /// <summary>
        /// Method for unblocking user using userDao class by admin skill
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task UnblockUserByAdminSkill(int userId)
        {
            return _userDao.UnblockUserByAdminSkill(userId);
        }
        /// <summary>
        /// Method for unsubscribe user using userDao class
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task UnsubscribeUser(int subId)
        {
            return _userDao.UnsubscribeUser(subId);
        }
        /// <summary>
        /// Method for get List of users user using userDao class
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> ViewUsersList()
        {
            return await _userDao.ViewUsersList();
        }
    }
}
