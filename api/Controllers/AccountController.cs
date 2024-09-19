using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController :ControllerBase
    {   
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody] RegisterDto registerDtp)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                var appUser = new AppUser
                {
                    UserName = registerDtp.UserName,
                    Email = registerDtp.Email
                };
                var createUser = await _userManager.CreateAsync(appUser, registerDtp.Password);
                if(createUser.Succeeded)
                {
                    IdentityResult result = new IdentityResult();
                    if(!registerDtp.IsAdmin)
                    {
                         result = await _userManager.AddToRoleAsync(appUser, "User");
                    }
                    else
                    {
                        result = await _userManager.AddToRoleAsync(appUser, "Admin");
                    }
                    if(result.Succeeded)
                    {
                        return Ok(
                            new NewUserDto{
                                UserName = appUser.UserName,
                                Email  = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500,result.Errors);
                    }
                }
                else
                {
                     return StatusCode(500,createUser.Errors);
                }
            }
            catch(Exception ex)
            {
                 return StatusCode(500, ex);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if(user == null) return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user,loginDto.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserDto{
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
    }
}