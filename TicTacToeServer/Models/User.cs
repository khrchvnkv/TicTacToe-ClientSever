using TicTacToe.Models.Interfaces;

namespace TicTacToe.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }

        public virtual ICollection<Game> FirstUserGames { get; set; } = new List<Game>();
        public virtual ICollection<Game> SecondUserGames { get; set; } = new List<Game>();
        public virtual ICollection<Move> Moves { get; set; } = new List<Move>();
    }
}