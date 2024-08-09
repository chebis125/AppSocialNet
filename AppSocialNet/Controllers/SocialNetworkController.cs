using Microsoft.AspNetCore.Mvc;
using AppSocialNet.Domain.Interfaces;


namespace AppSocialNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocialNetworkController : ControllerBase
    {
        private readonly ISocialNetworkService _socialNetworkService;

        public SocialNetworkController(ISocialNetworkService socialNetworkService)
        {
            _socialNetworkService = socialNetworkService;
        }

        [HttpPost("post")]
        public IActionResult PostMessage([FromQuery] string username, [FromQuery] string message)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("El nombre de usuario y el mensaje son requeridos.");
            }

            try
            {
                _socialNetworkService.PostMessage(username, message);
                var postTimestamp = DateTime.Now.ToString("HH:mm");
                return Ok($"Mensaje publicado por {username} a las {postTimestamp}: \"{message}\"");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al publicar el mensaje: {ex.Message}");
            }
        }

        [HttpPost("follow")]
        public IActionResult FollowUser([FromQuery] string follower, [FromQuery] string followee)
        {
            if (string.IsNullOrWhiteSpace(follower) || string.IsNullOrWhiteSpace(followee))
            {
                return BadRequest("El nombre de usuario seguidor y el nombre de usuario a seguir son requeridos.");
            }

            try
            {
                _socialNetworkService.FollowUser(follower, followee);
                var followTimestamp = DateTime.Now.ToString("HH:mm");
                return Ok($"{follower} comenzó a seguir a {followee} a las {followTimestamp}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al seguir al usuario: {ex.Message}");
            }
        }

        [HttpGet("dashboard")]
        public IActionResult GetDashboard([FromQuery] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("El nombre de usuario es requerido.");
            }

            try
            {
                var posts = _socialNetworkService.GetDashboard(username);

                if (posts == null || !posts.Any())
                {
                    return NotFound($"El dashboard de {username} está vacío.");
                }

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el dashboard: {ex.Message}");
            }
        }
    }
}