using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.ICustamRepository
{
    public interface IReservationTransactionRepository
    {

        Task ReserveBookAsync(Guid bookId, Guid memberId);
        Task CancelReservationAsync(Guid reservationId);
        Task ExpireReservationAsync(Guid reservationId);
        Task<List<ReservationTransaction>> GetReservationsByMemberAsync(Guid memberId);
        Task<List<ReservationTransaction>> GetReservationsByBookAsync(Guid bookId);
    }
}
