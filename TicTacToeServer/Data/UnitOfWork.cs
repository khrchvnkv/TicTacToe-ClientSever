using TicTacToe.Core.IConfiguration;
using TicTacToe.Core.Repositories;

namespace TicTacToe.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public GamesRepository Games { get; }
        public UsersRepository Users { get; }
        public MovesRepository Moves { get; }
        
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Games = new GamesRepository(context);
            Users = new UsersRepository(context);
            Moves = new MovesRepository(context);
        }
        
        public async Task CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}