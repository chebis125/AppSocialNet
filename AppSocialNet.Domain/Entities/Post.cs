using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSocialNet.Domain.Entities
{
    public class Post
    {
        public DateTime Timestamp { get; set; }
        public string? Username { get; set; }
        public string? Message { get; set; }
    }
}
