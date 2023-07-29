using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Core.Repositories
{
    public class MovesRepository : GenericRepository<Move>
    {
        public MovesRepository(AppDbContext context) : base(context)
        { }

        protected override void CopyDataOnUpdating(Move from, Move to)
        {
            to.GameId = from.GameId;
            to.UserId = from.UserId;
            to.CellIndex = from.CellIndex;
        }
    }
}