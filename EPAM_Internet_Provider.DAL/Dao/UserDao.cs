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
    }

    public class UserDao : IUserDao
    {
        private readonly ProviderContext _context;

        public UserDao(ProviderContext context)
        {
            _context = context;
        }

        public Task<User> FindUserByEmail(string email)
        {
            return _context.Users.SingleOrDefaultAsync(a => a.Email == email);
        }

        public Task<User> FindUserById(int userId)
        {
            return _context.Users.Where(a => a.UserId == userId)
                .Include(i => i.Subscributions)
                .Include(i=>i.Subscributions.Select(y=>y.SubscriptionRate))
                .Include(i=>i.Subscributions.Select(y=>y.Service))
                .SingleAsync();
//            return _context.Users.Include(i => i.Subscributions).FirstOrDefaultAsync(a => a.UserId==userId);
        }

        public Task UpdateUser(User user)
        {
//            _context.Users.AddOrUpdate(user);
            return _context.SaveChangesAsync();
        }

        public Task<bool> IsEmailExist(string email)
        {
            //            return Task.FromResult(false);
            return _context.Users.AnyAsync(a => a.Email == email);
        }

        public async Task<User> AddUser(User user)
        {
            var newuser = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return newuser;
        }

        //public Task<List<Service>> GetServices()
        //{
        //    List<Service> services = new List<Service>();
        //    foreach (var service in _context.Services)
        //    {
        //        services.Add(service);
        //    }
        //    return Task.FromResult(services);
        //}
    }
}
