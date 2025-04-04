using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SampleGoogleLogin.Controllers;


//class MyClass:Controller
//{
//    public async Task<IActionResult> Login ()
//    {
//        var redirectUrl = Url.Action("GoogleCallback", "Account");
//        var properties = new AuthenticationProperties { RedirectUri=redirectUrl };
//        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
//    }

//    // Google Callback (Handle Response from Google)
//    [Route("GoogleCallback")]
//    public async Task<IActionResult> GoogleCallback ()
//    {
//        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

//        if (result?.Principal==null)
//            return RedirectToAction("Login");

//        // Extract user claims (e.g., Name, Email)
//        var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
//        var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;

//        // Store tokens in UserToken table (example implementation)
//        var token = await HttpContext.GetTokenAsync("access_token");
//        var user = await _userManager.FindByEmailAsync(email);
//        if (user==null)
//        {
//            user=new ApplicationUser
//            {
//                UserName=email,
//                Email=email,
//                EmailConfirmed=true
//            };
//            await _userManager.CreateAsync(user);
//        }

//        await _dbContext.UserTokens.AddAsync(new UserToken
//        {
//            UserId=user.Id,
//            LoginProvider="Google",
//            Name="AccessToken",
//            Value=token
//        });
//        await _dbContext.SaveChangesAsync();

//        return RedirectToAction("Profile");
//    }

//}



public class AccountController : Controller
{
    // لاگین با گوگل
    public IActionResult Login()
    {
        var redirectUrl = Url.Action("signin-google", "Account");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }


    // لاگ‌اوت
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    // صفحه‌ی پروفایل کاربر
    [Authorize]
    [Route("{Controller}/signin-google")]
    public async Task<IActionResult> Profile ()
    {

        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        var claims2 = result.Principal.Identities
            .FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

        //return Json(claims2);

        // دریافت اطلاعات کاربر از کوکی‌ها
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var claims = User.Claims;

        var name = claims.FirstOrDefault(c => c.Type==ClaimTypes.Name)?.Value;
        var email = claims.FirstOrDefault(c => c.Type==ClaimTypes.Email)?.Value;
        var picture = claims.FirstOrDefault(c => c.Type=="picture")?.Value;

        var userInfo = new
        {
            Name = name,
            Email = email,
            Picture = picture
        };
        return View(userInfo);
    }

    //[Route("{Controller}/signin-google")]
    //public async Task<IActionResult> GetUserProfile()
    //{
    //    // Retrieve the access token from the authentication properties
    //    var accessToken = await HttpContext.GetTokenAsync("access_token");

    //    // Create an HttpClient instance
    //    using var client = new HttpClient();

    //    // Set the Authorization header with the access token
    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    //    // Make the GET request to the Google People API
    //    var response =
    //        await client.GetAsync(
    //            "https://people.googleapis.com/v1/people/me?personFields=names,emailAddresses,photos,phoneNumbers,addresses,birthdays");

    //    // Ensure the request was successful
    //    response.EnsureSuccessStatusCode();

    //    // Read the response content as a string
    //    var responseContent = await response.Content.ReadAsStringAsync();

    //    // Parse the JSON response (optional)
    //    var userProfile = JsonConvert.DeserializeObject<UserProfile>(responseContent);

    //    // Return the response or process it further
    //    return Ok(userProfile);
    //}


    public IActionResult LoginNationalCode ()
    {
        return View();
    }


    public IActionResult Index()
    {
        return View();
    }
}

// Example class to represent the user profile
public class UserProfile
    {
        public List<Name> Names { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; }
        public List<Photo> Photos { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Birthday> Birthdays { get; set; }
    }

    // Example classes for the response fields
    public class Name
    {
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }

    public class EmailAddress
    {
        public string Value { get; set; }
    }

    public class Photo
    {
        public string Url { get; set; }
    }

    public class PhoneNumber
    {
        public string Value { get; set; }
    }

    public class Address
    {
        public string FormattedValue { get; set; }
    }

    public class Birthday
    {
        public Date Date { get; set; }
    }

    public class Date
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
