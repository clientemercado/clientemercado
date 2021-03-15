using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaSubDeptosEmpresaViewModel
    {
        public string idSubDepartamentoEmpresa { get; set; }
        public string nomeSubDepartamentoEmpresa { get; set; }
        public string nomeDepartamentoEmpresa { get; set; }
        public string ativoInativoSubDeptoEmpresa { get; set; }

        public int id_SubDepartamentoEmpresaCliente { get; set; }
        public int id_DepartamentoEmpresaCliente { get; set; }
        public bool ativoInativo_SubDepartamentoEmpresaCliente { get; set; }
    }
}
