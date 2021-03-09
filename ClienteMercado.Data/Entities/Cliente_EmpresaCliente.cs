using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("Cliente_EmpresaCliente")]
    public partial class Cliente_EmpresaCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_ClienteEmpresaCliente { get; set; }
        public int id_EmpresaCliente { get; set; }

        [MaxLength(15)]
        public string cpf_ClienteEmpresaCliente { get; set; }

        [MaxLength(100)]
        public string nome_ClienteEmpresaCliente { get; set; }

        [MaxLength(150)]
        public string endereco_ClienteEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string bairro_ClienteEmpresaCliente { get; set; }

        [MaxLength(100)]
        public string complementoEndereco_ClienteEmpresaCliente { get; set; }

        public string cepEndereco_ClienteEmpresaCliente { get; set; }

        [MaxLength(15)]
        public string pais_ClienteEmpresaCliente { get; set; }

        [MaxLength(15)]
        public string telefone1_ClienteEmpresaCliente { get; set; }

        [MaxLength(15)]
        public string telefone2_ClienteEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string eMail1_ClienteEmpresaCliente { get; set; }

        [MaxLength(50)]
        public string eMail2_ClienteEmpresaCliente { get; set; }
        public bool receberEmails_ClienteEmpresaCliente { get; set; }
        public DateTime? dataCadastro_ClienteEmpresaCliente { get; set; }
        public bool ativoInativo_ClienteEmpresaCliente { get; set; }
        public DateTime? dataInativou_ClienteEmpresaCliente { get; set; }
        public int? idUsuarioInativou_ClienteEmpresaCliente { get; set; }
        public float? latitude_logitude_cep_UsuarioEmpresaCliente { get; set; }
        public string cidade_ClienteEmpresaCliente { get; set; }
        public string uf_ClienteEmpresaCliente { get; set; }


        [ForeignKey("id_EmpresaCliente")]
        public virtual EmpresaCliente empresa_cliente { get; set; }
    }
}
