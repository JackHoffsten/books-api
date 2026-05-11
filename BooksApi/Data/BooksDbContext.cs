using Microsoft.EntityFrameworkCore;
using BooksApi.Models;

namespace BooksApi.Data
{
  public class BooksDbContext : DbContext
  {
    public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Quote> Quotes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // User
      modelBuilder.Entity<User>()
          .HasKey(u => u.Id);

      modelBuilder.Entity<User>()
          .HasMany(u => u.Books)
          .WithOne(b => b.User)
          .HasForeignKey(b => b.UserId)
          .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<User>()
          .HasMany(u => u.Quotes)
          .WithOne(q => q.User)
          .HasForeignKey(q => q.UserId)
          .OnDelete(DeleteBehavior.Cascade);

      // Book
      modelBuilder.Entity<Book>()
          .HasKey(b => b.Id);

      modelBuilder.Entity<Book>()
          .Property(b => b.Title)
          .IsRequired()
          .HasMaxLength(200);

      modelBuilder.Entity<Book>()
          .Property(b => b.Author)
          .IsRequired()
          .HasMaxLength(200);

      // Quote
      modelBuilder.Entity<Quote>()
          .HasKey(q => q.Id);

      modelBuilder.Entity<Quote>()
          .Property(q => q.Text)
          .IsRequired()
          .HasMaxLength(1000);

      modelBuilder.Entity<Quote>()
          .Property(q => q.Author)
          .IsRequired()
          .HasMaxLength(200);
    }
  }
}
