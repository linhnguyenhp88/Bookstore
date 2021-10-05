using AutoMapper;
using BookShop.Common.Paging;
using BookShop.Domain.DbContexts;
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces.IRepository;
using BookShop.Domain.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
      
        public BooksService(IRepository<Book> bookRepository, ILoggingService loggingService)
        {
            _bookRepository = bookRepository;
            _logger = loggingService.CreateLogger<BooksService>();
        }

        /// <summary>
        /// Request query book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Book> GetBookAsync(int id)
        {
            _logger.LogInformation($"Begining get book by Id = {id}");

            return await _bookRepository.Query().Include(b => b.Author).Include(b => b.Photos)
                .FirstOrDefaultAsync(b => b.Id == id);        
        }

        /// <summary>
        /// Request query all book
        /// </summary>
        /// <param name="commandQuery"></param>
        /// <returns></returns>
        public async Task<PagedList<Book>> GetBooksAsync(BookPagingFilteringModel commandQuery)
        {
            _logger.LogInformation($"Begining query all book records =  {commandQuery}");
            var mainQuery = _bookRepository.Query().Include(b => b.Author).Include(b => b.Photos).AsQueryable();
            try
            {            
                if (!string.IsNullOrEmpty(commandQuery.QueryParams))
                {
                    var queryFilter = commandQuery.QueryParams.Trim().ToUpper();
                    mainQuery = mainQuery.Where(x => x.Author.Name.Trim().ToUpper().Contains(queryFilter) || x.Title.Trim().ToUpper().Contains(queryFilter));
                }             
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Get all book: An error occured: {ex.StackTrace}");
            }
            return await PagedList<Book>.CreateAsync(mainQuery, commandQuery.PageNumber, commandQuery.PageSize);
        }
    }
}
