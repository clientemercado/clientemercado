using ClienteMercado.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaUsuariosEmpresaViewModel : Usuario_EmpresaCliente
    {
        public string idUsuarioEmpresa { get; set; }
        public string nomeusuarioEmpresa { get; set; }
        public string fone1ContatoUsuarioEmpresa { get; set; }
        public string eMail1UsuarioEmpresa { get; set; }
        public string dataCadastroUsuarioEmpresa { get; set; }
        public string cidadeUsuarioEmpresa { get; set; }
        public string ativoInativoUsuarioEmpresa { get; set; }
    }
}
