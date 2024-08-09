using AppSocialNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSocialNet.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string username);
        void AddUser(User user);
        void UpdateUser(User user);
        User GetByUsername(string username); 
    }
}
