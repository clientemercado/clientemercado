using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("ProdutosPedidoCliente_EmpresaCliente")]
    public partial class ProdutosPedidoCliente_EmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_ProdutosPedidoCliente { get; set; }
        public int id_PedidoClienteEmpresaCliente { get; set; }
        public int id_ProdutoEmpresaCliente { get; set; }
        public bool itemPedidoEntregue_ProdutosPedidoCliente { get; set; }

        [MaxLength(200)]
        public string motivoItemPedidoNaoEntregue_ProdutosPedidoCliente { get; set; }
        public DateTime? dataEntregaItemPedido_ProdutosPedidoCliente { get; set; }
        public int quantidade_ProdutosPedidoCliente { get; set; }
        public decimal valorUnitario_ProdutosPedidoCliente { get; set; }


        [ForeignKey("id_PedidoClienteEmpresaCliente")]
        public virtual PedidoCliente_EmpresaCliente pedidoCliente_empresaCliente { get; set; }

        [ForeignKey("id_ProdutoEmpresaCliente")]
        public virtual Produto_EmpresaCliente produto_empresaCliente { get; set; }
    }
}
