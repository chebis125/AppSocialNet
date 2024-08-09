using AppSocialNet.Application.UseCases;
using AppSocialNet.Infrastructure.Repositories;
using System;
using System.Linq;

namespace AppSocialNet.ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new InMemoryUserRepository();
            var postRepository = new InMemoryPostRepository();
            var socialNetworkService = new SocialNetworkService(userRepository, postRepository);

            // Seed initial data
            userRepository.SeedUsers();

            Console.WriteLine("Bienvenido a AppSocialNet! Escribe un comando (post, follow, dashboard) o 'exit' para salir.");

            string input;
            while ((input = Console.ReadLine() ?? string.Empty) != "exit")
            {
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Comando no válido. Intenta de nuevo.");
                    continue;
                }

                var commandParts = input.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
                if (commandParts.Length < 2)
                {
                    Console.WriteLine("Comando no válido. Intenta de nuevo.");
                    continue;
                }

                var command = commandParts[0].ToLower();
                var username = commandParts[1];

                switch (command)
                {
                    case "post":
                        if (commandParts.Length < 3)
                        {
                            Console.WriteLine("Debe ingresar un mensaje para postear.");
                            break;
                        }

                        var message = commandParts[2];
                        socialNetworkService.PostMessage(username, message);
                        var postTimestamp = DateTime.Now.ToString("HH:mm");
                        Console.WriteLine($"{username} publicó -> \"{message}\" a las {postTimestamp}");
                        break;

                    case "follow":
                        if (commandParts.Length < 3)
                        {
                            Console.WriteLine("Debe especificar a quién seguir.");
                            break;
                        }

                        var userToFollow = commandParts[2];
                        try
                        {
                            socialNetworkService.FollowUser(username, userToFollow);
                            var followTimestamp = DateTime.Now.ToString("HH:mm");
                            Console.WriteLine($"{username} comenzó a seguir a {userToFollow} a las {followTimestamp}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "dashboard":
                        var posts = socialNetworkService.GetDashboard(username).ToList();
                        Console.WriteLine($"Número de publicaciones en el dashboard de {username}: {posts.Count}");
                        if (posts.Count == 0)
                        {
                            Console.WriteLine($"El dashboard de {username} está vacío.");
                        }
                        else
                        {
                            Console.WriteLine($"Dashboard de {username}:");
                            foreach (var post in posts.OrderByDescending(p => p.Timestamp))
                            {
                                Console.WriteLine($"{post.Timestamp.ToString("HH:mm")} - {post.Username}: {post.Message}");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Comando no reconocido. Usa 'post', 'follow', 'dashboard' o 'exit'.");
                        break;
                }

                Console.WriteLine();
            }

            Console.WriteLine("¡Gracias por usar AppSocialNet!");
        }
    }
}