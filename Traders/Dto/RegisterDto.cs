using System.ComponentModel.DataAnnotations;

namespace Traders.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "پر کردن این فیلد الزامی است")]
        public string? UserName { get; set; }
        [Required(ErrorMessage ="پر کردن این فیلد الزامی است")]
        [Display(Name ="ایمیل")]
        [EmailAddress(ErrorMessage ="{0} وارد شده معتبر نمی باشد")]
        public string? Email { get; set; }

    }
}
