using AppSocialNet.Application;
using AppSocialNet.Application.UseCases;
using AppSocialNet.Domain.Entities;
using AppSocialNet.Domain.Interfaces;
using Moq;
using Xunit;

namespace AppSocialNet.Tests
{
    public class SocialNetworkServiceTests
    {
        private readonly SocialNetworkService _service;
        private readonly Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
        private readonly Mock<IPostRepository> _postRepoMock = new Mock<IPostRepository>();

        public SocialNetworkServiceTests()
        {
            _service = new SocialNetworkService(_userRepoMock.Object, _postRepoMock.Object);
        }

        [Fact]
        public void PostMessage_ShouldAddPost()
        {
            var username = "Alfonso";
            var message = "Hola Mundo";

            _service.PostMessage(username, message);

            _postRepoMock.Verify(repo => repo.AddPost(It.Is<Post>(p => p.Username == username && p.Message == message)), Times.Once);
        }

        [Fact]
        public void FollowUser_ShouldAddFollowing()
        {
            var follower = new User { Username = "Alicia" };
            var followee = new User { Username = "Ivan" };

            _userRepoMock.Setup(repo => repo.GetUser("Alicia")).Returns(follower);
            _userRepoMock.Setup(repo => repo.GetUser("Ivan")).Returns(followee);

            _service.FollowUser("Alicia", "Ivan");

            Assert.Contains(followee, follower.Following);
            _userRepoMock.Verify(repo => repo.UpdateUser(follower), Times.Once);
        }

        [Fact]
        public void GetDashboard_ShouldReturnPostsFromFollowedUsers()
        {
            var follower = new User { Username = "Alicia" };
            var followee = new User { Username = "Ivan" };

            follower.Following.Add(followee);

            var posts = new List<Post>
            {
                new Post { Username = "Ivan", Message = "First post", Timestamp = DateTime.Now.AddMinutes(-5) },
                new Post { Username = "Ivan", Message = "Second post", Timestamp = DateTime.Now }
            };

            _userRepoMock.Setup(repo => repo.GetUser("Alicia")).Returns(follower);
            _postRepoMock.Setup(repo => repo.GetPostsByUser("Ivan")).Returns(posts);

            var result = _service.GetDashboard("Alicia");

            Assert.Equal(2, result.Count());
            Assert.Equal("Second post", result.First().Message);
        }
    }
}