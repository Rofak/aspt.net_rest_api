using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Restfull.CustomAttribute;
using Restfull.Data;
using Restfull.DTO.User;
using Restfull.Service.Interface;

namespace Restfull.Service
{
    public class CrudService<TEntity>: ICrudService<TEntity> where TEntity : class
    {
        public DataContext _dataContext;
        public DbSet<TEntity> table;

        public CrudService(DataContext dataContext)
        {       
            _dataContext = dataContext;
            table = _dataContext.Set<TEntity>();
            
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await table.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity =await table.FindAsync(id);
            if(entity != null)
            {
                table.Remove(entity);
                await _dataContext.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public IEnumerable<TEntity> FindAll()
        {
            return table;
        }

        public TEntity FindById(int id)
        {
            var entity=table.Find(id);
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<TEntity> Update(int id, TEntity entity)
        {
            _dataContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var data=_dataContext.Find<TEntity>(id);
            if(data != null)
            {
                dynamic payload = entity;
                payload.id = id;
                table.Update(payload);
                await _dataContext.SaveChangesAsync();
                return entity;
            }
            return null;
       
        }
    }
}
