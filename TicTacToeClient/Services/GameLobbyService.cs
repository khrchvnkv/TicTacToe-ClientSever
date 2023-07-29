using Microsoft.AspNetCore.SignalR.Client;
using TicTacToeClient.Entities;

namespace TicTacToeClient.Services
{
    public class GameLobbyService
    {
        private const string ConnectionURL = $"{Constants.ServerURL}/game-hub";

        private readonly Game _game;

        private HubConnection _hubConnection;

        public GameLobbyService(Game game)
        {
            _game = game;
        }

        public async Task StartLobbyListening()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(ConnectionURL)
                .Build();

            _hubConnection.On<string>(nameof(JoinToGame), async message => await JoinToGame(message));
            
            await _hubConnection.StartAsync();

            //var username = Console.ReadLine();
            //await hubConnection.SendAsync("Login", username);
        }

        private async Task JoinToGame(string json)
        {
            Console.WriteLine(json);
        }
    }
}