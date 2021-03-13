using ClienteMercado.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaItensDoPedidoViewModel : ProdutosPedidoCliente_EmpresaCliente
    {
        public int idPedido { get; set; }
        public string itemPedido { get; set; }
        public string quantidadeItemPedido { get; set; }
        public string valorUnitarioItemPedido { get; set; }
        public string dataEntregaItemPedido { get; set; }
        public string motivoNaoEntregaDotemPedido { get; set; }
    }
}
