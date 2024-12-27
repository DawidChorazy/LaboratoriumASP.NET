using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        string ADMIN_ID = Guid.NewGuid().ToString();
        string ROLE_ID = Guid.NewGuid().ToString();
        string USER_ID = Guid.NewGuid().ToString();
        string ROLE_USER_ID = Guid.NewGuid().ToString();

        // dodanie roli administratora
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "admin",
            NormalizedName = "ADMIN",
            Id = ROLE_ID,
            ConcurrencyStamp = ROLE_ID
        });
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "user",
            NormalizedName = "USER",
            Id = ROLE_USER_ID,
            ConcurrencyStamp = ROLE_USER_ID
        });

        // utworzenie administratora jako użytkownika
        var admin = new IdentityUser
        {
            Id = ADMIN_ID,
            Email = "adam@wsei.edu.pl",
            EmailConfirmed = true,
            UserName = "adam",
            NormalizedUserName = "ADMIN",
            NormalizedEmail = "ADAM@WSEI.EDU.PL"
        };

        // haszowanie hasła
        PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
        admin.PasswordHash = ph.HashPassword(admin, "1234abcd!@#$ABCD");

        // zapisanie użytkownika
        modelBuilder.Entity<IdentityUser>().HasData(admin);

        // przypisanie roli administratora użytkownikowi
        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

        var user = new IdentityUser
        {
            Id = USER_ID,
            Email = "jan@wsei.edu.pl",
            EmailConfirmed = true,
            UserName = "jan",
            NormalizedUserName = "USER",
            NormalizedEmail = "JAN@WSEI.EDU.PL"
        };

        user.PasswordHash = ph.HashPassword(user, "5678efgh!@#$EFGH");

        modelBuilder.Entity<IdentityUser>().HasData(user);

        modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_USER_ID,
                UserId = USER_ID
            });

        modelBuilder.Entity<ContactEntity>()
            .HasOne(e => e.Organization)
            .WithMany(o => o.Contacts)
            .HasForeignKey(e => e.OrganizationId);

        modelBuilder.Entity<ContactEntity>()
            .Property(e => e.OrganizationId)
            .HasDefaultValue(101);

        modelBuilder.Entity<ContactEntity>()
            .Property(e => e.Created)
            .HasDefaultValue(DateTime.Now);

        modelBuilder.Entity<OrganizationEntity>()
            .ToTable("organizations")
            .HasData(
                new OrganizationEntity()
                {
                    Id = 101,
                    Title = "WSEI",
                    Nip = "83492384",
                    Regon = "13424234",
                },
                new OrganizationEntity()
                {
                    Id = 102,
                    Title = "Firma",
                    Nip = "2498534",
                    Regon = "0873439249",
                }
            );

        modelBuilder.Entity<ContactEntity>().HasData(
            new ContactEntity()
            {
                Id = 1,
                Name = "AA",
                Email = "Adam",
                Phone = "13424234",
                OrganizationId = 101,
            },
            new ContactEntity()
            {
                Id = 2,
                Name = "C#",
                Email = "Ewa",
                Phone = "02879283",
                OrganizationId = 102,
            }
        );

        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(e => e.Address)
            .HasData(
                new { OrganizationEntityId = 101, City = "Kraków", Street = "Św. Filipa 17", PostalCode = "31-150", Region = "małopolskie" },
                new { OrganizationEntityId = 102, City = "Kraków", Street = "Krowoderska 45/6", PostalCode = "31-150", Region = "małopolskie" }
            );
    }
}
