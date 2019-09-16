using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuizaLabs.Wishlist.Apis.Models.DTO;
using LuizaLabs.Wishlist.Apis.Models.ViewModels;
using LuizaLabs.Wishlist.Core.Domain.Entities;
using LuizaLabs.Wishlist.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LuizaLabs.Wishlist.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ClientesController : ControllerBase
    {
        IClientesService _clientesService;
        public ClientesController(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }


        [HttpPost]
        [ActionName("CadastrarCliente")]
        public ActionResult CadastrarCliente(clientescadViewModel cliente)
        {
            try
            {

                Clientes cli = new Clientes();
                cli.Nome = cliente.Nome;
                cli.Email = cliente.Email;

                Clientes cliData = _clientesService.VerificarUsuario(cliente.Email);

                if (cliData != null)
                    return Conflict(new JObject(new JProperty("Conflict", "Cliente já existente")));


                _clientesService.CadastrarCliente(cli);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{email}")]
        [ActionName("RemoverCliente")]
        public ActionResult RemoverCliente(string email)
        {
            try
            {

                Clientes cliData = _clientesService.VerificarUsuario(email);

                if (cliData == null)
                {
                    return NotFound(new JObject(new JProperty("Not Found", "Cliente não existente")));
                }

                _clientesService.RemoverCliente(cliData);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}