using BLL.Interfaces.ICustamRepository;
using BLL.Repository;
using DAL.DataBase;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

public class ReservationTransactionRepository : GenericRepository<ReservationTransaction>, IReservationTransactionRepository
{
    public ReservationTransactionRepository(ApplicationContext db) : base(db) { }

    public async Task ReserveBookAsync(Guid bookId, Guid memberId)
    {
        var existingReservation = await _db.ReservationTransactions
            .FirstOrDefaultAsync(r => r.BookId == bookId && r.MemberId == memberId && r.IsActive);

        if (existingReservation != null)
            throw new Exception("This member has already reserved this book.");

        var reservation = new ReservationTransaction
        {
            Id = Guid.NewGuid(),
            BookId = bookId,
            MemberId = memberId,
            ReservationDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(2),
            IsActive = true
        };

        await _dbSet.AddAsync(reservation);

        var book = await _db.Books.FindAsync(bookId);
        if (book != null)
        {
            book.IsAvailable = false;
            _db.Books.Update(book);
        }

        await _db.SaveChangesAsync();
    }

    public async Task CancelReservationAsync(Guid reservationId)
    {
        var reservation = await _dbSet.FindAsync(reservationId);
        if (reservation != null)
        {
            reservation.IsActive = false;
            _dbSet.Update(reservation);

            var book = await _db.Books.FindAsync(reservation.BookId);
            if (book != null)
            {
                book.IsAvailable = true;
                _db.Books.Update(book);
            }

            await _db.SaveChangesAsync();
        }
    }

    public async Task ExpireReservationAsync(Guid reservationId)
    {
        var reservation = await _dbSet.FindAsync(reservationId);
        if (reservation != null && reservation.DueDate < DateTime.Now)
        {
            reservation.IsActive = false;
            _dbSet.Update(reservation);

            var book = await _db.Books.FindAsync(reservation.BookId);
            if (book != null)
            {
                book.IsAvailable = true;
                _db.Books.Update(book);
            }

            await _db.SaveChangesAsync();
        }
    }

    public async Task<List<ReservationTransaction>> GetReservationsByBookAsync(Guid bookId)
    {
        return await _dbSet
            .Where(r => r.BookId == bookId && r.IsActive)
            .Include(r => r.Book)
            .Include(r => r.Member)
            .ToListAsync();
    }

    public async Task<List<ReservationTransaction>> GetReservationsByMemberAsync(Guid memberId)
    {
        return await _dbSet
            .Where(r => r.MemberId == memberId && r.IsActive)
            .Include(r => r.Book)
            .ToListAsync();
    }
}
