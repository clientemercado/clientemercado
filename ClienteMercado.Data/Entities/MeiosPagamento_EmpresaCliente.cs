using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("MeiosPagamento_EmpresaCliente")]
    public partial class MeiosPagamento_EmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_MeiosPagamentoEmpresaCliente { get; set; }

        [MaxLength(15)]
        public string descricao_MeiosPagamentoEmpresaCliente { get; set; }
    }
}
