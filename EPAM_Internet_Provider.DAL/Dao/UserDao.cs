using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using EPAM_Internet_Provider.DAL.Repositories;
using EPAM_Internet_Provider.Domain.Models;

namespace EPAM_Internet_Provider.DAL.Dao
{
    public interface IUserDao
    {
        Task<User> FindUserByEmail(string email);
        Task<bool> IsEmailExist(string email);
        Task<User> AddUser(User user);
        Task<User> FindUserById(int userId);
        Task UpdateUser(User user);
        Task<IEnumerable<User>> ViewUsersList();
        Task UnsubscribeUser(int subId);
        Task BlockUserByAdminSkill(int userId);
        Task UnblockUserByAdminSkill(int userId);
    }

    public class UserDao : IUserDao
    {
        private readonly ProviderContext _context;

        public UserDao(ProviderContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method to get user by email from Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<User> FindUserByEmail(string email)
        {
            return _context.Users.SingleOrDefaultAsync(a => a.Email == email);
        }
        /// <summary>
        /// Method to get user by id from Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<User> FindUserById(int userId)
        {
            return _context.Users.Where(a => a.UserId == userId)
                .Include(i => i.Subscributions)
                .Include(i=>i.Subscributions.Select(y=>y.SubscriptionRate))
                .Include(i=>i.Subscributions.Select(y=>y.Service))
                .SingleAsync();
        }
        /// <summary>
        /// Method to update user in Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task UpdateUser(User user)
        {
            _context.Users.AddOrUpdate(user);
            return _context.SaveChangesAsync();
        }
        /// <summary>
        /// Method to check is email exist in Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<bool> IsEmailExist(string email)
        {
            return _context.Users.AnyAsync(a => a.Email == email);
        }
        /// <summary>
        /// Method to add user by in Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> AddUser(User user)
        {
            var newuser = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return newuser;
        }
        /// <summary>
        /// Method to get users list from Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> ViewUsersList()
        {
            return await Task.FromResult(_context.Users.AsEnumerable());
        }
        /// <summary>
        /// Method to unsubscribe user by subId from Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task UnsubscribeUser(int subId)
        {
            var result = _context.Subscriptions.Find(subId);
            _context.Subscriptions.Remove(result);
            _context.SaveChanges();
            return Task.FromResult(result);
        }
        /// <summary>
        /// Method to block user by admin skill from Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task BlockUserByAdminSkill(int userId)
        {
            var user = _context.Users.Find(userId);
            foreach (var sub in user.Subscributions)
            {
                sub.IsBlocked = true;
            }
            _context.SaveChanges();
            return Task.FromResult(user);
        }
        /// <summary>
        /// Method to unblock user by admin skill from Database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task UnblockUserByAdminSkill(int userId)
        {
            var user = _context.Users.Find(userId);
            foreach (var sub in user.Subscributions)
            {
                sub.IsBlocked = false;
            }
            _context.SaveChanges();
            return Task.FromResult(user);
        }
    }
}
