namespace Restfull.Service.Interface
{
    public interface ICrudService<TEntity>
    {
        IEnumerable<TEntity> FindAll();
        TEntity FindById(int id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(int id ,TEntity entity);
        Task<TEntity> Delete(int id);
    }
}
