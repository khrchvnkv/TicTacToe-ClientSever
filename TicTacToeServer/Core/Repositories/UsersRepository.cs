using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Core.Repositories
{
    public class UsersRepository : GenericRepository<User>
    {
        public UsersRepository(AppDbContext context) : base(context)
        { }

        public async Task<User?> GetByName(string? name)
        {
            if (name is null || string.IsNullOrEmpty(name)) return null;

            return await DbSet.FirstOrDefaultAsync(u => u.Name == name);
        }
        protected override void CopyDataOnUpdating(User from, User to)
        {
            to.Name = from.Name;
        }
    }
}