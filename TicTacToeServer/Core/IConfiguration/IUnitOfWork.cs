using TicTacToe.Core.Repositories;

namespace TicTacToe.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        GamesRepository Games { get; }
        UsersRepository Users { get; }
        MovesRepository Moves { get; }

        Task CompleteAsync();
    }
}