namespace TicTacToe.Core.IRepositories
{
    public interface IGenericRepository<TModel> where TModel: class
    {
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel?> GetById(Guid id);
        Task<bool> Add(TModel entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(TModel entity);
    }
}