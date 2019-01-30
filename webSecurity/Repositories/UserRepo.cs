using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webSecurity.Data;
using webSecurity.ViewModels;

namespace webSecurity.Repositories
{
    public class UserRepo
    {
        ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        // Get all users in the databaFse.
        public IEnumerable<UserVM> All()
        {
            var users = _context.Users.Select(u => new UserVM()
            {
                Email = u.Email
            });
            return users;
        }

        public UserVM GetUser(string id)
        {
            var user = _context.Users.Select(u => new UserVM()
            {
                Email = u.Email
            }).Where(u => u.Email == id).FirstOrDefault();
            return user;
        }

        public bool RemoveUser(string id)
        {
            var user = _context.Users.Select(u => u).Where(u => u.Email == id).FirstOrDefault();
            if(id == null)
            {
                return false;
            }
            _context.Users.Remove(user);

            _context.SaveChanges();
            return true;
        }
    }

}
