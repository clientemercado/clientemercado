using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Domain.Services
{
    public class NPedidoClienteEmpresaService
    {
        DPedidoClienteEmpresaRepository drepository = new DPedidoClienteEmpresaRepository();

        /// <summary>
        /// CONSULTAR DADOS do PEDIDO do CLIENTE da EMPRESA
        /// </summary>
        public PedidoCliente_EmpresaCliente ConsultarDadosDoPedidoCleinteEmpresa(PedidoCliente_EmpresaCliente obj)
        {
            return drepository.ConsultarDadosDoPedidoCleinteEmpresa(obj);
        }

        /// <summary>
        /// ALTERAR DADOS do PEDIDO do CLIENTE da EMPRESA
        /// </summary>
        public void AlterarDadosPedidoClienteEmpresa(PedidoCliente_EmpresaCliente obj)
        {
            drepository.AlterarDadosPedidoClienteEmpresa(obj);
        }

        /// <summary>
        /// CARREGAR LISTA de PRODUTOS do PEDIDO
        /// </summary>
        public List<ListaItensDoPedidoViewModel> BuscarListaDeProdutosDoPedido(int idPedido)
        {
            return drepository.BuscarListaDeProdutosDoPedido(idPedido);
        }
    }
}
