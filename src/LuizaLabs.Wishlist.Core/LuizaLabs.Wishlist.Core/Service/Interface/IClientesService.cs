using LuizaLabs.Wishlist.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Core.Service
{
    public interface IClientesService
    {
        void CadastrarCliente(Clientes cli);
        void RemoverCliente(Clientes cli);
        void AdicionarProduto(Clientes cli);
        Clientes VerificarUsuario(string email);
        Task<bool> VerificarExistProdutoAsync(string produto);
        void RemoverProduto(Clientes cliData);
    }
}
