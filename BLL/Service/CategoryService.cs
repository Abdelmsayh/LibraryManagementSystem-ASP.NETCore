using AutoMapper;
using BLL.Interfaces.ICustamRepository;
using BLL.Interfaces.IService;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CategoryService : ICategoryService
    {
        protected readonly IGenericRepository<Category> _genericRepo;
        protected readonly ICategoryRepository _categoryRepo;
        protected readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> genericRepo, ICategoryRepository categoryRepo, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<bool> AddCategoryAsync(CategoryDTO entity)
        {
            var category = _mapper.Map<Category>(entity);
            return await _genericRepo.AddAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(CategoryDTO entity)
        {
            var category = _mapper.Map<Category>(entity);
            return await _genericRepo.DeleteAsync(category);
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync(Expression<Func<CategoryDTO, bool>>? filter = null)
        {
            var categories = await _genericRepo.GetAllAsync();
            var mapped = _mapper.Map<List<CategoryDTO>>(categories);

            if (filter != null)
                mapped = mapped.AsQueryable().Where(filter).ToList();

            return mapped;
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _genericRepo.GetByAsync(c => c.Id == id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO?> GetSingleCategoryAsync(Expression<Func<CategoryDTO, bool>> filter)
        {
            var categories = await _genericRepo.GetAllAsync();
            var mapped = _mapper.Map<List<CategoryDTO>>(categories);
            return mapped.AsQueryable().FirstOrDefault(filter);
        }

        public async Task<List<BookDTO>> GetBooksInCategoryAsync(Guid categoryId)
        {
            var books = await _categoryRepo.GetBooksInCategoryAsync(categoryId);
            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<List<BookDTO>> SearchBooksInCategoryAsync(Guid categoryId, Expression<Func<BookDTO, bool>> predicate)
        {
            var books = await _categoryRepo.SearchBooksInCategoryAsync(categoryId, b => true);
            var mappedBooks = _mapper.Map<List<BookDTO>>(books);
            return mappedBooks.AsQueryable().Where(predicate).ToList();
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDTO entity)
        {
            var category = _mapper.Map<Category>(entity);
            return await _genericRepo.UpdateAsync(category);
        }
    }
}
