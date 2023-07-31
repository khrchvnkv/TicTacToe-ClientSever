using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using TicTacToeClient.Entities;

namespace TicTacToeClient.Services
{
    public class GameFactory
    {
        public async Task<Game> CreateGame(User user, GameLobbyService lobby)
        {
            using var client = new HttpClient();
            var response =
                await client.PostAsJsonAsync($"{Constants.ServerURL}/api/create-game", user);

            var game = new Game(lobby, user);
            var result = await response.Content.ReadAsStringAsync();
            var dynamicData = JsonConvert.DeserializeObject<dynamic>(result);
            if (dynamicData is not null)
            {
                game.Id = dynamicData.id;
            }
            return game;
        }
        
        public async Task<Game?> JoinGame(GameLobbyService lobby, string? gameId, User user)
        {
            if (gameId is null || string.IsNullOrEmpty(gameId)) return null;
            
            using var client = new HttpClient();
            var response =
                await client.PatchAsJsonAsync($"{Constants.ServerURL}/api/join-game/{gameId}/{user.Id}", gameId);

            var statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.NoContent)
            {
                var game = new Game(lobby, user)
                {
                    Id = Guid.Parse(gameId)
                };
                return game;
            }

            return null;
        }
    }
}