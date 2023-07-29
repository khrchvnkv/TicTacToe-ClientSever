using TicTacToe.Models.Enums;
using TicTacToe.Models.Interfaces;

namespace TicTacToe.Models
{
    public class Game : IEntity
    {
        public Guid Id { get; set; }
        
        public virtual Guid FirstUserId { get; set; }
        public virtual User FirstUser { get; set; }
        
        public virtual Guid? SecondUserId { get; set; }
        public virtual User? SecondUser { get; set; }
        
        public GameStatus Status { get; set; }

        public virtual ICollection<Move> Moves { get; set; } = new List<Move>();
    }
}