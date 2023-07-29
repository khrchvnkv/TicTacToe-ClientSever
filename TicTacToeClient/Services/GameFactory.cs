using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using TicTacToeClient.Entities;

namespace TicTacToeClient.Services
{
    public class GameFactory
    {
        public async Task<Game> CreateGame(Guid userId)
        {
            using var client = new HttpClient();
            var response =
                await client.PostAsJsonAsync($"{Constants.ServerURL}/api/create-game/{userId}", userId);

            var game = new Game();
            var result = await response.Content.ReadAsStringAsync();
            var dynamicData = JsonConvert.DeserializeObject<dynamic>(result);
            if (dynamicData is not null)
            {
                game.Id = dynamicData.id;
            }
            return game;
        }
        
        public async Task<Game?> JoinGame(string? gameId, Guid userId)
        {
            if (gameId is null || string.IsNullOrEmpty(gameId)) return null;
            
            using var client = new HttpClient();
            var response =
                await client.PatchAsJsonAsync($"{Constants.ServerURL}/api/join-game/{gameId}/{userId}", gameId);

            var statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.NoContent)
            {
                var game = new Game
                {
                    Id = Guid.Parse(gameId)
                };
                return game;
            }

            return null;
        }
    }
}