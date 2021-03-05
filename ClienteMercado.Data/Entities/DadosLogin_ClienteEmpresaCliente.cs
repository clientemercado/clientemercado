using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("DadosLogin_ClienteEmpresaCliente")]
    public partial class DadosLogin_ClienteEmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_DadosLoginClienteEmpresaCliente { get; set; }
        public int id_ClienteEmpresaCliente { get; set; }

        [MaxLength(10)]
        public string Lg_DadosLoginClienteEmpresaCliente { get; set; }

        [MaxLength(10)]
        public string Pw_DadosLoginClienteEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string eMail1_DadosLoginClienteEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string eMail2_DadosLoginClienteEmpresaCliente { get; set; }


        [ForeignKey("id_ClienteEmpresaCliente")]
        public virtual Cliente_EmpresaCliente ciente_empresaCliente { get; set; }
    }
}
