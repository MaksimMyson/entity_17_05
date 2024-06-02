using System;
using System.Linq;
using entity_17_05.Context;
using entity_17_05.Models;

namespace entity_17_05
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new GameLibraryContext())
            {
                context.Database.EnsureCreated();

                if (!context.Games.Any())
                {
                    context.Games.AddRange(
                        new Game
                        {
                            Name = "The Witcher 3: Wild Hunt",
                            Studio = "CD Projekt Red",
                            Genre = "RPG",
                            ReleaseDate = new DateTime(2015, 5, 19),
                            GameMode = "Single-player",
                            CopiesSold = 25000000
                        },
                        new Game
                        {
                            Name = "Cyberpunk 2077",
                            Studio = "CD Projekt Red",
                            Genre = "RPG",
                            ReleaseDate = new DateTime(2020, 12, 10),
                            GameMode = "Single-player",
                            CopiesSold = 13000000
                        },
                        new Game
                        {
                            Name = "Overwatch",
                            Studio = "Blizzard Entertainment",
                            Genre = "FPS",
                            ReleaseDate = new DateTime(2016, 5, 24),
                            GameMode = "Multiplayer",
                            CopiesSold = 50000000
                        }
                    );

                    context.SaveChanges();
                }

                // Виклик нових функцій
                SearchGamesByName(context, "Cyberpunk 2077");
                SearchGamesByStudio(context, "CD Projekt Red");
                SearchGamesByStudioAndName(context, "CD Projekt Red", "The Witcher 3: Wild Hunt");
                SearchGamesByGenre(context, "RPG");
                SearchGamesByReleaseYear(context, 2020);
                DisplaySinglePlayerGames(context);
                DisplayMultiplayerGames(context);
                DisplayMostSoldGame(context);
                DisplayLeastSoldGame(context);
                DisplayTop3MostPopularGames(context);
                DisplayTop3LeastPopularGames(context);
                AddNewGame(context, new Game
                {
                    Name = "New Game",
                    Studio = "New Studio",
                    Genre = "Action",
                    ReleaseDate = DateTime.Now,
                    GameMode = "Single-player",
                    CopiesSold = 1000
                });
                UpdateGame(context, "Cyberpunk 2077", "CD Projekt Red", new Game
                {
                    Name = "Cyberpunk 2077",
                    Studio = "CD Projekt Red",
                    Genre = "RPG",
                    ReleaseDate = new DateTime(2020, 12, 10),
                    GameMode = "Single-player",
                    CopiesSold = 14000000
                });
                DeleteGame(context, "New Game", "New Studio");
            }
        }

        static void SearchGamesByName(GameLibraryContext context, string name)
        {
            var games = context.Games.Where(g => g.Name.Contains(name)).ToList();
            Console.WriteLine($"Games with name containing '{name}':");
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Genre: {game.Genre}, Release Date: {game.ReleaseDate.ToShortDateString()}");
            }
        }

        static void SearchGamesByStudio(GameLibraryContext context, string studio)
        {
            var games = context.Games.Where(g => g.Studio.Contains(studio)).ToList();
            Console.WriteLine($"Games by studio '{studio}':");
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Genre: {game.Genre}, Release Date: {game.ReleaseDate.ToShortDateString()}");
            }
        }

        static void SearchGamesByStudioAndName(GameLibraryContext context, string studio, string name)
        {
            var games = context.Games.Where(g => g.Studio.Contains(studio) && g.Name.Contains(name)).ToList();
            Console.WriteLine($"Games by studio '{studio}' with name containing '{name}':");
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Genre: {game.Genre}, Release Date: {game.ReleaseDate.ToShortDateString()}");
            }
        }

        static void SearchGamesByGenre(GameLibraryContext context, string genre)
        {
            var games = context.Games.Where(g => g.Genre.Contains(genre)).ToList();
            Console.WriteLine($"Games with genre '{genre}':");
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Genre: {game.Genre}, Release Date: {game.ReleaseDate.ToShortDateString()}");
            }
        }

        static void SearchGamesByReleaseYear(GameLibraryContext context, int year)
        {
            var games = context.Games.Where(g => g.ReleaseDate.Year == year).ToList();
            Console.WriteLine($"Games released in year '{year}':");
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Genre: {game.Genre}, Release Date: {game.ReleaseDate.ToShortDateString()}");
            }
        }

        static void DisplaySinglePlayerGames(GameLibraryContext context)
        {
            var games = context.Games.Where(g => g.GameMode == "Single-player").ToList();
            Console.WriteLine("Single-player games:");
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Genre: {game.Genre}, Release Date: {game.ReleaseDate.ToShortDateString()}");
            }
        }

        static void DisplayMultiplayerGames(GameLibraryContext context)
        {
            var games = context.Games.Where(g => g.GameMode == "Multiplayer").ToList();
            Console.WriteLine("Multiplayer games:");
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Genre: {game.Genre}, Release Date: {game.ReleaseDate.ToShortDateString()}");
            }
        }

        static void DisplayMostSoldGame(GameLibraryContext context)
        {
            var game = context.Games.OrderByDescending(g => g.CopiesSold).FirstOrDefault();
            if (game != null)
            {
                Console.WriteLine($"Most sold game: {game.Name}, Studio: {game.Studio}, Copies Sold: {game.CopiesSold}");
            }
        }

        static void DisplayLeastSoldGame(GameLibraryContext context)
        {
            var game = context.Games.OrderBy(g => g.CopiesSold).FirstOrDefault();
            if (game != null)
            {
                Console.WriteLine($"Least sold game: {game.Name}, Studio: {game.Studio}, Copies Sold: {game.CopiesSold}");
            }
        }

        static void DisplayTop3MostPopularGames(GameLibraryContext context)
        {
            var games = context.Games.OrderByDescending(g => g.CopiesSold).Take(3).ToList();
            Console.WriteLine("Top 3 most popular games:");
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Copies Sold: {game.CopiesSold}");
            }
        }

        static void DisplayTop3LeastPopularGames(GameLibraryContext context)
        {
            var games = context.Games.OrderBy(g => g.CopiesSold).Take(3).ToList();
            Console.WriteLine("Top 3 least popular games:");
            foreach (var game in games)
            {
                Console.WriteLine($"Name: {game.Name}, Studio: {game.Studio}, Copies Sold: {game.CopiesSold}");
            }
        }

        static void AddNewGame(GameLibraryContext context, Game newGame)
        {
            var existingGame = context.Games.FirstOrDefault(g => g.Name == newGame.Name && g.Studio == newGame.Studio);
            if (existingGame == null)
            {
                context.Games.Add(newGame);
                context.SaveChanges();
                Console.WriteLine($"Game '{newGame.Name}' by '{newGame.Studio}' added successfully.");
            }
            else
            {
                Console.WriteLine($"Game '{newGame.Name}' by '{newGame.Studio}' already exists.");
            }
        }

        static void UpdateGame(GameLibraryContext context, string name, string studio, Game updatedGame)
        {
            var game = context.Games.FirstOrDefault(g => g.Name == name && g.Studio == studio);
            if (game != null)
            {
                game.Name = updatedGame.Name;
                game.Studio = updatedGame.Studio;
                game.Genre = updatedGame.Genre;
                game.ReleaseDate = updatedGame.ReleaseDate;
                game.GameMode = updatedGame.GameMode;
                game.CopiesSold = updatedGame.CopiesSold;
                context.SaveChanges();
                Console.WriteLine($"Game '{name}' by '{studio}' updated successfully.");
            }
            else
            {
                Console.WriteLine($"Game '{name}' by '{studio}' not found.");
            }
        }

        static void DeleteGame(GameLibraryContext context, string name, string studio)
        {
            var game = context.Games.FirstOrDefault(g => g.Name == name && g.Studio == studio);
            if (game != null)
            {
                Console.WriteLine($"Are you sure you want to delete game '{name}' by '{studio}'? (y/n)");
                var confirmation = Console.ReadLine();
                if (confirmation?.ToLower() == "y")
                {
                    context.Games.Remove(game);
                    context.SaveChanges();
                    Console.WriteLine($"Game '{name}' by '{studio}' deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Deletion canceled.");
                }
            }
            else
            {
                Console.WriteLine($"Game '{name}' by '{studio}' not found.");
            }
        }
    }
}