using Microsoft.AspNetCore.SignalR.Client;

namespace TicTacToeClient.Services
{
    public class GameLobbyService
    {
        private const string ConnectionURL = $"{Constants.ServerURL}/game-hub";
        
        private HubConnection _hubConnection;

        public event Action<Guid> GameStarted;

        public async Task StartLobbyListening()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(ConnectionURL)
                .Build();

            _hubConnection.On<Guid>(nameof(StartGame), StartGame);
            _hubConnection.On<Guid, Guid, int>(nameof(GetMove), GetMove);
            
            await _hubConnection.StartAsync();
        }
        public async Task SendMessageToClientsAndStartGame(Guid gameId) => await _hubConnection.SendAsync(nameof(StartGame), gameId);
        public async Task MakeMove(Guid gameId, Guid userId, int cellIndex)
        {
            // TODO: отправка данных о ходе игрока

            await _hubConnection.SendAsync(nameof(MakeMove), gameId, userId, cellIndex);
        }
        public async Task GetMove(Guid gameId, Guid userId, int cellIndex)
        {
            Console.WriteLine($"Get Move: {gameId}:{userId}:{cellIndex}");
        }
        private void StartGame(Guid gameId) => GameStarted?.Invoke(gameId);
    }
}