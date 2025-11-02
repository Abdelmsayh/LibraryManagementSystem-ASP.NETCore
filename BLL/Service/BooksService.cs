using AutoMapper;
using AutoMapper.Execution;
using BLL.Interfaces.ICustamRepository;
using BLL.Interfaces.IService;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class BooksService : IBooksService
    {
        protected readonly IGenericRepository<Book> _genericRepo;
        protected readonly IBookRepository _bookRepo;
        protected readonly IMapper _mapper;

        public BooksService(IGenericRepository<Book> genericRepo, IBookRepository bookRepo, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<bool> AddBookAsync(BookDTO entity)
        {
            var book = _mapper.Map<Book>(entity);
            return await _genericRepo.AddAsync(book);
        }

        public async Task<bool> UpdateBookAsync(BookDTO entity)
        {
            var book = _mapper.Map<Book>(entity);
            return await _genericRepo.UpdateAsync(book);
        }

        public async Task<bool> DeleteBookAsync(BookDTO entity)
        {
            var book = _mapper.Map<Book>(entity);
            return await _genericRepo.DeleteAsync(book);
        }

        public async Task<List<BookDTO>> GetAllBooksAsync(Expression<Func<BookDTO, bool>>? filter = null)
        {
            IEnumerable<Book> collection = await _genericRepo.GetAllIncludeAsync(
                filter: cat => cat.IsActive == true,
                includeProperties: new List<Expression<Func<Book, object>>>
                {
                     a => a.Category
                });

            var booksDTO = _mapper.Map<IEnumerable<BookDTO>>(collection);
            return booksDTO.ToList();
        }


        public async Task<IEnumerable<BookDTO>> GetBooksByCategoryAsync(Guid categoryId)
        {
            var books = await _genericRepo.GetAllIncludeAsync(
                filter: b => b.IsActive && b.CategoryId == categoryId,
                includeProperties: new List<Expression<Func<Book, object>>>
                {
                      b => b.Category
                });

            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }


        public async Task<BookDTO?> GetBooksByIdAsync(Guid id)
        {
            var includeProps = new List<Expression<Func<Book, object>>>
                {
                    m => m.Category
                   
                };

            var book = await _genericRepo.GetByIncludeAsync(
                m => m.Id == id,
                includeProps
            );

            if (book == null)
                
                return null; return _mapper.Map<BookDTO>(book);
        }

        public async Task<List<BookDTO>> SearchBookAsync(string keyword)
        {
            var books = await _bookRepo.SearchBookAsync(keyword);
            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO?> GetTopRatingBookAsync()
        {
            var book = await _bookRepo.GetTopRatingBookAsync();
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO?> GetLowRatingBookAsync()
        {
            var book = await _bookRepo.GetLowRatingBookAsync();
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO?> GetTopBorrowedBookAsync()
        {
            var book = await _bookRepo.GetTopBorrowedBookAsync();
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO?> GetTopReservedBookAsync()
        {
            var book = await _bookRepo.GetTopReservedBookAsync();
            return _mapper.Map<BookDTO>(book);
        }
    }
}
