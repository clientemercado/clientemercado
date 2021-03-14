using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("CupomDesconto_EmpresaCliente")]
    public partial class CupomDesconto_EmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_CuponDescontoEmpresaCliente { get; set; }
        public int id_EmpresaCliente { get; set; }
        public DateTime? dataCadastroCupon_CupomDescontoEmpresaCliente { get; set; }
        public int? idUsuarioCadastrouCupon_CupomDescontoEmpresaCliente { get; set; }

        [MaxLength(30)]
        public string nomeCupom_CupomDescontoEmpresaCliente { get; set; }
        public DateTime dataValidade_CupomDescontoEmpresaCliente { get; set; }
        public decimal? percentualDesconto_CupomDescontoEmpresaCliente { get; set; }
        public bool ativoInativo_CupomDescontoEmpresaCliente { get; set; }
        public int? idUsuarioAtivou_CupomDescontoEmpresaCliente { get; set; }
        public DateTime? dataInativou_SubDepartamentoEmpresaCliente { get; set; }
        public int idUsuarioInativou_SubDepartamentoEmpresaCliente { get; set; }


        [ForeignKey("id_EmpresaCliente")]
        public virtual EmpresaCliente empresa_cliente { get; set; }
    }
}
