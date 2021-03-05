using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("SubDepartamento_EmpresaCliente")]
    public partial class SubDepartamento_EmpresaCliente
    {
        public SubDepartamento_EmpresaCliente()
        {
            this.produto_empresaCliente = new List<Produto_EmpresaCliente>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_SubDepartamentoEmpresaCliente { get; set; }
        public int id_DepartamentoEmpresaCliente { get; set; }

        [MaxLength(100)]
        public string descricao_SubDepartamentoEmpresaCliente { get; set; }
        public bool ativoInativo_SubDepartamentoEmpresaCliente { get; set; }
        public DateTime? dataInativou_SubDepartamentoEmpresaCliente { get; set; }
        public int? idUsuarioInativou_SubDepartamentoEmpresaCliente { get; set; }


        [ForeignKey("id_DepartamentoEmpresaCliente")]
        public virtual Departamento_EmpresaCliente departamento_empresaCliente { get; set; }

        public virtual ICollection<Produto_EmpresaCliente> produto_empresaCliente { get; set; }
    }
}
