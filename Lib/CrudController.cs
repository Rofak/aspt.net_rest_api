
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restfull.Service;

namespace Restfull.Lib
{
    [Authorize]
    public abstract class CrudController<TEntity,Service> :ControllerBase, ICrudController<TEntity> where TEntity : class
    {
        public Service service;

        public CrudController(Service _service) {
            service = _service; 
        }

        [HttpPost]
        public virtual async Task<TEntity> Create([FromBody] TEntity entity)
        {
            return await  (service as CrudService<TEntity>).Create(entity);
        }

        [HttpDelete("{id}")]
        public async Task<TEntity> Delete(int id)
        {
            return await (service as CrudService<TEntity>).Delete(id);
        }

        [HttpGet]
        public IEnumerable<TEntity> FindAll()
        {
           return (service as CrudService<TEntity>).FindAll();
        }

        [HttpGet("{id}")]
        public ActionResult<TEntity> FindById(int id)
        {
            var data= (service as CrudService<TEntity>).FindById(id);
            if (data != null)
            {
                return Ok(data);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public virtual async Task<TEntity> Update(int id ,TEntity entity)
        {
            return await (service as CrudService<TEntity>).Update(id, entity); 
        }
    }
}
