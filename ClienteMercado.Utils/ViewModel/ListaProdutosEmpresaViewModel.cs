using ClienteMercado.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaProdutosEmpresaViewModel : Produto_EmpresaCliente
    {
        public string idCodProduto { get; set; }
        public string nomeProduto { get; set; }
        public string tipoEmbalagemProduto { get; set; }
        public string pesoProduto { get; set; }
        public string unidadeProduto { get; set; }
        public string valorVendaProduto { get; set; }
        public string departamentoProduto { get; set; }
        public string subDepartamentoProduto { get; set; }
        public string promocaoVigenteProduto { get; set; }
        public string ativoInativoProduto { get; set; }
        public string fabricanteMarcaProduto { get; set; }
    }
}
