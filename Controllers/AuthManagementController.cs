using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using core_rest_api.Configuration;
using Microsoft.Extensions.Options;
using core_rest_api.Models.DTOs.Requests;
using core_rest_api.Models.DTOs.Responses;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;


namespace core_rest_api.Controllers{
    [Route("[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user){
            if(ModelState.IsValid){
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if(existingUser != null){
                    return BadRequest();
                }

                var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if(isCreated.Succeeded){
                    var jwtToken = GenerateJwtToken(newUser);

                    return Ok(new RegistrationResponse(){
                        Success = true,
                        Token = jwtToken
                    });
                } else {
                    return BadRequest(new RegistrationResponse(){
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            } else {
                return BadRequest(new RegistrationResponse(){
                    Errors = new List<string>() {
                        "Invalid payload"
                    },
                    Success = false
                });
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if(existingUser == null)
                {
                    return BadRequest(new RegistrationResponse(){
                        Errors = new List<string>() {
                            "Invalid login request"
                        },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if(!isCorrect)
                {
                    return BadRequest(new RegistrationResponse(){
                        Errors = new List<string>() {
                            "Invalid login request"
                        },
                        Success = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new RegistrationResponse(){
                    Success = true,
                    Token = jwtToken
                });
            } 

            return BadRequest(new RegistrationResponse(){
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Success = false
            });
        }

        private string GenerateJwtToken(IdentityUser user){
            var JwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new []{
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = JwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = JwtTokenHandler.WriteToken(token);

            return jwtToken;
        }




    }
}