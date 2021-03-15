using ClienteMercado.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaLocalidadesAtendidasViewModel : Localidade_CidadeEmpresaCliente
    {
        public string idLocalidade { get; set; }
        public string nomeLocalidade { get; set; }
        public string cidadeLocalidade { get; set; }
        public string cepLocalidade { get; set; }
    }
}
