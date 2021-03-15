using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaEmpresasClientesViewModel
    {
        public string idEmpresa { get; set; }
        public string nomeRazaoSocialEmpresa { get; set; }
        public string nomeFantasiaEmpresa { get; set; }
        public string cidadeEmpresa { get; set; }
        public string dataCadastroEmpresa { get; set; }
        public string ativaInativaEmpresa { get; set; }

        public int id_EmpresaCliente { get; set; }
        public DateTime dataCadastro_EmpresaCliente { get; set; }
        public bool ativaInativa_EmpresaCliente { get; set; }
    }
}
