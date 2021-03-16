using ClienteMercado.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaPromocoesEmpresaViewModel : PromocaoVenda_EmpresaCliente
    {
        public string idPromocaoVenda { get; set; }
        public string nomeOferta { get; set; }
        public string percentualOffOferta { get; set; }
        public string dataCadastroOferta { get; set; }
        public string dataValidadeOferta { get; set; }
        public string ativaInativaOferta { get; set; }
    }
}
