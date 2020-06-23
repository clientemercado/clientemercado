using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.ViewModel
{
    public class ListaPorItemCotadoJaCalculadoUsuarioEmpresaCotanteViewModel
    {
        public int ID_DACOTACAO { get; set; }

        public decimal PRECO_FINAL_CALCULADO_DO_PRODUTO { get; set; }

        public int ID_CODIGO_ITENS_COTACAO_USUARIO_EMPRESA { get; set; }

        public decimal QUANTIDADE_ITENS_COTACAO_USUARIO_EMPRESA { get; set; }

        public decimal PRECO_ITENS_COTACAO_USUARIO_EMPRESA { get; set; }
    }
}
