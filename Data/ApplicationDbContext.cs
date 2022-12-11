using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using Microsoft.AspNetCore.Identity;
using TrainingRazor.Models;

namespace TrainingRazor.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private ApplicationUser saUserAdmin { get; set; }
    private ApplicationUser saUserCustomer { get; set; }
    private IdentityRole role1 { get; set; }
    private IdentityRole role2 { get; set;}
    private IdentityUserRole<string> userAdmin { get; set; }
    private IdentityUserRole<string> userCustomer { get; set; }
    private PasswordHasher<ApplicationUser> passwordHasher { get; set; }
    
    public DbSet<CustPurchased> CustPurchaseds { get; set; }
    public DbSet<Product> Products { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)  
    {  
        base.OnModelCreating(builder);
        this.SeedUsers(builder);
        this.SeedProducts(builder);
    }  

    private void SeedUsers(ModelBuilder builder)  
    {
        string saUsernameAdmin = "admin@razorpage.com";
        string saUsernameCustomer = "customer@razorpage.com";

        //YOU CAN ADD MORE ROLE AS YOU WANT
        role1 = new IdentityRole() 
        { 
            Id = Guid.NewGuid().ToString(), 
            Name = "SystemAdmin", 
            ConcurrencyStamp = Guid.NewGuid().ToString(), 
            NormalizedName = "SYSTEMADMIN" 
        };

        role2 = new IdentityRole() 
        { 
            Id = Guid.NewGuid().ToString(), 
            Name = "Customer", 
            ConcurrencyStamp = Guid.NewGuid().ToString(), 
            NormalizedName = "CUSTOMER" 
        };

        //ADD USER ADMIN
        saUserAdmin = new ApplicationUser()  
        {  
            Id = Guid.NewGuid().ToString(),
            UserName = saUsernameAdmin,  
            Email = saUsernameAdmin,
            NormalizedEmail = saUsernameAdmin.ToUpper(),
            NormalizedUserName = saUsernameAdmin.ToUpper(),
            Name = "System Admin",
            LockoutEnabled = false,  
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true,
        };

        //ADD USER CUSTOMER
        saUserCustomer = new ApplicationUser()  
        {  
            Id = Guid.NewGuid().ToString(),
            UserName = saUsernameCustomer,  
            Email = saUsernameCustomer,
            NormalizedEmail = saUsernameCustomer.ToUpper(),
            NormalizedUserName = saUsernameCustomer.ToUpper(),
            Name = "Customer 1",
            LockoutEnabled = false,  
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true,
        };
        
        passwordHasher = new PasswordHasher<ApplicationUser>(); 

        var saPwdHashed1 = passwordHasher.HashPassword(saUserAdmin, "Abcd123!");  
        saUserAdmin.PasswordHash = saPwdHashed1;

        var saPwdHashed2 = passwordHasher.HashPassword(saUserCustomer, "Abcd123!");  
        saUserCustomer.PasswordHash = saPwdHashed2;
        
        userAdmin = new IdentityUserRole<string>() 
        { 
            RoleId = role1.Id,
            UserId = saUserAdmin.Id
        };

        userCustomer = new IdentityUserRole<string>() 
        { 
            RoleId = role2.Id,
            UserId = saUserCustomer.Id
        };


        //BUT MAKE SURE ADD THE ROLE INSIDE IDENTITY ROLE LIKE THIS IF YOU WANT TO ADD MORE ROLE
        builder.Entity<IdentityRole>().HasData(role1);
        builder.Entity<IdentityRole>().HasData(role2);
        builder.Entity<ApplicationUser>().HasData(saUserAdmin);
        builder.Entity<ApplicationUser>().HasData(saUserCustomer);  
        builder.Entity<IdentityUserRole<string>>().HasData(userAdmin);
        builder.Entity<IdentityUserRole<string>>().HasData(userCustomer);
    } 

    private void SeedProducts(ModelBuilder builder)  
    {
        builder.Entity<Product>().HasData( 
                new Product() { Id = 1, Name = "Logitech MX Mouse", Price = 345.12M },
                new Product() { Id = 2, Name = "Keycron Keyboard", Price = 295.23M },
                new Product() { Id = 3, Name = "Razer Lapotop", Price = 7345.69M },
                new Product() { Id = 4, Name = "Iphone 14 Pro Max", Price = 8798.12M },
                new Product() { Id = 5, Name = "Xiaomi Ear Buds", Price = 45.46M }
            );
    }
}
