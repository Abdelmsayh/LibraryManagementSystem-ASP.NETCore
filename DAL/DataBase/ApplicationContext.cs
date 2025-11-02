using DAL.Entities;
using DAL.Database;
using DAL.Extend;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataBase
{
    public partial class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> ops) : base(ops) { }
    }

    public partial class ApplicationContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowingTransaction> BorrowingTransactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<ReservationTransaction> ReservationTransactions { get; set; }


        //public DbSet<Role> Roles { get; set; }
        //public DbSet<UserInRole> UserInRoles { get; set; }
        //public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DbInitializer(modelBuilder).Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
