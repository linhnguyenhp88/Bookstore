using BookShop.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Domain.DbContexts
{
    public class BookShopContext :
         IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public BookShopContext(DbContextOptions<BookShopContext> options)
         : base(options)
        { }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<DeliveryBase> Deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Author>(b =>
            {
                b.HasKey(a => new { a.Id });
                b.ToTable("Deliveries");
            });

            builder.Entity<Author>(b =>
            {
                b.HasKey(a => new { a.Id });              
                b.ToTable("Authors");
            });

            builder.Entity<Photo>(b =>
            {
                b.HasKey(p => new { p.Id });
                b.HasOne(p => p.Book).WithMany(x => x.Photos).HasForeignKey(m => m.BookId);
                b.ToTable("Photos");
            });

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

                userRole.ToTable("User_Role");
            });

            builder.Entity<User>()
               .ToTable("Users");

            builder.Entity<Role>()
               .ToTable("Roles");

            builder.Entity<IdentityRoleClaim<int>>(b =>
            {
                b.ToTable("Role_Claim");
            });

            builder.Entity<IdentityUserClaim<int>>(b =>
            {
                b.ToTable("User_Claim");
            });         
        }
    }
}
