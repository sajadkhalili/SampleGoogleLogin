using System.Security.Principal;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SampleGoogleLogin.Controllers;

namespace SampleGoogleLogin.Controllers
{


    public class User 

    {
        public long UserId { get; set; }
        //public  string UserName { get; set; }
        //public  string NormalizedUserName { get; set; }
        public  string Email { get; set; }
        public  string NormalizedEmail { get; set; }
        //public  bool EmailConfirmed { get; set; }
        //public  string PasswordHash { get; set; }
        //public  string SecurityStamp { get; set; }
        //public  string ConcurrencyStamp { get; set; }
        public  string PhoneNumber { get; set; }
        //public  bool PhoneNumberConfirmed { get; set; }
        //public  bool TwoFactorEnabled { get; set; }
        public  DateTimeOffset? LockoutEnd { get; set; }
        public  bool LockoutEnabled { get; set; }
        public  int AccessFailedCount { get; set; }
    }

}
public class UserLogin
{

    //LoginProvider: نام ارائه‌دهنده ورود ( مثلاً Google یا Facebook ).

    //ProviderKey: کلید یکتا از طرف ارائه‌دهنده ( مثلاً شناسه کاربر در گوگل).

    //ProviderDisplayName: نام نمایشی ارائه‌دهنده ( اختیاری، مثلاً "Google").

    //UserId: شناسه کاربر در برنامه شما.

    public  long UserLoginId { get; set; }
    public  long UserId { get; set; }
    public  string LoginProvider { get; set; }
    public  string ProviderKey { get; set; }
    public  string ProviderDisplayName { get; set; }
}



public class UserToken
{
    public long UserTokenId { get; set; }
    public  long UserId { get; set; }
    public  string LoginProvider { get; set; }
    public  string Name { get; set; }
    public  string Value { get; set; }
}



public class UserContext : DbContext
{
    public UserContext(DbContextOptions options):base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
}