using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using ClienteMercado.Utils.Net;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Infra.Repositories
{
    public class DProdutoEmpresaRepository : RepositoryBase<Produto_EmpresaCliente>
    {
        int? idEmpresa = Sessao.IdEmpresaUsuario;

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
        /// BUSCAR LISTA de PRODUTOS da EMPRESA
        /// </summary>
        /// <returns></returns>
        public List<ListaProdutosEmpresaViewModel> BuscarListaDeProdutosDaEmpresa()
        {
            try
            {
                var query = "SELECT PE.id_ProdutoEmpresaCliente, PE.descricao_ProdutoEmpresaCliente AS nomeProduto, " + 
                            "DE.descricao_DepartamentoEmpresaCliente AS departamentoProduto, SD.descricao_SubDepartamentoEmpresaCliente AS subDepartamentoProduto, " +
                            "EF.descricao_EmpresaFabricantesMarcas AS fabricanteMarcaProduto, PR.nomeOferta_PromocaoVendaEmpresaCliente AS promocaoVigenteProduto, " +
                            "PE.descricao_ProdutoEmpresaCliente AS nomeProduto, PE.tipoEmbalagem_ProdutoEmpresaCliente AS tipoEmbalagemProduto, PE.pesoEmbalagem_ProdutoEmpresaCliente, " +
                            "PE.unidadePesoEmbalagem_ProdutoEmpresaCliente AS unidadeProduto, PE.valorVenda_ProdutoEmpresaCliente, PE.ativoInativo_ProdutoEmpresaCliente " +
                            "FROM  Produto_EmpresaCliente PE " +
                            "INNER JOIN SubDepartamento_EmpresaCliente SD ON(SD.id_SubDepartamentoEmpresaCliente = PE.id_SubDepartamentoEmpresaCliente) " +
                            "INNER JOIN Departamento_EmpresaCliente DE ON(DE.id_DepartamentoEmpresaCliente = SD.id_DepartamentoEmpresaCliente) " +
                            "INNER JOIN Empresa_FabricantesMarcas EF ON(EF.id_EmpresaFabricantesMarcas = PE.id_EmpresaFabricantesMarcas) " +
                            "INNER JOIN PromocaoVenda_EmpresaCliente PR ON(PR.id_PromocaoVendaEmpresaCliente = PE.id_PromocaoVendaEmpresaCliente) " +
                            "WHERE PE.id_EmpresaCliente = " + idEmpresa;
                var listaProdutosEmpresa = _contexto.Database.SqlQuery<ListaProdutosEmpresaViewModel>(query).ToList();

                return listaProdutosEmpresa;
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
