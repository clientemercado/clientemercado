using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaDeIdsDeEmpresasQueAnexaramACotacaoViewModel
    {
        public int ID_CODIGO_EMPRESA { get; set; }

        public int ID_COTACAO_INDIVIDUAL_EMPRESA_CENTRAL_COMPRAS { get; set; }

        public bool NEGOCIACAO_COTACAO_ACEITA { get; set; }
    }
}
