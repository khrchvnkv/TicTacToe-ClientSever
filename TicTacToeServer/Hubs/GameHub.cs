using Microsoft.AspNetCore.SignalR;

namespace TicTacToe.Hubs
{
    public sealed class GameHub : Hub
    {
        public async Task StartGame(Guid gameId) => await Clients.All.SendAsync(nameof(StartGame), gameId);
        public async Task MakeMove(Guid gameId, Guid userId, int cellIndex) => await Clients.All.SendAsync(
            "GetMove", gameId, userId, cellIndex);
    }
}