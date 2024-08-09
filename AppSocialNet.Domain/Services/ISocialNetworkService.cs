using System.Collections.Generic;
using AppSocialNet.Domain.Entities;

namespace AppSocialNet.Domain.Interfaces
{
    public interface ISocialNetworkService
    {
        void PostMessage(string username, string message);
        void FollowUser(string followerUsername, string followeeUsername);
        IEnumerable<Post> GetDashboard(string username);
    }
}