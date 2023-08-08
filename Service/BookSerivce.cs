using Restfull.Data;
using Restfull.Entity;

namespace Restfull.Service
{
    public class BookSerivce:CrudService<Book>
    {
        public DataContext Context;
        public BookSerivce(DataContext dataContext) : base(dataContext)
        {
            Context = dataContext;
        }
    }
}
