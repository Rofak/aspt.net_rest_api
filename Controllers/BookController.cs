using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restfull.CustomAttribute;
using Restfull.Data;
using Restfull.DTO.Book;
using Restfull.Entity;
using Restfull.Lib;
using Restfull.Service;
using System;
using System.Reflection;

namespace Restfull.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class BookController : CrudController<Book, BookSerivce>
    {
        public BookController(DataContext dataContext) : base(new BookSerivce(dataContext))
        {
          
        }

    }
}

