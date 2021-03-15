using ClienteMercado.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaPedidosClientesViewModel : PedidoCliente_EmpresaCliente
    {
        public string idPedidoCliente { get; set; }
        public string codControlePedido { get; set; }
        public string nomeClientePedido { get; set; }
        public string valorPedido { get; set; }
        public string pedidoEntregue { get; set; }
    }
}
