using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    /// <summary>
    /// Lớp này chứa thông tin về cơ sở dữ liệu
    /// </summary>
    public class BookStreetContext : DbContext
    {
        public BookStreetContext()
        {
        }

        public BookStreetContext(DbContextOptions<BookStreetContext> options)
            : base(options)
        {
        }

        public DbSet<BookStreet> BookStreets { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthors> BookAuthors { get; set; }
        public DbSet<Kios> Kioses { get; set; }
        public DbSet<KiosStore> KiosStores { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AdminStore> AdminStores { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookAuthors>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<KiosStore>()
                .HasKey(ks => new { ks.KiosId, ks.StoreId });

            modelBuilder.Entity<Stock>()
                .HasKey(s => new { s.BookId, s.StoreId });

            modelBuilder.Entity<BookStreet>()
                .HasKey(b => b.StreetId);

            modelBuilder.Entity<BookStreet>()
                .HasOne(bs => bs.Area)
                .WithMany(a => a.BookStreets)
                .HasForeignKey(bs => bs.AreaId);

            modelBuilder.Entity<Location>()
                .HasKey(l => l.LocationId);

            modelBuilder.Entity<Location>()
                .HasOne(l => l.Store)
                .WithMany(s => s.Locations)
                .HasForeignKey(l => l.StoreId);

            modelBuilder.Entity<Store>()
                .HasKey(s => s.StoreId);

            modelBuilder.Entity<Store>()
                .HasOne(s => s.BookStreet)
                .WithMany(bs => bs.Stores)
                .HasForeignKey(s => s.StreetId);

            modelBuilder.Entity<Book>()
                .HasKey(b => b.BookId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Distributor)
                .WithMany(d => d.Books)
                .HasForeignKey(b => b.DistributorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);

            modelBuilder.Entity<Kios>()
                .HasKey(k => k.KiosId);

            modelBuilder.Entity<Kios>()
                .HasOne(k => k.Area)
                .WithMany(a => a.Kioses)
                .HasForeignKey(k => k.AreaId);

            modelBuilder.Entity<AdminStore>()
                .HasKey(a => a.AdminStoreId);

            modelBuilder.Entity<AdminStore>()
                .HasOne(a => a.Store)
                .WithMany(s => s.AdminStores)
                .HasForeignKey(a => a.StoreId);
        }
    }
}
