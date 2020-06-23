using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaItensCotadosRespondidosPeloFornecedorViewModel
    {
        public string nomeProdutoCotado { get; set; }

        public string quantidadeCotada { get; set; }

        public string precoUnitario { get; set; }

        public string desconto { get; set; }

        public string valorTotal { get; set; }

        public string produtoAlternativo { get; set; }
    }
}
