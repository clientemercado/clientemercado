using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using System.Collections.Generic;
using System.Linq;

namespace ClienteMercado.Infra.Repositories
{
    public class DItensPedidoCentralComprasRepository : RepositoryBase<itens_pedido_central_compras>
    {
        //GRAVAR ITEM do PEDIDO
        public int GravarItemDoPedido(itens_pedido_central_compras obj)
        {
            itens_pedido_central_compras gravarItemPedido =
                _contexto.itens_pedido_central_compras.Add(obj);
            _contexto.SaveChanges();

            return gravarItemPedido.ID_CODIGO_ITENS_PEDIDO_CENTRAL_COMPRAS;
        }

        //VERIFICAR SE O FORNECEDOR RECEBEU PEDIDO DESTE PRODUTO
        public bool ConsultarSeOFornecedorRecebeuPedidoParaEsteProduto(int idItemCotadoAoFornecedor)
        {
            bool produtoPedido = false;

            itens_pedido_central_compras produtoPedidoAoFornecedor = 
                _contexto.itens_pedido_central_compras.FirstOrDefault(m => (m.ID_CODIGO_COTACAO_FILHA_NEGOCIACAO_CENTRAL_COMPRAS == idItemCotadoAoFornecedor));

            if (produtoPedidoAoFornecedor != null)
            {
                produtoPedido = true;
            }

            return produtoPedido;
        }

        //EXCLUIR o ITEM do PEDIDO
        public bool ExcluirItemDoPedido(int idItemASerExcluido, int idPedido)
        {
            bool itemPedidoExcluido = false;

            //itens_pedido_central_compras itemPedidoASerExcluido =
            //    _contexto.itens_pedido_central_compras.FirstOrDefault(m => ((m.ID_CODIGO_COTACAO_FILHA_NEGOCIACAO_CENTRAL_COMPRAS == idItemASerExcluido)
            //    && (m.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS == idPedido)));

            itens_pedido_central_compras itemPedidoASerExcluido = 
                _contexto.itens_pedido_central_compras.FirstOrDefault(m => ((m.ID_ITENS_COTACAO_INDIVIDUAL_EMPRESA_CENTRAL_COMPRAS == idItemASerExcluido) 
                && (m.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS == idPedido)));

            if (itemPedidoASerExcluido != null)
            {
                _contexto.itens_pedido_central_compras.Remove(itemPedidoASerExcluido);
                _contexto.SaveChanges();

                itemPedidoExcluido = true;
            }

            return itemPedidoExcluido;
        }

        //EXCLUIR TODOS os ITENS do PEDIDO
        public bool ExcluirTodosOsItensDoPedido(int idPedido)
        {
            bool itensPedidoExcluido = false;

            List<itens_pedido_central_compras> listaItensPedidoASerExcluido =
                _contexto.itens_pedido_central_compras.Where(m => (m.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS == idPedido)).ToList();

            if (listaItensPedidoASerExcluido.Count > 0)
            {
                var command0 = "DELETE FROM itens_pedido_central_compras WHERE ID_CODIGO_PEDIDO_CENTRAL_COMPRAS = " + idPedido;
                _contexto.Database.ExecuteSqlCommand(command0);

                itensPedidoExcluido = true;
            }

            return itensPedidoExcluido;
        }

        //BUSCAR LISTA de ITENS PEDIDOS
        public List<itens_pedido_central_compras> ConsultarListaDeItensJahPedidos(string idsPedidos)
        {
            var query = "SELECT * FROM itens_pedido_central_compras WHERE ID_CODIGO_PEDIDO_CENTRAL_COMPRAS IN(" + idsPedidos + ")";
            var result = _contexto.Database.SqlQuery<itens_pedido_central_compras>(query).ToList();

            return result;
        }

        //CONSULTAR se o PRODUTO JÁ foi PEDIDO
        public itens_pedido_central_compras ConsultarSeOProdutoJahFoiPedido(int idItemIndividual)
        {
            itens_pedido_central_compras itemPedido =
                _contexto.itens_pedido_central_compras.FirstOrDefault(m => (m.ID_ITENS_COTACAO_INDIVIDUAL_EMPRESA_CENTRAL_COMPRAS == idItemIndividual));

            return itemPedido;
        }
    }
}
