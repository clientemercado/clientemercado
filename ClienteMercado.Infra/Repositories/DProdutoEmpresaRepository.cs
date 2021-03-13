using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Infra.Repositories
{
    public class DProdutoEmpresaRepository : RepositoryBase<Produto_EmpresaCliente>
    {
        /// <summary>
        /// GRAVAR NOVO PRODUTO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>
        public Produto_EmpresaCliente GravarNovoProdutoEmpresaCliente(Produto_EmpresaCliente obj)
        {
            try
            {
                Produto_EmpresaCliente novoProdutoEmpresa =
                    _contexto.produto_empresaCliente.Add(obj);
                _contexto.SaveChanges();

                return novoProdutoEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CONSULTAR DADOS do PRODUTO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>
        public Produto_EmpresaCliente ConsultarDadosDoProduto(Produto_EmpresaCliente obj)
        {
            try
            {
                Produto_EmpresaCliente dadosDoProduto = 
                    _contexto.produto_empresaCliente.Include("SubDepartamento_EmpresaCliente")
                    .Include("Empresa_FabricantesMarcas").Include("PromocaoVenda_EmpresaCliente")
                    .FirstOrDefault(m => (m.id_ProdutoEmpresaCliente == obj.id_ProdutoEmpresaCliente));

                return dadosDoProduto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
