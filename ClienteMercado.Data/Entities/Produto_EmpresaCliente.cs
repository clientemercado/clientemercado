using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("Produto_EmpresaCliente")]
    public partial class Produto_EmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_ProdutoEmpresaCliente { get; set; }
        public int id_SubDepartamentoEmpresaCliente { get; set; }
        public int id_EmpresaCliente { get; set; }
        public int id_EmpresaFabricantesMarcas { get; set; }
        public int id_PromocaoVendaEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string descricao_ProdutoEmpresaCliente { get; set; }

        [MaxLength(10)]
        public string tipoEmbalagem_ProdutoEmpresaCliente { get; set; }
        public decimal pesoEmbalagem_ProdutoEmpresaCliente { get; set; }

        [MaxLength(10)]
        public string unidadePesoEmbalagem_ProdutoEmpresaCliente { get; set; }

        [MaxLength(20)]
        public string idImportacao_ProdutoEmpresaCliente { get; set; }
        public decimal valorVenda_ProdutoEmpresaCliente { get; set; }
        public decimal? valorVendaAnterior_ProdutoEmpresaCliente { get; set; }
        public bool ativoInativo_ProdutoEmpresaCliente { get; set; }
        public DateTime? dataInativou_ProdutoEmpresaCliente { get; set; }
        public int? idUsuarioInativou_ProdutoEmpresaCliente { get; set; }


        [ForeignKey("id_SubDepartamentoEmpresaCliente")]
        public virtual SubDepartamento_EmpresaCliente subDepartamento_empresaCliente { get; set; }

        [ForeignKey("id_EmpresaCliente")]
        public virtual EmpresaCliente empresa_cliente { get; set; }

        [ForeignKey("id_EmpresaFabricantesMarcas")]
        public virtual Empresa_FabricantesMarcas empresa_fabricantesMarcas { get; set; }

        [ForeignKey("id_PromocaoVendaEmpresaCliente")]
        public virtual PromocaoVenda_EmpresaCliente promocaoVenda_empresaCliente{ get; set; }
    }
}
