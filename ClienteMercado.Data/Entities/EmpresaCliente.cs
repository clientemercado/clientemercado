using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("EmpresaCliente")]
    public partial class EmpresaCliente
    {
        public EmpresaCliente()
        {
            this.usuario_empresaCliente = new List<Usuario_EmpresaCliente>();
            this.cliente_empresaCliente = new List<Cliente_EmpresaCliente>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_EmpresaCliente { get; set; }

        [MaxLength(15)]
        public string cnpj_EmpresaCliente { get; set; }

        [MaxLength(100)]
        public string razaoSocial_EmpresaCliente { get; set; }

        [MaxLength(100)]
        public string nomeFantasia_EmpresaCliente { get; set; }

        [MaxLength(100)]
        public string logomarca_EmpresaCliente { get; set; }

        [MaxLength(150)]
        public string endereco_EmpresaCliente { get; set; }

        [MaxLength(50)]
        public string bairro_EmpresaCliente { get; set; }

        [MaxLength(100)]
        public string complementoEndereco_EmpresaCliente { get; set; }

        public Int64 cepEndereco_EmpresaCliente { get; set; }

        [MaxLength(15)]
        public string pais_EmpresaCliente { get; set; }

        [MaxLength(15)]
        public string telefone1_EmpresaCliente { get; set; }

        [MaxLength(15)]
        public string telefone2_EmpresaCliente { get; set; }

        [MaxLength(50)]
        public string email1_EmpresaCliente { get; set; }

        [MaxLength(50)]
        public string email2_EmpresaCliente { get; set; }
        public bool receberEmails_EmpresaCliente { get; set; }
        public bool aceitacaoTermosPolitica_EmpresaCliente { get; set; }
        public DateTime? dataCadastro_EmpresaCliente { get; set; }
        public bool ativaInativa_EmpresaCliente { get; set; }
        public DateTime? dataInativou_EmpresaCliente { get; set; }
        public int? idUsuarioInativou_EmpresaCliente { get; set; }
        public float? latitude_logitude_cep_EmpresaCliente { get; set; }
        public string cidade_EmpresaCliente { get; set; }
        public string uf_EmpresaCliente { get; set; }

        public virtual ICollection<Usuario_EmpresaCliente> usuario_empresaCliente { get; set; }
        public virtual ICollection<Cliente_EmpresaCliente> cliente_empresaCliente { get; set; }
    }
}
