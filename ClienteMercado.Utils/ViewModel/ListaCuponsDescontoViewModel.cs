using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaCuponsDescontoViewModel
    {
        public int idCuponDescontoEmpresaCliente { get; set; }
        public string nomeCupom { get; set; }
        public string dataCadastroCupon { get; set; }
        public string dataValidade { get; set; }
        public string percentualDesconto { get; set; }
        public string ativoInativo { get; set; }
        public string usuarioCadastrouCupon { get; set; }
        public string usuarioAtivouCupon { get; set; }

        public DateTime dataCadastroCupon_CupomDescontoEmpresaCliente { get; set; }
        public DateTime dataValidade_CupomDescontoEmpresaCliente { get; set; }
        public decimal percentualDesconto_CupomDescontoEmpresaCliente { get; set; }
        public bool ativoInativo_CupomDescontoEmpresaCliente { get; set; }
        public int? idUsuarioCadastrouCupon_CupomDescontoEmpresaCliente { get; set; }
        public int? idUsuarioAtivou_CupomDescontoEmpresaCliente { get; set; }
    }
}
