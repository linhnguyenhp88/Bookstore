using BookShop.Common.Paging;
using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Interfaces.IServices
{
    public interface IBooksService
    {
        Task<Book> GetBookAsync(int id);

        Task<PagedList<Book>> GetBooksAsync(BookPagingFilteringModel commandQuery);
    }
}
