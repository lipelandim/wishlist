
using LuizaLabs.Wishlist.Core.Domain.Entities;
using LuizaLabs.Wishlist.Core.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace LuizaLabs.Wishlist.Core.Service
{
   

    public class ClientesService : IClientesService
    {
        IClientesRepository _clientesRepository;
        String UrlApi = "";
        public ClientesService(IClientesRepository clientesRepository, IOptions<SettingsApi> settings)
        {
            _clientesRepository = clientesRepository;
            UrlApi = settings.Value.ApiMglu;
        }

        public void AdicionarProduto(Clientes cli)
        {
            _clientesRepository.AdicionarProdutoAsync(cli);
        }

        public void CadastrarCliente(Clientes cli)
        {
            _clientesRepository.CadastrarCliente(cli);
        }

        public void RemoverCliente(Clientes cli)
        {
            _clientesRepository.RemoverCliente(cli);
        }

        public void RemoverProduto(Clientes cliData)
        {
            _clientesRepository.RemoverProduto(cliData);
        }

        public async System.Threading.Tasks.Task<bool> VerificarExistProdutoAsync(string produto)
        {



            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(UrlApi + produto);

                if (response.StatusCode ==  System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
            

        public Clientes VerificarUsuario(string email)
        {
            return _clientesRepository.VerificarUsuario(email);
        }
    }
}
