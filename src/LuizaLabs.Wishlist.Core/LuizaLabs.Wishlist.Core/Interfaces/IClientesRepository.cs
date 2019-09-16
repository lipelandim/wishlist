using LuizaLabs.Wishlist.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Core.Interfaces
{
    public interface IClientesRepository
    {
        void CadastrarCliente(Clientes cli);
        void RemoverCliente(Clientes email);
        Task<bool> AdicionarProdutoAsync(Clientes cli);
        Clientes VerificarUsuario(string email);
        void RemoverProduto(Clientes cli);
    }
}
