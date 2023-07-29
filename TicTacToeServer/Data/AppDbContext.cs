using Microsoft.EntityFrameworkCore;
using TicTacToe.Models;

namespace TicTacToe.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Move> Moves => Set<Move>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User-Game relationship

            modelBuilder
                .Entity<Game>()
                .HasOne(g => g.FirstUser)
                .WithMany(u => u.FirstUserGames)
                .HasForeignKey(g => g.FirstUserId);

            modelBuilder
                .Entity<Game>()
                .HasOne(g => g.SecondUser)
                .WithMany(u => u.SecondUserGames)
                .HasForeignKey(g => g.SecondUserId);

            #endregion

            #region User-Move relationships

            modelBuilder
                .Entity<Move>()
                .HasOne(m => m.User)
                .WithMany(u => u.Moves)
                .HasForeignKey(m => m.UserId);

            #endregion

            #region Game-Move relationships

            modelBuilder
                .Entity<Move>()
                .HasOne(m => m.Game)
                .WithMany(g => g.Moves)
                .HasForeignKey(m => m.GameId);

            #endregion
        }
    }
}