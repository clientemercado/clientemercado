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

        /// <summary>
        /// ALTERAR DADOS do PEDIDO do CLIENTE da EMPRESA
        /// </summary>
        /// <returns></returns>
        public void AlterarDadosProdutoEmpresa(Produto_EmpresaCliente obj)
        {
            try
            {
                Produto_EmpresaCliente dadosProdutoEmpresa =
                    _contexto.produto_empresaCliente.FirstOrDefault(m => ((m.id_ProdutoEmpresaCliente == obj.id_ProdutoEmpresaCliente)
                    && (m.id_EmpresaCliente == obj.id_EmpresaCliente)));

                if (dadosProdutoEmpresa != null)
                {
                    dadosProdutoEmpresa.id_SubDepartamentoEmpresaCliente = Convert.ToInt32(obj.id_SubDepartamentoEmpresaCliente);
                    dadosProdutoEmpresa.id_EmpresaFabricantesMarcas = Convert.ToInt32(obj.id_EmpresaFabricantesMarcas);

                    if (obj.id_PromocaoVendaEmpresaCliente > 0)
                        dadosProdutoEmpresa.id_PromocaoVendaEmpresaCliente = Convert.ToInt32(obj.id_PromocaoVendaEmpresaCliente);

                    dadosProdutoEmpresa.descricao_ProdutoEmpresaCliente = obj.descricao_ProdutoEmpresaCliente;
                    dadosProdutoEmpresa.tipoEmbalagem_ProdutoEmpresaCliente = obj.tipoEmbalagem_ProdutoEmpresaCliente;
                    dadosProdutoEmpresa.pesoEmbalagem_ProdutoEmpresaCliente = obj.pesoEmbalagem_ProdutoEmpresaCliente;
                    dadosProdutoEmpresa.unidadePesoEmbalagem_ProdutoEmpresaCliente = obj.unidadePesoEmbalagem_ProdutoEmpresaCliente;
                    dadosProdutoEmpresa.valorVenda_ProdutoEmpresaCliente = obj.valorVenda_ProdutoEmpresaCliente;
                    dadosProdutoEmpresa.id_SubDepartamentoEmpresaCliente = obj.id_SubDepartamentoEmpresaCliente;
                    dadosProdutoEmpresa.id_EmpresaFabricantesMarcas = obj.id_EmpresaFabricantesMarcas;
                    dadosProdutoEmpresa.id_PromocaoVendaEmpresaCliente = obj.id_PromocaoVendaEmpresaCliente;
                    dadosProdutoEmpresa.ativoInativo_ProdutoEmpresaCliente = obj.ativoInativo_ProdutoEmpresaCliente;

                    _contexto.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
