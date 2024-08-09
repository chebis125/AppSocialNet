using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSocialNet.Domain.Entities
{
    public class User
    {
        public string? Username { get; set; }
        public HashSet<User> Following { get; set; } = new HashSet<User>();
    }

}
