using ClienteMercado.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaMeiosPagtoViewModel : MeiosPagamento_EmpresaCliente
    {
        public string idMeioPagto { get; set; }
        public string nomeMeioPagto { get; set; }
    }
}
