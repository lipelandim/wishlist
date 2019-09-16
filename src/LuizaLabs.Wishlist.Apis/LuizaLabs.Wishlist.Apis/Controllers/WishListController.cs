using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuizaLabs.Wishlist.Apis.Models.DTO;
using LuizaLabs.Wishlist.Apis.Models.ViewModels;
using LuizaLabs.Wishlist.Core.Domain.Entities;
using LuizaLabs.Wishlist.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LuizaLabs.Wishlist.Apis.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class wishListController : ControllerBase
    {
        private readonly IClientesService _clientesService;

        public wishListController(IClientesService clienteService)
        {
            _clientesService = clienteService;
        }

        
        // GET api/values/teste@luizalabs.com.br
        [HttpGet("{email}")]
        public ActionResult<string> Get(string email)
        {
            try
            {

                Clientes cliData = _clientesService.VerificarUsuario(email);

                return Ok(cliData);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [ActionName("addProdCli")]
        public ActionResult Put(produtoClienteViewModel prodCli)
        {
            try
            {

                Clientes cliData = _clientesService.VerificarUsuario(prodCli.Email);

                if (cliData == null)
                {
                    return NotFound(new JObject(new JProperty("Not Found", "Cliente não existente")));
                }
                if (!_clientesService.VerificarExistProdutoAsync(prodCli.Produto).Result)
                {
                    return NotFound(new JObject(new JProperty("Not Found", "Produto não existente")));
                }

                if (cliData.Wishlist != null)
                {
                    if (cliData.Wishlist.Contains(prodCli.Produto))
                    {
                        return BadRequest(new JObject(new JProperty("Erro", "Produto já existente para este cliente")));
                    }


                    var lstStr = cliData.Wishlist.ToList();
                    lstStr.Add(prodCli.Produto);

                    cliData.Wishlist = lstStr.ToArray();
                }
                else
                {

                    cliData.Wishlist = new string[] { prodCli.Produto };
                }
                _clientesService.AdicionarProduto(cliData);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        [ActionName("removeProdCli")]
        public ActionResult Delete(produtoClienteViewModel prodCli)
        {
            try
            {

                Clientes cliData = _clientesService.VerificarUsuario(prodCli.Email);

                if (cliData == null)
                {
                    return NotFound(new JObject(new JProperty("Not Found", "Cliente não existente")));
                }


                if (cliData.Wishlist != null)
                {
                    if (!cliData.Wishlist.Contains(prodCli.Produto))
                    {
                        return NotFound(new JObject(new JProperty("Not Found", "Produto não existente para este cliente")));
                    }


                    var lstStr = cliData.Wishlist.ToList();
                    lstStr.Remove(prodCli.Produto);

                    cliData.Wishlist = lstStr.ToArray();
                }
                else
                {
                    return NotFound(new JObject(new JProperty("Not Found", "Não possui produtos para este cliente")));
                }

                _clientesService.RemoverProduto(cliData);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}
