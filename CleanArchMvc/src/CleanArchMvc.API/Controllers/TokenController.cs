using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate;
            _configuration = configuration;
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
        {
            var result = await _authenticate.Authenticate(loginModel.Email, loginModel.Password);

            if (result)
            {
                return GenereteToken(loginModel);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt!");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] LoginModel loginModel)
        {
            var result = await _authenticate.RegisterUser(loginModel.Email, loginModel.Password);

            if (result)
            {
                return Ok($"User {loginModel.Email} was created successfully!");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt!");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenereteToken(LoginModel loginModel)
        {
            //declarações do usuário
            var claims = new[]
            {
                new Claim("email",loginModel.Email),
                new Claim("meuvalor","valor qualquer"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            //gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //gerar a assinatura digital
            var credencials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração
            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new(
                //Emissor
                issuer: _configuration["Jwt:Issuer"],
                //Audiência
                audience: _configuration["Jwt:Audience"],
                //Claims
                claims: claims,
                //Data de expiração
                expires: expiration,
                //Assinatura Digital
                signingCredentials: credencials
                );

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}