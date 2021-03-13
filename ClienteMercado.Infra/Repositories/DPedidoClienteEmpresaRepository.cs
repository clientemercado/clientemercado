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
    public class DPedidoClienteEmpresaRepository : RepositoryBase<PedidoCliente_EmpresaCliente>
    {
        int? idEmpresa = Sessao.IdEmpresaUsuario;

        public PedidoCliente_EmpresaCliente ConsultarDadosDoPedidoCleinteEmpresa(PedidoCliente_EmpresaCliente pedidoCliente_EmpresaCliente, object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// CONSULTAR DADOS do PEDIDO do CLIENTE da EMPRESA
        /// </summary>
        public PedidoCliente_EmpresaCliente ConsultarDadosDoPedidoCleinteEmpresa(PedidoCliente_EmpresaCliente obj)
        {
            try
            {
                PedidoCliente_EmpresaCliente dadosPedidoClienteEmpresa = 
                    _contexto.pedidoCliente_empresaCliente.Include("Cliente_EmpresaCliente").Include("Cidade_EmpresaCliente").Include("MeiosPagamento_EmpresaCliente")
                    .FirstOrDefault(m => ((m.id_PedidoClienteEmpresaCliente == obj.id_PedidoClienteEmpresaCliente) 
                    && (m.id_EmpresaCliente == idEmpresa)));

                return dadosPedidoClienteEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CARREGAR LISTA de PRODUTOS do PEDIDO
        /// </summary>
        public List<ListaItensDoPedidoViewModel> BuscarListaDeProdutosDoPedido(int idPedido)
        {
            try
            {
                var query = "SELECT PP.id_ProdutosPedidoCliente AS idProdutoPedido, P.descricao_ProdutoEmpresaCliente AS itemPedido, " + 
                            "PP.id_PedidoClienteEmpresaCliente as idPedido, PP.quantidade_ProdutosPedidoCliente, PP.valorUnitario_ProdutosPedidoCliente, " + 
                            "PP.dataEntregaItemPedido_ProdutosPedidoCliente, PP.motivoItemPedidoNaoEntregue_ProdutosPedidoCliente AS motivoNaoEntregaDotemPedido " +
                            "FROM ProdutosPedidoCliente_EmpresaCliente PP " +
                            "INNER JOIN Produto_EmpresaCliente P ON(P.id_ProdutoEmpresaCliente = PP.id_ProdutoEmpresaCliente) " +
                            "WHERE PP.id_PedidoClienteEmpresaCliente = " + idPedido;
                var listaItensDoPedido = _contexto.Database.SqlQuery<ListaItensDoPedidoViewModel>(query).ToList();

                return listaItensDoPedido;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ALTERAR DADOS do PEDIDO do CLIENTE da EMPRESA
        /// </summary>
        public void AlterarDadosPedidoClienteEmpresa(PedidoCliente_EmpresaCliente obj)
        {
            try
            {
                PedidoCliente_EmpresaCliente dadosPedidoClienteEmpresa =
                    _contexto.pedidoCliente_empresaCliente.FirstOrDefault(m => ((m.id_PedidoClienteEmpresaCliente == obj.id_PedidoClienteEmpresaCliente)
                    && (m.id_EmpresaCliente == obj.id_EmpresaCliente)));

                if (dadosPedidoClienteEmpresa != null)
                {
                    dadosPedidoClienteEmpresa.valorPedido_PedidoClienteEmpresaCliente = obj.valorPedido_PedidoClienteEmpresaCliente;
                    dadosPedidoClienteEmpresa.idUsuarioEmpresaEntregou_ClienteEmpresaCliente = obj.idUsuarioEmpresaEntregou_ClienteEmpresaCliente;
                    dadosPedidoClienteEmpresa.pedidoEntregue_PedidoClienteEmpresaCliente = obj.pedidoEntregue_PedidoClienteEmpresaCliente;
                    dadosPedidoClienteEmpresa.dataEntregaPedido_ClienteEmpresaCliente = obj.dataEntregaPedido_ClienteEmpresaCliente;

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
