using DAL.Entities;
using DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Database
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            //// ===== Categories =====
            //var cat1Id = Guid.NewGuid();
            //var cat2Id = Guid.NewGuid();
            //var cat3Id = Guid.NewGuid();
            //var cat4Id = Guid.NewGuid();
            //var cat5Id = Guid.NewGuid();

            //modelBuilder.Entity<Category>().ToTable("Categories")
            //    .HasData(
            //        new Category { Id = cat1Id, CategoryName = "Fiction", Description = "Novels and stories", IsActive = true, CreatedOn = DateTime.Now },
            //        new Category { Id = cat2Id, CategoryName = "Science", Description = "Science books", IsActive = true, CreatedOn = DateTime.Now },
            //        new Category { Id = cat3Id, CategoryName = "History", Description = "Historical books", IsActive = true, CreatedOn = DateTime.Now },
            //        new Category { Id = cat4Id, CategoryName = "Technology", Description = "Tech & Programming", IsActive = true, CreatedOn = DateTime.Now },
            //        new Category { Id = cat5Id, CategoryName = "Children", Description = "Kids books", IsActive = true, CreatedOn = DateTime.Now }
            //    );

            //// ===== Books =====
            //var bookIds = new Guid[10];
            //for (int i = 0; i < 10; i++) bookIds[i] = Guid.NewGuid();

            //modelBuilder.Entity<Book>().ToTable("Books")
            //    .HasData(
            //        new Book { Id = bookIds[0], Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", CategoryId = cat1Id, ISBN = "9780743273565", PublishedYear = 1925, Rating = 5, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now },
            //        new Book { Id = bookIds[1], Title = "A Brief History of Time", Author = "Stephen Hawking", CategoryId = cat2Id, ISBN = "9780553380163", PublishedYear = 1988, Rating = 4, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now },
            //        new Book { Id = bookIds[2], Title = "Sapiens", Author = "Yuval Noah Harari", CategoryId = cat3Id, ISBN = "9780062316097", PublishedYear = 2011, Rating = 5, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now },
            //        new Book { Id = bookIds[3], Title = "Clean Code", Author = "Robert C. Martin", CategoryId = cat4Id, ISBN = "9780132350884", PublishedYear = 2008, Rating = 5, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now },
            //        new Book { Id = bookIds[4], Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling", CategoryId = cat5Id, ISBN = "9780590353427", PublishedYear = 1997, Rating = 5, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now },
            //        new Book { Id = bookIds[5], Title = "The Alchemist", Author = "Paulo Coelho", CategoryId = cat1Id, ISBN = "9780061122415", PublishedYear = 1988, Rating = 4, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now },
            //        new Book { Id = bookIds[6], Title = "Introduction to Algorithms", Author = "Cormen et al.", CategoryId = cat4Id, ISBN = "9780262033848", PublishedYear = 2009, Rating = 5, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now },
            //        new Book { Id = bookIds[7], Title = "The Origin of Species", Author = "Charles Darwin", CategoryId = cat2Id, ISBN = "9781509827695", PublishedYear = 1859, Rating = 5, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now },
            //        new Book { Id = bookIds[8], Title = "World History", Author = "H.G. Wells", CategoryId = cat3Id, ISBN = "9780679405365", PublishedYear = 1922, Rating = 4, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now },
            //        new Book { Id = bookIds[9], Title = "Charlie and the Chocolate Factory", Author = "Roald Dahl", CategoryId = cat5Id, ISBN = "9780142410318", PublishedYear = 1964, Rating = 5, IsAvailable = true, IsActive = true, CreatedOn = DateTime.Now }
            //    );

        




     


        }
    }
}
