using APP_BLL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace APP_DAL
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorBook>()
                .HasKey(t => new { t.AuthorId, t.BookId });

            modelBuilder.Entity<AuthorBook>()
                .HasOne(e => e.Author)
                .WithMany(e => e.AuthorBook)
                .HasForeignKey(fk => fk.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(e => e.Book)
                .WithMany(e => e.AuthorBook)
                .HasForeignKey(fk => fk.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<AuthorBook>()
            //    .HasKey(t => new { t.AuthorId, t.BookId });

            //modelBuilder.Entity<AuthorBook>()
            //    .HasOne(a => a.Author)
            //    .WithMany(b => b.AuthorBook)
            //    .HasForeignKey(fk => fk.AuthorId);

            //modelBuilder.Entity<AuthorBook>()
            //    .HasOne(a => a.Book)
            //    .WithMany(b => b.AuthorBook)
            //    .HasForeignKey(fk => fk.BookId);
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory()
          + "/../App_WebApi/appsettings.json").Build();

                var builder = new DbContextOptionsBuilder<AppDbContext>();
                var connectionString = configuration.GetConnectionString("DatabaseConnection");
                builder.UseSqlServer(connectionString);
                return new AppDbContext(builder.Options);
            }
        }
    }
}
