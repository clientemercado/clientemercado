using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public pedido_central_compras GerarPedidoCC(pedido_central_compras obj)
        {
            pedido_central_compras gravarPedidoCC =
                _contexto.pedido_central_compras.Add(obj);
            _contexto.SaveChanges();

            return gravarPedidoCC;
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

        //CONFIRMAR o ACEITE do PEDIDO
        public void SetarConfirmandoAceiteDoPedido(int iCM, int iCCF, int idPedido, int idTipoFrete, int idFormaPagto, string dataEntrega)
        {
            var dataFormatada = 
                Convert.ToDateTime(dataEntrega).Year.ToString() + "-" + Convert.ToDateTime(dataEntrega).Month.ToString() + "-" + Convert.ToDateTime(dataEntrega).Day.ToString();

            pedido_central_compras dadosDoPedidoCC = 
                _contexto.pedido_central_compras.FirstOrDefault(m => ((m.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS == iCCF) 
                && (m.ID_CODIGO_COTACAO_MASTER_CENTRAL_COMPRAS == iCM) && (m.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS == idPedido)));

            if (dadosDoPedidoCC != null)
            {
                dadosDoPedidoCC.CONFIRMADO_PEDIDO_CENTRAL_COMPRAS = true;
                dadosDoPedidoCC.DATA_ENTREGA_PEDIDO_CENTRAL_COMPRAS = Convert.ToDateTime(dataFormatada);
                dadosDoPedidoCC.ID_FORMA_PAGAMENTO = idFormaPagto;
                dadosDoPedidoCC.ID_TIPO_FRETE = idTipoFrete;

                _contexto.SaveChanges();
            }
        }

        //GERAR NOVO CODIGO de CONTROLE do PEDIDO
        public string GerarCodigoControleDoPedido(int cCC)
        {
            var codControle = "";

            var query = "SELECT TOP 1 ID_CODIGO_PEDIDO_CENTRAL_COMPRAS " +
                        "FROM pedido_central_compras PC " +
                        "INNER JOIN central_de_compras CC ON(CC.ID_CENTRAL_COMPRAS = " + cCC + ") " +
                        "ORDER BY ID_CODIGO_PEDIDO_CENTRAL_COMPRAS DESC";
            var result = _contexto.Database.SqlQuery<pedido_central_compras>(query).ToList();

            codControle = result[0].ID_CODIGO_PEDIDO_CENTRAL_COMPRAS > 0 ? (result[0].ID_CODIGO_PEDIDO_CENTRAL_COMPRAS + 1).ToString() : "000001";

            return codControle;
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
