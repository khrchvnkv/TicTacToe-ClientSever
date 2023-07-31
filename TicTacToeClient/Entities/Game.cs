using System.Net.Http.Json;
using TicTacToeClient.Services;
using TicTacToeClient.UserInputParser;

namespace TicTacToeClient.Entities
{
    public class Game : IDisposable
    {
        private readonly GameLobbyService _lobby;
        public readonly User User;
        
        private GameBoard _gameBoard;
        private HashSet<int> _firstUserCells;
        private HashSet<int> _secondUserCells;

        public Guid Id { get; set; }
        public bool IsInitialized { get; set; }
        
        private HashSet<int> UserMoves 
        {
            get
            {
                if (User.IsAdmin) return _firstUserCells;
                return _secondUserCells;
            }    
        }
        
        public Game(GameLobbyService lobby, User user)
        {
            _lobby = lobby;
            User = user;
            lobby.GameStarted += TryInitializeGame;
        }

        public void Dispose()
        {
            _lobby.GameStarted -= TryInitializeGame;
        }

        public void StartGame()
        {
            _gameBoard = new GameBoard();
            _firstUserCells = new HashSet<int>();
            _secondUserCells = new HashSet<int>();
            _gameBoard.ShowGameBoard(_firstUserCells, _secondUserCells);
            StartGameLoop();
        }

        private void TryInitializeGame(Guid gameId)
        {
            if (gameId == Id) IsInitialized = true;
        }
        
        private async Task StartGameLoop()
        {
            var moveInputParser = new MoveUserInputParser("Enter cell index:");
            while (true)
            {
                if (User.CurrentTurn)
                {
                    var inputCellIndex =
                        moveInputParser.ParseMoveInput(_gameBoard.ShowGameBoard(_firstUserCells, _secondUserCells));
                    if (!_firstUserCells.Contains(inputCellIndex) &&
                        !_secondUserCells.Contains(inputCellIndex))
                    {
                        await _lobby.MakeMove(Id, User.Id, inputCellIndex);
                        UserMoves.Add(inputCellIndex);
                        Console.WriteLine("TEST");
                        User.CurrentTurn = false;
                    }
                }
                else
                {
                    while (!User.CurrentTurn)
                    {
                        Thread.Sleep(200);
                    }
                }
            }
        }
    }
}