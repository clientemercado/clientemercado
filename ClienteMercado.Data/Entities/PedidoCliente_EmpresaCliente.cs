using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("PedidoCliente_EmpresaCliente")]
    public partial class PedidoCliente_EmpresaCliente
    {
        public PedidoCliente_EmpresaCliente()
        {
            this.produtosPedidoCliente_empresaCliente = new List<ProdutosPedidoCliente_EmpresaCliente>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_PedidoClienteEmpresaCliente { get; set; }
        public int id_ClienteEmpresaCliente { get; set; }
        public int id_CidadeEmpresaCliente { get; set; }
        public int id_LocalidadeCidadeEmpresaCliente { get; set; }
        public int id_MeiosPagamentoEmpresaCliente { get; set; }
        public int id_CuponDescontoEmpresaCliente { get; set; }
        public int id_EmpresaCliente { get; set; }

        [MaxLength(15)]
        public string codControlePedido_PedidoClienteEmpresaCliente { get; set; }
        public decimal valorPedido_PedidoClienteEmpresaCliente { get; set; }
        public bool pedidoEntregue_PedidoClienteEmpresaCliente { get; set; }
        public int? idUsuarioEmpresaEntregou_ClienteEmpresaCliente { get; set; }
        public DateTime dataEntregaPedido_ClienteEmpresaCliente { get; set; }


        [ForeignKey("id_ClienteEmpresaCliente")]
        public virtual Cliente_EmpresaCliente cliente_empresaCliente { get; set; }

        [ForeignKey("id_CidadeEmpresaCliente")]
        public virtual Cidade_EmpresaCliente cidade_empresaCliente { get; set; }

        [ForeignKey("id_LocalidadeCidadeEmpresaCliente")]
        public virtual Localidade_CidadeEmpresaCliente localidade_empresaCliente { get; set; }

        [ForeignKey("id_MeiosPagamentoEmpresaCliente")]
        public virtual MeiosPagamento_EmpresaCliente meiosPagamento_empresaCliente { get; set; }

        [ForeignKey("id_CuponDescontoEmpresaCliente")]
        public virtual CupomDesconto_EmpresaCliente cuponDesconto_empresaCliente { get; set; }

        [ForeignKey("id_EmpresaCliente")]
        public virtual EmpresaCliente empresa_cliente { get; set; }


        public virtual ICollection<ProdutosPedidoCliente_EmpresaCliente> produtosPedidoCliente_empresaCliente { get; set; }
    }
}
