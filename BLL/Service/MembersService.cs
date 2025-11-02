using AutoMapper;
using BLL.Interfaces.ICustamRepository;
using BLL.Interfaces.IService;
using BLL.Models;
using BLL.Repository;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class MembersService : IMembersService
    {
        protected readonly IGenericRepository<Member> _genericRepo;
        protected readonly IMembersRepository _memberRepo; 
        protected readonly IMapper _mapper;

        public MembersService(IGenericRepository<Member> genericRepo, IMembersRepository memberRepo, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _memberRepo = memberRepo;
            _mapper = mapper;
        }


        public async Task<bool> AddMemberAsync(MemberDTO entity)
        {
            var member = _mapper.Map<Member>(entity);
            return await _genericRepo.AddAsync(member);
        }

        public async Task<bool> UpdateMemberAsync(MemberDTO entity)
        {
            var member = _mapper.Map<Member>(entity);
            return await _genericRepo.UpdateAsync(member);
        }

        public async Task<bool> DeleteMemberAsync(MemberDTO entity)
        {
            var member = _mapper.Map<Member>(entity);
            return await _genericRepo.DeleteAsync(member);
        }

 
        public async Task<List<MemberDTO>> GetAllMembersAsync(Expression<Func<MemberDTO, bool>>? filter = null)
        {
            var members = await _genericRepo.GetAllAsync();
            var mapped = _mapper.Map<List<MemberDTO>>(members);

            if (filter != null)
                mapped = mapped.AsQueryable().Where(filter).ToList();

            return mapped;
        }

  
        public async Task<MemberDTO?> GetMemberByIdAsync(Guid id)
        {
            var member = await _genericRepo.GetByAsync(m => m.Id == id);
            return _mapper.Map<MemberDTO>(member);
        }

  
        public async Task<MemberDTO?> GetSingleMemberAsync(Expression<Func<MemberDTO, bool>> filter)
        {
            var members = await _genericRepo.GetAllAsync();
            var mapped = _mapper.Map<List<MemberDTO>>(members);
            return mapped.AsQueryable().FirstOrDefault(filter);
        }


        public async Task<bool> CanBorrowAsync(Guid memberId)
        {
            var member = await _genericRepo.GetByAsync(m => m.Id == memberId);
            if (member == null) return false;

            bool hasOutstandingFines = member.BorrowingTransactions.Any(t => t.FineAmount > 0 && !t.IsReturned);
            bool reachedLimit = member.BorrowingTransactions.Count(t => !t.IsReturned) >= 5;

            return !hasOutstandingFines && !reachedLimit;
        }

       
        public async Task<List<BookDTO>> GetBorrowedBooksAsync(Guid memberId)
        {
            var books = await _memberRepo.GetBorrowedBooksAsync(memberId); 
            return _mapper.Map<List<BookDTO>>(books);
        }


        public async Task<MemberDTO> GetMemberProfileWithHistoryAsync(Guid memberId)
        {
            var includeProps = new List<Expression<Func<Member, object>>>
                {
                    m => m.BorrowingTransactions,
                    m => m.ReservationTransactions,
                    
                };

            var member = await _genericRepo.GetByIncludeAsync(
                m => m.Id == memberId,
                includeProps
            );

            if (member == null)
                return null;


            return _mapper.Map<MemberDTO>(member);
        }

        public async Task<decimal> GetOutstandingFinesAsync(Guid memberId)
        {
            var fines = await _memberRepo.GetOutstandingFinesAsync(memberId); 
            return fines;
        }


        public async Task<List<MemberDTO>> FilterMembersAsync(Expression<Func<MemberDTO, bool>> predicate)
        {
            var members = await _genericRepo.GetAllAsync();
            var mapped = _mapper.Map<List<MemberDTO>>(members);
            return mapped.AsQueryable().Where(predicate).ToList();
        }


        public async Task<List<MemberDTO>> SearchMembersAsync(string keyword)
        {
            var members = await _genericRepo.GetAllAsync();
            var mapped = _mapper.Map<List<MemberDTO>>(members);

            return mapped.Where(m =>
                (m.FullName ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                (m.Email ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                (m.Phone ?? "").Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}
