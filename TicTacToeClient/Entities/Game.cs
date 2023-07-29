namespace TicTacToeClient.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        
        private GameBoard _gameBoard;

        public void StartGame()
        {
            _gameBoard = new GameBoard();
            _gameBoard.ShowGameBoard(new HashSet<int>(), new HashSet<int>());
        }
    }
}