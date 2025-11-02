using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.IService
{
    public interface IReservationTransactionService
    {
        Task<List<ReservationTransactionDTO>> GetAllReservationsAsync(Expression<Func<ReservationTransactionDTO, bool>>? filter = null);
        Task<ReservationTransactionDTO?> GetReservationByIdAsync(Guid id);
        Task<ReservationTransactionDTO?> GetSingleReservationAsync(Expression<Func<ReservationTransactionDTO, bool>> filter);
        Task<bool> AddReservationAsync(ReservationTransactionDTO entity);
        Task<bool> UpdateReservationAsync(ReservationTransactionDTO entity);
        Task<bool> DeleteReservationAsync(ReservationTransactionDTO entity);

        Task ReserveBookAsync(Guid bookId, Guid memberId);
        Task CancelReservationAsync(Guid reservationId);
        Task ExpireReservationAsync(Guid reservationId);
        Task<List<ReservationTransactionDTO>> GetReservationsByMemberAsync(Guid memberId);
        Task<List<ReservationTransactionDTO>> GetReservationsByBookAsync(Guid bookId);
    }
}
