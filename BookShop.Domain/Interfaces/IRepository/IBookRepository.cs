using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Domain.Interfaces.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        IQueryable<Book> GetBookAsyncByAuthor(string authorName);
    }
}
