using BookShop.Domain.DbContexts;
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookShop.Domain.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookShopContext context)
            : base(context)
        {

        }
        public IQueryable<Book> GetBookAsyncByAuthor(string authorName)
        {
            return DbSet.Where(x => x.Author.Name == authorName);
        }
    }
}
