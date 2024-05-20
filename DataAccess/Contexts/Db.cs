using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public Db(DbContextOptions options) : base(options) // super in Java
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region String Property Maximum Lengths
            modelBuilder.Entity<Book>().Property(b => b.Name).HasMaxLength(100);
            modelBuilder.Entity<Book>().Property(b => b.Isbn).HasMaxLength(13);

            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(50);
            modelBuilder.Entity<Author>().Property(a => a.Surname).HasMaxLength(50);

            modelBuilder.Entity<Genre>().Property(a => a.Name).HasMaxLength(30);
            #endregion

            #region Relationships
            modelBuilder.Entity<Author>().HasMany(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookGenre>().HasOne(bg => bg.Book).WithMany(b => b.BookGenres).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BookGenre>().HasOne(bg => bg.Genre).WithMany(g => g.BookGenres).OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Unique Indices
            modelBuilder.Entity<BookGenre>().HasIndex(bg => new { bg.BookId, bg.GenreId }).IsUnique();
            #endregion
        }
    }
}
