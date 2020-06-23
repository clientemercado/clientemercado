using System;
using System.Collections.Generic;
using System.Linq;
using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;

namespace ClienteMercado.Infra.Repositories
{
    public class DPedidoCentralComprasRepository : RepositoryBase<pedido_central_compras>
    {
        //VERIFICAR se a COTAÇÃO se já se converteu em PEDIDO
        public pedido_central_compras VerificaSeACotacaoPossuiPedido(int iCM)
        {
            pedido_central_compras cotacaoComPedido = 
                _contexto.pedido_central_compras.FirstOrDefault(m => (m.ID_CODIGO_COTACAO_MASTER_CENTRAL_COMPRAS == iCM));

            return cotacaoComPedido;
        }

        //GERAR o PEDIDO feito pelo USUÁRIO ADM da CENTRAL de COMPRAS (Independente do Pedido ser TOTAL ou PARCIAL)
        public int GerarPedidoCC(pedido_central_compras obj)
        {
            pedido_central_compras gravarPedidoCC = 
                _contexto.pedido_central_compras.Add(obj);
                _contexto.SaveChanges();

            return gravarPedidoCC.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS;
        }

        //VERIFICAR se o FORNECEDOR recebeu PEDIDO para ESTA COTAÇÃO
        public pedido_central_compras VerificarSeEstaCotacaoRecebeuPedido(int iCM, int iCCF)
        {
            pedido_central_compras recebeuPedidoParaACotacao = 
                _contexto.pedido_central_compras.FirstOrDefault(m => ((m.ID_CODIGO_COTACAO_MASTER_CENTRAL_COMPRAS == iCM) && (m.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS == iCCF)));

            return recebeuPedidoParaACotacao;
        }

        //ATUALIZAR o VALOR TOTAL REGISTRADO para o PEDIDO
        public void AtualizarValorDoPedido(pedido_central_compras obj)
        {
            pedido_central_compras dadosDoPedidoAAtualizar = 
                _contexto.pedido_central_compras.FirstOrDefault(m => (m.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS == obj.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS));

            if (dadosDoPedidoAAtualizar != null)
            {
                dadosDoPedidoAAtualizar.VALOR_PEDIDO_CENTRAL_COMPRAS = obj.VALOR_PEDIDO_CENTRAL_COMPRAS;
                _contexto.SaveChanges();
            }
        }

        //EXCLUIR o PEDIDO
        public bool ExcluirOPedido(int idPedido)
        {
            bool pedidoExcluido = false;

            pedido_central_compras pedidoASerExcluido =
                _contexto.pedido_central_compras.FirstOrDefault(m => (m.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS == idPedido));

            if (pedidoASerExcluido != null)
            {
                var command0 = "DELETE FROM pedido_central_compras WHERE ID_CODIGO_PEDIDO_CENTRAL_COMPRAS = " + idPedido;
                _contexto.Database.ExecuteSqlCommand(command0);

                pedidoExcluido = true;
            }

            return pedidoExcluido;
        }

        //VERIFICAR TODOS os PEDIDOS para ESTA COTAÇÃO
        public List<pedido_central_compras> BuscarTodosOsPedidosParaACotacao(int iCM)
        {
            List<pedido_central_compras> listaDePedidosDaCotacao = 
                _contexto.pedido_central_compras.Where(m => (m.ID_CODIGO_COTACAO_MASTER_CENTRAL_COMPRAS == iCM)).ToList();

            return listaDePedidosDaCotacao;
        }

        //BUSCAR DADOS do PEDIDO - ATUALIZAR o VALOR TOTAL REGISTRADO para o PEDIDO
        public pedido_central_compras ConsultarDadosDoPedidoPeloCodigo(int idPedidoGeradoCC)
        {
            pedido_central_compras dadosDoPedidoDaCC = 
                _contexto.pedido_central_compras.FirstOrDefault(m => (m.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS == idPedidoGeradoCC));

            return dadosDoPedidoDaCC;
        }
    }
}
