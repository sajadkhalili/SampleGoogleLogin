using System.ComponentModel.DataAnnotations;

namespace SampleGoogleLogin.Models;

public class LoginModel
{
    [Display(Name = "کد ملی")]
    [Required (ErrorMessage = "اجباری ایست")]
    public string NationalCode { get; set; }

    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = "اجباری ایست")]
    public string PhoneNumber { get; set; }
}
