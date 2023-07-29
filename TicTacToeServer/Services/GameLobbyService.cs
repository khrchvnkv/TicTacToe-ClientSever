using TicTacToe.Core.IConfiguration;
using TicTacToe.Models;
using TicTacToe.Models.Enums;

namespace TicTacToe.Services
{
    public sealed class GameLobbyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameLobbyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Game> CreateGame(Guid userGuid)
        {
            var game = new Game()
            {
                FirstUserId = userGuid,
                Status = GameStatus.Initializing
            };
            await _unitOfWork.Games.Add(game);
            await _unitOfWork.CompleteAsync();
            return game;
        }
        
        public async Task<Game?> JoinGame(Guid gameGuid, Guid userGuid)
        {
            var game = await _unitOfWork.Games.GetById(gameGuid);
            if (game is null || game.SecondUserId is not null) return null;

            game.SecondUserId = userGuid;
            await _unitOfWork.CompleteAsync();
            return game;
        }
    }
}