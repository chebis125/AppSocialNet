using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSocialNet.Domain.Entities;
using AppSocialNet.Domain.Interfaces;

namespace AppSocialNet.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>();

        public User GetUser(string username)
        {
            _users.TryGetValue(username, out var user);
            return user;
        }

        public void UpdateUser(User user)
        {
            _users[user.Username] = user;
        }

        public void AddUser(User user)
        {
            if (!_users.ContainsKey(user.Username))
            {
                _users[user.Username] = user;
            }
        }

        public User GetByUsername(string username)
        {
            return GetUser(username); 
        }

 
        public void SeedUsers()
        {
            var user1 = new User { Username = "user1" };
            var user2 = new User { Username = "user2" };
            var user3 = new User { Username = "user3" };

            user1.Following.Add(user2);
            user2.Following.Add(user3);

            _users.Add(user1.Username, user1);
            _users.Add(user2.Username, user2);
            _users.Add(user3.Username, user3);
        }
    }
}