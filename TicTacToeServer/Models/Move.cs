using TicTacToe.Models.Interfaces;

namespace TicTacToe.Models
{
    public class Move : IEntity
    {
        public Guid Id { get; set; }
        
        public virtual Guid GameId { get; set; }
        public virtual Game Game { get; set; }

        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
        
        public int CellIndex { get; set; }
    }
}