using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using LuizaLabs.Wishlist.Apis.Models.DTO;
using LuizaLabs.Wishlist.Apis.Models.ViewModels;
using LuizaLabs.Wishlist.Core.Domain.Entities;
using LuizaLabs.Wishlist.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using static LuizaLabs.Wishlist.Core.TokenConfiguration;

namespace LuizaLabs.Wishlist.Apis.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
    
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public object Login(
           [FromBody]AccessCredentials credenciais,
           [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations,
           [FromServices]IDistributedCache cache)
        {
            bool credenciaisValidas = false;
            if (credenciais != null && !String.IsNullOrWhiteSpace(credenciais.UserID))
            {
                if (credenciais.GrantType == "password")
                {
                    Users usDest = new Users
                    {
                        userId = credenciais.UserID,
                        accessKey = credenciais.AccessKey
                    };
                    var usuarioBase = _usersService.Find(usDest);
                    credenciaisValidas = (usuarioBase != null &&
                        credenciais.UserID == usuarioBase.userId &&
                        credenciais.AccessKey == usuarioBase.accessKey);
                }
                else if (credenciais.GrantType == "refresh_token")
                {
                    if (!String.IsNullOrWhiteSpace(credenciais.RefreshToken))
                    {
                        RefreshTokenData refreshTokenBase = null;

                        string strTokenArmazenado =
                            cache.GetString(credenciais.RefreshToken);
                        if (!String.IsNullOrWhiteSpace(strTokenArmazenado))
                        {
                            refreshTokenBase = JsonConvert
                                .DeserializeObject<RefreshTokenData>(strTokenArmazenado);
                        }

                        credenciaisValidas = (refreshTokenBase != null &&
                            credenciais.UserID == refreshTokenBase.UserID &&
                            credenciais.RefreshToken == refreshTokenBase.RefreshToken);

                        // Elimina o token de refresh já que um novo será gerado
                        if (credenciaisValidas)
                            cache.Remove(credenciais.RefreshToken);
                    }

                }
            }

            if (credenciaisValidas)
            {
                return GenerateToken(
                    credenciais.UserID, signingConfigurations,
                    tokenConfigurations, cache);
            }
            else
            {
                return BadRequest(new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                });
            }
        }

        private object GenerateToken(string userID,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IDistributedCache cache)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(userID, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userID)
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(tokenConfigurations.Seconds);
            TimeSpan finalExpiration = TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            var resultado = new
            {
                authenticated = true,
                created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                refreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
                message = "OK"
            };

            var refreshTokenData = new RefreshTokenData();
            refreshTokenData.RefreshToken = resultado.refreshToken;
            refreshTokenData.UserID = userID;

            DistributedCacheEntryOptions opcoesCache =
                new DistributedCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(finalExpiration);
            cache.SetString(resultado.refreshToken,
                JsonConvert.SerializeObject(refreshTokenData),
                opcoesCache);

            return resultado;
        }

        [AllowAnonymous]
        [HttpPut]
        public ActionResult AddUsuario(Users users)
        {
            try
            {

            _usersService.Add(users);
            return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}