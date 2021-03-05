using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("Cidade_EmpresaCliente")]
    public partial class Cidade_EmpresaCliente
    {
        public Cidade_EmpresaCliente()
        {
            this.localidadeCidade_empresaCliente = new List<Localidade_CidadeEmpresaCliente>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_CidadeEmpresaCliente { get; set; }

        [MaxLength(100)]
        public string cidade_CidadeEmpresaCliente { get; set; }


        public virtual ICollection<Localidade_CidadeEmpresaCliente> localidadeCidade_empresaCliente { get; set; }
    }
}
