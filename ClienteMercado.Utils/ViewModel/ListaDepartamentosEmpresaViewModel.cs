using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaDepartamentosEmpresaViewModel
    {
        public string idDepartamentoEmpresa { get; set; }
        public string nomeDepartamentoEmpresa { get; set; }
        public string ativoInativoDeptoEmpresa { get; set; }

        public int id_DepartamentoEmpresaCliente { get; set; }
        public bool ativoInativo_DepartamentoEmpresaCliente { get; set; }
    }
}
