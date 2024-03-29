﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
{ //a gente vai gabaritar
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
        }

        [HttpGet("Get")]
        public ActionResult<string> Get()
        {
            return "<<Controlador AccountController :: WebApiUsuarios>>";
        }
        //POST api/Account/CriarUser
        //[AllowAnonymous]
        [HttpPost("CriarUser")]
        public async Task<ActionResult> Create([FromBody] Person person)
        {
            var usuario = new User { UserName = person.Login.Email, Email = person.Login.Email };
            var resultado = await _userManager.CreateAsync(usuario, person.Login.Password);
            if (resultado.Succeeded)
            {
                usuario.Person = new Person()
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    DateBirth = person.DateBirth,
                    Login = person.Login
                };
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest("Alguma coisa deu errado socorro");
            }
            //try
            //{

            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);
            //}
            //_context.People.Add(person);
            //await _context.SaveChangesAsync();
            //return Ok(person);

            //if (person == null)
            //{
            //    return BadRequest("O usuario não pode ser null");
            //}
            //else
            //{
            //    var usuario = new User { UserName = person.Login.Email, Email = person.Login.Email };
            //    if (_userManager.GetLoginsAsync(usuario) == null)
            //    {
            //        return BadRequest("Login ja existe");
            //    }
            //    else
            //    {
            //        var resultado = await _userManager.CreateAsync(usuario, person.Login.Password);
            //        try
            //        {
            //            if (resultado.Succeeded)
            //            {
            //                usuario.Person = person;
            //                _context.People.Add(person);
            //                _context.Logins.Add(person.Login);
            //                _context.Usuarios.Add(usuario);
            //                await _context.SaveChangesAsync();
            //                await _context.SaveChangesAsync();
            //                return Ok(person);
            //            }
            //            else
            //            {
            //                return BadRequest("Alguma coisa deu errado socorro");
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            return BadRequest(e.Message);
            //        }
            //    }
            //}

        }
        //POST api/Account/Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            //Verificar se o email já existe, se não existe retornar q o email não existe
            //var l = _userManager.GetLoginsAsync
            var user = await _context.Usuarios.FindAsync(login);
            if (user == null)
            {
                return BadRequest("Usuario não foi encontrado, verifique o login " + user);
            }
            else
            {

                var resultado = await _signInManager.PasswordSignInAsync(login.Email, login.Password,
                    isPersistent: false, lockoutOnFailure: false);
                if (resultado.Succeeded)
                {
                    var token = TokenUsuario(login);

                    return Ok(new { Token = token, Message = "Sucesso" });
                }
                else
                {
                    //Add aq alguma logica para retornar o erro
                    ModelState.AddModelError(string.Empty, "login falhou");
                    return BadRequest(ModelState);
                }
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
        //[Authorize]
        //[HttpPost("ChangePassword")]
        //public async Task<ActionResult> ChangePassword(Login login,ChangePassword model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var user = await _context.Usuarios.FindAsync(login);
        //    if(user == null)
        //    {
        //        return BadRequest("Usuario não foi encontrado, verifique o login" + user);
        //    }
        //    else
        //    {
        //        IdentityResult result = await _userManager.ChangePasswordAsync(user, model.OldPassword,
        //        model.NewPassword);

        //        if (!result.Succeeded)
        //        {
        //            return BadRequest(result);
        //        }
        //        else
        //        {
        //            return Ok();
        //        }

        //    }
        //    //ResetPassword
        //}
        //public async Task<ActionResult> ResetPassword(Login login, ResetPassword model, JwtSecurityToken tokenReader)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var user = await _context.Usuarios.FindAsync(login.Id);
        //    if (user == null)
        //    {
        //        return BadRequest("Usuario não foi encontrado, verifique o login" + user);
        //    }k
        //    else
        //    {
        //        IdentityResult result = await _userManager.ResetPasswordAsync(user, tokenReader.SigningCredentials.Key.ToString(), model.NewPassword);

        //        if (!result.Succeeded)
        //        {
        //            return BadRequest(result);
        //        }
        //        else
        //        {
        //            return Ok();
        //        }

        //    }
        //}


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
