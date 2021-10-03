﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookShop.Common.Paging;
using BookShop.Domain.Dtos;
using BookShop.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService, IMapper mapper)
        {
            _booksService = booksService;
            _mapper = mapper;
        }

        [HttpGet]      
        [Route("get-book/{id}", Name = "GetBook")]
        public async Task<IActionResult> GetBook(int id)
        {
            var bookEntity = await _booksService.GetBookAsync(id);

            if (bookEntity == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<BookForDetailedDto>(bookEntity);

            return Ok(result);
        }

        [HttpGet]
        [Route("{get-all-books}", Name = "get-all-books")]
        public async Task<IActionResult> GetBooks([FromQuery]BookPagingFilteringModel command)
        {
            var bookEntities = await _booksService.GetBooksAsync(command);

            var result = _mapper.Map<IEnumerable<BookForListDto>>(bookEntities);

            Response.AddPagination(bookEntities.CurrentPage, bookEntities.PageSize,
                bookEntities.TotalCount, bookEntities.TotalPages);

            return Ok(result);
        }
    }
}