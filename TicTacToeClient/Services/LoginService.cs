using System.Net.Http.Json;
using Newtonsoft.Json;
using TicTacToeClient.Entities;

namespace TicTacToeClient.Services
{
    public sealed class LoginService
    {
        public async Task<User> LoginUser(string? username)
        {
            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync($"{Constants.ServerURL}/api/login", username);

            var userData = new User();
            
            var result = await response.Content.ReadAsStringAsync();
            var dynamicData = JsonConvert.DeserializeObject<dynamic>(result);
            
            if (dynamicData is not null)
            {
                userData.Id = dynamicData.id;
                userData.Name = dynamicData.name;
            }

            return userData;
        }
    }
}