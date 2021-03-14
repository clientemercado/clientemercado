using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Domain.Services
{
    public class NProdutoEmpresaService
    {
        DProdutoEmpresaRepository drepository = new DProdutoEmpresaRepository();

        /// <summary>
        /// GRAVAR NOVO PRODUTO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>
        public Produto_EmpresaCliente GravarNovoProdutoEmpresaCliente(Produto_EmpresaCliente obj)
        {
            return drepository.GravarNovoProdutoEmpresaCliente(obj);
        }

        /// <summary>
        /// CONSULTAR DADOS do PRODUTO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>

        public Produto_EmpresaCliente ConsultarDadosDoProduto(Produto_EmpresaCliente obj)
        {
            return drepository.ConsultarDadosDoProduto(obj);
        }

        /// <summary>
        /// ALTERAR DADOS do PEDIDO do CLIENTE da EMPRESA
        /// </summary>
        /// <returns></returns>
        public void AlterarDadosProdutoEmpresa(Produto_EmpresaCliente obj)
        {
            drepository.AlterarDadosProdutoEmpresa(obj);
        }
    }
}
