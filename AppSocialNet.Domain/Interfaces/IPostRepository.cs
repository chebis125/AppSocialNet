using AppSocialNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSocialNet.Domain.Interfaces
{
    public interface IPostRepository
    {
        void AddPost(Post post);
        IEnumerable<Post> GetPostsByUser(string username);
        IEnumerable<Post> GetPostsByUsers(List<User> users); 
    }
}
