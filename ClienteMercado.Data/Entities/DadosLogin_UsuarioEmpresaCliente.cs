using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("DadosLogin_UsuarioEmpresaCliente")]
    public partial class DadosLogin_UsuarioEmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_DadosLoginUsuarioEmpresaCliente { get; set; }
        public int id_UsuarioEmpresaCliente { get; set; }

        [MaxLength(10)]
        public string Lg_DadosLoginUsuarioEmpresaCliente { get; set; }

        [MaxLength(10)]
        public string Pw_DadosLoginUsuarioEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string eMail1_DadosLoginUsuarioEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string eMail2_DadosLoginUsuarioEmpresaCliente { get; set; }


        [ForeignKey("id_UsuarioEmpresaCliente")]
        public virtual Usuario_EmpresaCliente usuario_empresaCliente { get; set; }
    }
}
