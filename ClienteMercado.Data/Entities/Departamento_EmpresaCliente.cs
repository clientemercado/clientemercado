using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("Departamento_EmpresaCliente")]
    public partial class Departamento_EmpresaCliente
    {
        public Departamento_EmpresaCliente()
        {
            this.subDepto_empresaCliente = new List<SubDepartamento_EmpresaCliente>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_DepartamentoEmpresaCliente { get; set; }
        public int id_EmpresaCliente { get; set; }

        [MaxLength(100)]
        public string descricao_DepartamentoEmpresaCliente { get; set; }
        public bool ativoInativo_DepartamentoEmpresaCliente { get; set; }
        public DateTime? dataInativou_DepartamentoEmpresaCliente { get; set; }
        public int? idUsuarioInativou_DepartamentoEmpresaCliente { get; set; }
        public string imagem_DepartamentoEmpresaCliente { get; set; }


        [ForeignKey("id_EmpresaCliente")]
        public virtual EmpresaCliente empresa_cliente { get; set; }

        public virtual ICollection<SubDepartamento_EmpresaCliente> subDepto_empresaCliente { get; set; }
    }
}
