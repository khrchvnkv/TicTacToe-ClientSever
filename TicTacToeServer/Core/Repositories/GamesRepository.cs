using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Core.Repositories
{
    public class GamesRepository : GenericRepository<Game>
    {
        public GamesRepository(AppDbContext context) : base(context)
        { }

        protected override void CopyDataOnUpdating(Game from, Game to)
        {
            to.FirstUserId = from.FirstUserId;
            to.SecondUserId = from.SecondUserId;
            to.Status = from.Status;
        }
    }
}