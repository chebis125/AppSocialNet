using System;
using System.Collections.Generic;
using System.Linq;
using AppSocialNet.Domain.Entities;
using AppSocialNet.Domain.Interfaces;
namespace AppSocialNet.Infrastructure.Repositories
{
    public class InMemoryPostRepository : IPostRepository
    {
        private readonly List<Post> _posts = new List<Post>();

        public void AddPost(Post post)
        {
            _posts.Add(post);
        }

        public IEnumerable<Post> GetPostsByUser(string username)
        {
            return _posts.Where(p => p.Username == username);
        }

        public IEnumerable<Post> GetPostsByUsers(List<User> users)
        {
            var usernames = users.Select(u => u.Username).ToList();
            return _posts.Where(p => usernames.Contains(p.Username));
        }
    }
}