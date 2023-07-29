using Microsoft.EntityFrameworkCore;
using TicTacToe.Core.IRepositories;
using TicTacToe.Data;
using TicTacToe.Models.Interfaces;

namespace TicTacToe.Core.Repositories
{
    public abstract class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class, IEntity
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<TModel> DbSet;

        protected GenericRepository(AppDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TModel>();
        }

        public async Task<IEnumerable<TModel>> GetAll() => await DbSet.ToListAsync();
        public async Task<TModel?> GetById(Guid id) => await DbSet.FirstOrDefaultAsync(e => e.Id == id);
        public async Task<bool> Add(TModel entity)
        {
            await DbSet.AddAsync(entity);
            return true;
        }
        public async Task<bool> Delete(Guid id)
        {
            var dbEntity = await GetById(id);
            if (dbEntity is null) return false;

            DbSet.Remove(dbEntity);
            return true;
        }
        public async Task<bool> Update(TModel entity)
        {
            var dbEntity = await GetById(entity.Id);
            if (dbEntity is null) return await Add(entity);

            CopyDataOnUpdating(entity, dbEntity);
            return true;
        }
        protected abstract void CopyDataOnUpdating(TModel from, TModel to);
    }
}