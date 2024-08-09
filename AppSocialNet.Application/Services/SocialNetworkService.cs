using System;
using System.Collections.Generic;
using System.Linq;
using AppSocialNet.Domain.Entities;
using AppSocialNet.Domain.Interfaces;

namespace AppSocialNet.Application.UseCases
{
    public class SocialNetworkService : ISocialNetworkService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public SocialNetworkService(IUserRepository userRepository, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        public void PostMessage(string username, string message)
        {
            var user = _userRepository.GetUser(username);
            if (user == null)
            {
                user = new User { Username = username };
                _userRepository.AddUser(user);
            }

            var post = new Post
            {
                Username = username,
                Message = message,
                Timestamp = DateTime.Now
            };

            _postRepository.AddPost(post);
        }

        public void FollowUser(string followerUsername, string followeeUsername)
        {
            var follower = _userRepository.GetUser(followerUsername);
            var followee = _userRepository.GetUser(followeeUsername);

            if (follower == null || followee == null)
            {
                throw new Exception("Uno o ambos usuarios no existen.");
            }

            if (!follower.Following.Contains(followee))
            {
                follower.Following.Add(followee);
                _userRepository.UpdateUser(follower);
            }
        }

        public IEnumerable<Post> GetDashboard(string username)
        {
            var user = _userRepository.GetUser(username);
            if (user == null) return Enumerable.Empty<Post>();

            var posts = new List<Post>();
            foreach (var followedUser in user.Following)
            {
                posts.AddRange(_postRepository.GetPostsByUser(followedUser.Username));
            }

            posts.Sort((p1, p2) => p2.Timestamp.CompareTo(p1.Timestamp));
            return posts;
        }
    }
}