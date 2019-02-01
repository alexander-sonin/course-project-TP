using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TPproject.Models
{
    public class UserContext : DbContext
    {
        public UserContext() :
        base("DbConnection")
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }

        public User GetUser(String name)
        {
            return User.FirstOrDefault(u => u.Email == name);
        }
        public List<User> GetAllUser(UserContext context)
        {
            List<User> UserList = new List<User>();
            User.Include(r => r.Role);
            foreach (User user in context.User.ToList())
            {
                UserList.Add(user);
            }
            return UserList;
        }
    }
}