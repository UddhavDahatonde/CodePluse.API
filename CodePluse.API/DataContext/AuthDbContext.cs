using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.DataContext
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "8021c6e8-09ea-40ff-adfc-07700b03cb9b";
            var writerRoleId = "9d68c05f-ffcc-469f-b73b-5fe11b6c692f";
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id=readerRoleId,
                    Name="Reader",
                    ConcurrencyStamp=readerRoleId,
                    NormalizedName=readerRoleId.ToUpper()
                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name="Writer",
                    ConcurrencyStamp=writerRoleId,
                    NormalizedName=writerRoleId.ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            var adminUserId = "41063965-7cbf-478c-82bd-90137e691272";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "uddhav@gmail.com",
                NormalizedEmail = "uddhav@gmail.com".ToUpper(),
                NormalizedUserName = "uddhav@gmail.com".ToUpper()
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Uddhav@123");
            builder.Entity<IdentityUser>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId=adminUserId,
                    RoleId=readerRoleId
                },
                new()
                {
                    UserId=adminUserId,
                    RoleId=writerRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
