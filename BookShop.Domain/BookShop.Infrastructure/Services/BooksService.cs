using AutoMapper;
using BookShop.Common.Paging;
using BookShop.Domain.DbContexts;
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces.IRepository;
using BookShop.Domain.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Infrastructure.Services
{
    public class BooksService : IBooksService
    {
        private readonly IRepository<Book> _bookRepository;     
        private readonly IMapper _mapper;
        public BooksService(IRepository<Book> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;       
            _mapper = mapper;
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _bookRepository.Query().Include(b => b.Author).Include(b => b.Photos)
                .FirstOrDefaultAsync(b => b.Id == id);        
        }

        public async Task<PagedList<Book>> GetBooksAsync(BookPagingFilteringModel commandQuery)
        {                             
            var mainQuery = _bookRepository.Query().Include(b => b.Author).Include(b => b.Photos).AsQueryable();

            if (!string.IsNullOrEmpty(commandQuery.QueryParams))
            {
                var queryFilter = commandQuery.QueryParams.Trim().ToUpper();
                mainQuery = mainQuery.Where(x => x.Author.Name.Trim().ToUpper().Contains(queryFilter) || x.Title.Trim().ToUpper().Contains(queryFilter));
            }

            return await PagedList<Book>.CreateAsync(mainQuery, commandQuery.PageNumber, commandQuery.PageSize);
        }
    }
}
