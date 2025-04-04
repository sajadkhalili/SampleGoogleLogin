using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

namespace SampleGoogleLogin;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();



        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme=CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme=GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {

                options.ClientId=builder.Configuration["Authentication:Google:ClientId"];
                options.ClientSecret=builder.Configuration["Authentication:Google:ClientSecret"];

                //options.ClientId="62999589548-ivd24os9a10qnjfsn91gb72dt56mog93.apps.googleusercontent.com";
                //options.ClientSecret="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";

                options.CallbackPath=new PathString("/signin-google"); // این خط باید وجود داشته باشد
                options.Scope.Add("openid");
        
                options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
                options.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
                //options.Scope.Add("https://www.googleapis.com/auth/user.phonenumbers.read");
                //options.Scope.Add("https://www.googleapis.com/auth/user.addresses.read");
                //options.Scope.Add("https://www.googleapis.com/auth/user.birthday.read");

                //options.Scope.Add("https://www.googleapis.com/auth/calendar.readonly"); // دسترسی به تقویم
                //options.Scope.Add("https://www.googleapis.com/auth/drive.readonly"); // دسترسی به Google Drive
                // options.Scope.Add("https://www.googleapis.com/auth/user.gender.read"); // دسترسی به جنسیت
                


                options.SaveTokens=true; // ذخیره توکن‌ها برای استفاده بعدی
            });



        builder.Services.AddDbContext<UserContext>(op => op.UseSqlServer(
            builder.Configuration.GetConnectionString("UserDB")
        ));
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
