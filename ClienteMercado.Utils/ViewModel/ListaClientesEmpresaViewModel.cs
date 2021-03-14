using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaClientesEmpresaViewModel
    {
        public int id_ClienteEmpresaCliente { get; set; }
        public string nomeClienteEmpresa { get; set; }
        public string enderecoClienteEmpresa { get; set; }
        public string cidadeClienteEmpresa { get; set; }
        public string dataCadastroClienteEmpresa { get; set; }
        public DateTime dataCadastro_ClienteEmpresaCliente { get; set; }
        public string quantidadePedidosApp { get; set; }
        public string totalvalorPedidoApp { get; set; }
    }
}
