using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPierre.Data;
using ProjectPierre.DTO.UserDTOs;
using ProjectPierre.Interfaces;
using ProjectPierre.Mappers;
using ProjectPierre.Models;
using ProjectPierre.Services;

namespace ProjectPierre.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ICartRepository _cartRepo;

        public UserController(DataContext context, UserManager<User> userManager, ICartRepository cartRepo, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _cartRepo = cartRepo;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDTO userRegisterDTO)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new User
                {
                    UserName = userRegisterDTO.Username,
                    Email = userRegisterDTO.Email,
                };

                var newUser = await _userManager.CreateAsync(user, userRegisterDTO.Password);

                if (newUser.Succeeded)
                {
                    var newRole = await _userManager.AddToRoleAsync(user, "User");
                    
                    if (newRole.Succeeded)
                    {
                        await _cartRepo.CreateCartAsync(user.Id);

                        return Ok(
                            new NewUserDTO
                            {
                                UserName = user.UserName,
                                Email = user.Email
                            }
                        );
                    }

                    return StatusCode(500, newRole.Errors);
                }

                return StatusCode(500, newUser.Errors);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginDTO.UserName.ToLower());

            if (user == null)
            {
                return Unauthorized("That username is invalid.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            user.RefreshToken = refreshToken.Token;
            user.TokenCreateDate = refreshToken.Created;
            user.TokenExpirationDate = refreshToken.Expires;

            await _context.SaveChangesAsync();

            return Ok(user.ToLoggedInUserDTOFromUser(token));
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var requestRefreshToken = Request.Cookies["refreshToken"];
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == requestRefreshToken);

            if (user == null)
            {
                return Unauthorized("Invalid refresh token.");
            }

            if (user.TokenExpirationDate < DateTime.Now)
            {
                return Unauthorized("Refresh token has expired. Please login again.");
            }

            var token = _tokenService.CreateToken(user);
            return Ok(token);
        }
    }
}
