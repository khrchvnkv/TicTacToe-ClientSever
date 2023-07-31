namespace TicTacToeClient.Entities
{
    public class Move
    {
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        public int CellIndex { get; set; }
    }
}