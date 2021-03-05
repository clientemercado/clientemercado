using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("Usuario_EmpresaCliente")]
    public partial class Usuario_EmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_UsuarioEmpresaCliente { get; set; }

        public int id_EmpresaCliente { get; set; }

        [MaxLength(15)]
        public string cpf_UsuarioEmpresaCliente { get; set; }

        [MaxLength(100)]
        public string nome_UsuarioEmpresaCliente { get; set; }

        [MaxLength(150)]
        public string endereco_UsuarioEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string bairro_UsuarioEmpresaCliente { get; set; }

        [MaxLength(100)]
        public string complementoEndereco_UsuarioEmpresaCliente { get; set; }

        public Int64 cepEndereco_UsuarioEmpresaCliente { get; set; }

        [MaxLength(15)]
        public string pais_UsuarioEmpresaCliente { get; set; }

        [MaxLength(15)]
        public string telefone1_UsuarioEmpresaCliente { get; set; }

        [MaxLength(15)]
        public string telefone2_UsuarioEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string eMail1_UsuarioEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string eMail2_UsuarioEmpresaCliente { get; set; }
        public bool receberEmails_UsuarioEmpresaCliente { get; set; }
        public DateTime? dataCadastro_UsuarioEmpresaCliente { get; set; }
        public bool ativoInativo_UsuarioEmpresaCliente { get; set; }
        public DateTime? dataInativou_UsuarioEmpresaCliente { get; set; }
        public int? idUsuarioInativou_UsuarioEmpresaCliente { get; set; }
        public float? latitude_logitude_cep_UsuarioEmpresaCliente { get; set; }


        [ForeignKey("id_EmpresaCliente")]
        public virtual EmpresaCliente empresa_cliente { get; set; }
    }
}
