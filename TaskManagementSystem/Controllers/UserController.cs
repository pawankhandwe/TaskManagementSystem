using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Model;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register registerModel)
        {
           
                var user = new ApplicationUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Username // Using username as email in this example
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    return Ok("Registration successful!");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            

            return BadRequest("Invalid registration data.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginModel.Username);

                var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return Ok("Login Successfull");
                }

                return BadRequest("Invalid login attempt.");
            }

            return BadRequest("Invalid Model");
        }
        [HttpPost("logout")] // Requires the user to be authenticated
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            // Clear the existing authentication cookies
         
            return Ok("Logged out successfully.");
        }
    }
}
