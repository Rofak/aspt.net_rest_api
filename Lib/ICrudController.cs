using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Restfull.Lib
{
    public interface ICrudController<TEntity>
    {
        IEnumerable<TEntity> FindAll();
        ActionResult <TEntity> FindById(int id);
        Task<TEntity> Create([FromBody]TEntity entity);
        Task<TEntity> Update(int id,[FromBody]TEntity entity);
        Task<TEntity> Delete(int id);
    }
}
