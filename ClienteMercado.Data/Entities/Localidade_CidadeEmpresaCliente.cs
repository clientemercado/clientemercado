using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("Localidade_CidadeEmpresaCliente")]
    public partial class Localidade_CidadeEmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_LocalidadeCidadeEmpresaCliente { get; set; }
        public int id_CidadeEmpresaCliente { get; set; }

        [MaxLength(100)]
        public string nomeLocalidade_LocalidadeCidadeEmpresaCliente { get; set; }
        public float? latitude_logitude_cep_UsuarioEmpresaCliente { get; set; }

        [ForeignKey("id_CidadeEmpresaCliente")]
        public virtual Cidade_EmpresaCliente cidade_empresaCliente { get; set; }
    }
}
