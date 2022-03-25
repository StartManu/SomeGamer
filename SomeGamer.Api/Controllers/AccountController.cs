using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SomeGamer.Core.Entity;
using SomeGamer.Core.Entity.Account;
using SomeGamer.Data.Context;
using SomeGamer.Data.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SomeGamer.Api.Controllers
{ //teste 
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SomeGamerDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration configuration,
            SomeGamerDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }//teste

        [HttpGet("Get")] 
        public ActionResult<string> Get()
        {
            return "<<Controlador AccountController :: WebApiUsuarios>>";
        }
        //POST api/Account/CriarUser
        [AllowAnonymous]
        [HttpPost("CriarUser")]
        public async Task<ActionResult> Create([FromBody] Login login)
        {
            var usuario = new User { UserName = login.Email, Email = login.Email };
            //add aqui uma regra para verificar se o user já existe
            //Verificar se o email já existe
            var resultado = await _userManager.CreateAsync(usuario, login.Password);

            if (resultado.Succeeded)
            {
                return Ok(new { Message = "Usuário Registrado com sucesso" });
            }
            else
            {
                return BadRequest("Alguma coisa deu errado socorro");
            }
        }
        //POST api/Account/Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] Login contaDeUsusario)
        {
            //Verificar se o email já existe, se não existe retornar q o email não existe
            var resultado = await _signInManager.PasswordSignInAsync(contaDeUsusario.Email, contaDeUsusario.Password,
                isPersistent: false, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                var token = TokenUsuario(contaDeUsusario);
                return Ok(new { Token = token, Message = "Sucesso" });
            }
            else
            {
                //Add aq alguma logica para retornar o erro
                ModelState.AddModelError(string.Empty, "login falhou");
                return BadRequest(ModelState);
            }

        }
        //POST api/Account/Logout
        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                //Remove o token aq tmb
                
                await _signInManager.SignOutAsync();
                return Ok();
            }
                        
        }
        // POST api/Account/ChangePassword
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword(Login login,ChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.Usuarios.FindAsync(login);
            if(user == null)
            {
                return BadRequest("Usuario não foi encontrado, verifique o login");
            }
            IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok();
            }


        }
        private object TokenUsuario(Login contaDeUsusario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, contaDeUsusario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var chaveJwt = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credenciais = new SigningCredentials(chaveJwt, SecurityAlgorithms.HmacSha256);

            var expiracaoDoToken = DateTime.UtcNow.AddMinutes(30);

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiracaoDoToken,
                    signingCredentials: credenciais
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
