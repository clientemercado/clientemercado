﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteMercado.Data.Entities
{
    [Table("cotacao_master_usuario_empresa")]
    public partial class cotacao_master_usuario_empresa
    {
        public cotacao_master_usuario_empresa()
        {
            this.itens_cotacao_usuario_empresa = new List<itens_cotacao_usuario_empresa>();
            this.cotacao_filha = new List<cotacao_filha_usuario_empresa>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_CODIGO_COTACAO_MASTER_USUARIO_EMPRESA { get; set; }

        [Required]
        public int ID_CODIGO_STATUS_COTACAO { get; set; }

        [Required]
        public int ID_CODIGO_EMPRESA { get; set; }

        [Required]
        public int ID_CODIGO_USUARIO { get; set; }

        [Required]
        public int ID_CODIGO_TIPO_COTACAO { get; set; }

        [Required]
        public int ID_CODIGO_TIPO_FRETE { get; set; }

        [Required]
        public int ID_GRUPO_ATIVIDADES { get; set; }

        [MaxLength(30)]
        public string NOME_COTACAO_USUARIO_EMPRESA { get; set; }

        [Required]
        public System.DateTime DATA_CRIACAO_COTACAO_USUARIO_EMPRESA { get; set; }

        public System.DateTime DATA_ENCERRAMENTO_COTACAO_USUARIO_EMPRESA { get; set; }

        [MaxLength(30)]
        public string CONDICAO_PAGAMENTO_COTACAO_USUARIO_EMPRESA { get; set; }

        [MaxLength(200)]
        public string OBSERVACAO_COTACAO_USUARIO_EMPRESA { get; set; }

        [Required]
        public decimal PERCENTUAL_RESPONDIDA_COTACAO_USUARIO_EMPRESA { get; set; }

        [ForeignKey("ID_CODIGO_STATUS_COTACAO")]
        public virtual status_cotacao status_cotacao { get; set; }

        [ForeignKey("ID_CODIGO_EMPRESA")]
        public virtual empresa_usuario empresa_usuario { get; set; }

        [ForeignKey("ID_CODIGO_USUARIO")]
        public virtual usuario_empresa usuario_empresa  { get; set; }

        [ForeignKey("ID_CODIGO_TIPO_COTACAO")]
        public virtual tipos_cotacao tipos_cotacao { get; set; }

        [ForeignKey("ID_CODIGO_TIPO_FRETE")]
        public virtual tipos_frete tipos_frete { get; set; }

        [ForeignKey("ID_GRUPO_ATIVIDADES")]
        public virtual grupo_atividades_empresa grupo_atividades_empresa { get; set; }

        public virtual ICollection<itens_cotacao_usuario_empresa> itens_cotacao_usuario_empresa { get; set; }

        public virtual ICollection<cotacao_filha_usuario_empresa> cotacao_filha { get; set; }
    }
}
