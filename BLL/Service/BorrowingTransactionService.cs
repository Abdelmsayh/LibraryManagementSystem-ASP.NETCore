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
    public class BorrowingTransactionService : IBorrowingTransactionService
    {
        protected readonly IGenericRepository<BorrowingTransaction> _genericRepo;

        protected readonly IBorrowingTransactionRepository _transactionRepo;

        protected readonly IMapper _mapper;

        public BorrowingTransactionService(
            IGenericRepository<BorrowingTransaction> genericRepo,
            IBorrowingTransactionRepository transactionRepo,
            IMapper mapper)
        {
            _genericRepo = genericRepo;
            _transactionRepo = transactionRepo;
            _mapper = mapper;
        }

        

        public async Task<bool> AddBorrowTransactionAsync(BorrowingTransactionDTO entity)
        {
            var transaction = _mapper.Map<BorrowingTransaction>(entity);

            transaction.Id = Guid.NewGuid();
            transaction.ReturnDate = null; 

            return await _genericRepo.AddAsync(transaction);
        }

        public async Task<bool> UpdateBorrowTransactionAsync(BorrowingTransactionDTO entity)
        {
            var transaction = _mapper.Map<BorrowingTransaction>(entity);
            return await _genericRepo.UpdateAsync(transaction);
        }

        public async Task<bool> DeleteBorrowTransactionAsync(BorrowingTransactionDTO entity)
        {
            var transaction = _mapper.Map<BorrowingTransaction>(entity);
            return await _genericRepo.DeleteAsync(transaction);
        }

        public async Task<List<BorrowingTransactionDTO>> GetAllBorrowTransactionsAsync(Expression<Func<BorrowingTransactionDTO, bool>>? filter = null)
        {
            var transactions = await _genericRepo.GetAllIncludeAsync(
                filter: cat => cat.IsActive == true,
                includeProperties: new List<Expression<Func<BorrowingTransaction, object>>>
                {
                      a => a.Member
                });
            var mapped = _mapper.Map<List<BorrowingTransactionDTO>>(transactions);

            if (filter != null)
                mapped = mapped.AsQueryable().Where(filter).ToList();

            return mapped;
        }

        public async Task<BorrowingTransactionDTO?> GetBorrowTransactionByIdAsync(Guid id)
        {

            var includes = new List<Expression<Func<BorrowingTransaction, object>>>
            {
                  t => t.Member, 
                     t => t.Book     
             };

            var transaction = await _genericRepo.GetByIncludeAsync(t => t.Id == id, includes);

            return _mapper.Map<BorrowingTransactionDTO>(transaction);
        }


        public async Task BorrowBookAsync(Guid bookId, Guid memberId)
        {
            await _transactionRepo.BorrowBookAsync(bookId, memberId);
        }

        public async Task ReturnBookAsync(Guid transactionId)
        {
            await _transactionRepo.ReturnBookAsync(transactionId);
        }

        public async Task<decimal> CalculateFineAsync(Guid transactionId)
        {
            return await _transactionRepo.CalculateFineAsync(transactionId);
        }


        public async Task<List<BorrowingTransactionDTO>> GetOverdueTransactionsAsync()
        {
            var transactions = await _transactionRepo.GetOverdueTransactionsAsync();
            return _mapper.Map<List<BorrowingTransactionDTO>>(transactions);
        }

        public async Task<List<BookDTO>> GetBorrowedBooksAsync()
        {
            var books = await _transactionRepo.GetBorrowedBooksAsync();
            return _mapper.Map<List<BookDTO>>(books);
        }


        public async Task<List<BookDTO>> GetBorrowedBooksByMemberAsync(Guid memberId)
        {
            var books = await _transactionRepo.GetBorrowedBooksByMemberAsync(memberId);
            return _mapper.Map<List<BookDTO>>(books);
        }


        public async Task<List<BorrowingTransactionDTO>> SearchAsync(Expression<Func<BorrowingTransactionDTO, bool>> predicate)
        {
            var transactions = await _genericRepo.GetAllAsync();
            var mapped = _mapper.Map<List<BorrowingTransactionDTO>>(transactions);
            return mapped.AsQueryable().Where(predicate).ToList();
        }
    }
}
