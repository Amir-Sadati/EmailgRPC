using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Traders.Dto;
using Traders.gRPCservice;

namespace Traders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MailgRPCservice _mailgRP;

        public AccountController(UserManager<IdentityUser> userManager,
            MailgRPCservice mailgRP)
        {
            _userManager = userManager;
            _mailgRP = mailgRP;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            //We can use repository or other pattern and use
            //Seprate Methood but its demo. so I write all
            //code Here :D

            if (_userManager.Users.Any(x => x.UserName == model.UserName))
                return BadRequest("نام کاربری شما توسط فر دیگری انتخاب شده است");
           var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
           var result=await _userManager.CreateAsync(user);
            if (!result.Succeeded)
               return BadRequest();

           var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _mailgRP.SendEmail(model.Email, code);

            return Ok("yes");
        }
    }
}
