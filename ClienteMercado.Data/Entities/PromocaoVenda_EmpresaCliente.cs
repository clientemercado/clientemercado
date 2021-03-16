using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("PromocaoVenda_EmpresaCliente")]
    public partial class PromocaoVenda_EmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_PromocaoVendaEmpresaCliente { get; set; }
        public int id_EmpresaCliente { get; set; }

        [MaxLength(20)]
        public string nomeOferta_PromocaoVendaEmpresaCliente { get; set; }
        public decimal percentualOffOferta_PromocaoVendaEmpresaCliente { get; set; }

        [MaxLength(100)]
        public string bannerOferta_PromocaoVendaEmpresaCliente { get; set; }

        [MaxLength(120)]
        public string linkBuscaOferta_PromocaoVendaEmpresaCliente { get; set; }
        public DateTime dataCadastroOferta_PromocaoVendaEmpresaCliente { get; set; }
        public DateTime dataValidade_PromocaoVendaEmpresaCliente { get; set; }
        public int? idUsuarioCadastrouOferta_PromocaoVendaEmpresaCliente { get; set; }
        public bool ativoInativo_PromocaoVendaEmpresaCliente { get; set; }
        public DateTime? dataInativou_PromocaoVendaEmpresaCliente { get; set; }
        public int? idUsuarioInativou_PromocaoVendaEmpresaCliente { get; set; }


        [ForeignKey("id_EmpresaCliente")]
        public virtual EmpresaCliente empresa_cliente { get; set; }
    }
}
