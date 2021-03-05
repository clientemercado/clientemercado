using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("Empresa_FabricantesMarcas")]
    public partial class Empresa_FabricantesMarcas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_EmpresaFabricantesMarcas { get; set; }

        [MaxLength(100)]
        public string descricao_EmpresaFabricantesMarcas { get; set; }
    }
}
