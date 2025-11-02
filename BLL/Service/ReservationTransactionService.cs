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
    public class ReservationTransactionService : IReservationTransactionService
    {
        protected readonly IGenericRepository<ReservationTransaction> _genericRepo;  
        protected readonly IReservationTransactionRepository _reservationRepo;         
        protected readonly IMapper _mapper;

        public ReservationTransactionService(
            IGenericRepository<ReservationTransaction> genericRepo,
            IReservationTransactionRepository reservationRepo,
            IMapper mapper)
        {
            _genericRepo = genericRepo;
            _reservationRepo = reservationRepo;
            _mapper = mapper;
        }

     
        public async Task<bool> AddReservationAsync(ReservationTransactionDTO entity)
        {
            var reservation = _mapper.Map<ReservationTransaction>(entity);
            reservation.ReservationDate = DateTime.UtcNow;
            return await _genericRepo.AddAsync(reservation);
        }


        public async Task<bool> UpdateReservationAsync(ReservationTransactionDTO entity)
        {
            var reservation = _mapper.Map<ReservationTransaction>(entity);
            return await _genericRepo.UpdateAsync(reservation);
        }


        public async Task<bool> DeleteReservationAsync(ReservationTransactionDTO entity)
        {
            var reservation = _mapper.Map<ReservationTransaction>(entity);
            return await _genericRepo.DeleteAsync(reservation);
        }

      
        public async Task<List<ReservationTransactionDTO>> GetAllReservationsAsync(Expression<Func<ReservationTransactionDTO, bool>>? filter = null)
        {
            List<Expression<Func<ReservationTransaction, object>>> includeProperties = new()
                {
        
                    r => r.Book,
                    r => r.Member
                };

  
            var reservations = await _genericRepo.GetAllIncludeAsync(filter: null, includeProperties: includeProperties);

 
            var mapped = _mapper.Map<List<ReservationTransactionDTO>>(reservations);

            if (filter != null)
                mapped = mapped.AsQueryable().Where(filter).ToList();

            return mapped;
        }

        public async Task<ReservationTransactionDTO?> GetReservationByIdAsync(Guid id)
        {

            var includes = new List<Expression<Func<ReservationTransaction, object>>>
            {
                  t => t.Member,
                     t => t.Book
             };

            var transaction = await _genericRepo.GetByIncludeAsync(t => t.Id == id, includes);

            return _mapper.Map<ReservationTransactionDTO>(transaction);
        }

        public async Task<List<ReservationTransactionDTO>> GetReservationsByBookAsync(Guid bookId)
        {
            var reservations = await _reservationRepo.GetReservationsByBookAsync(bookId);
            return _mapper.Map<List<ReservationTransactionDTO>>(reservations);
        }

        public async Task<List<ReservationTransactionDTO>> GetReservationsByMemberAsync(Guid memberId)
        {
            var reservations = await _reservationRepo.GetReservationsByMemberAsync(memberId);
            return _mapper.Map<List<ReservationTransactionDTO>>(reservations);
        }

        public async Task<ReservationTransactionDTO?> GetSingleReservationAsync(Expression<Func<ReservationTransactionDTO, bool>> filter)
        {
            var reservations = await _genericRepo.GetAllAsync();
            var mapped = _mapper.Map<List<ReservationTransactionDTO>>(reservations);
            return mapped.AsQueryable().FirstOrDefault(filter);
        }
        public async Task ReserveBookAsync(Guid bookId, Guid memberId)
        {
            await _reservationRepo.ReserveBookAsync(bookId, memberId);
        }

        public async Task CancelReservationAsync(Guid reservationId)
        {
            await _reservationRepo.CancelReservationAsync(reservationId);
        }

        public async Task ExpireReservationAsync(Guid reservationId)
        {
            await _reservationRepo.ExpireReservationAsync(reservationId);
        }
    }
}
