//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClienteMercado.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bairros_empresa_usuario
    {
        public bairros_empresa_usuario()
        {
            this.enderecos_cep_empresa_usuario = new HashSet<enderecos_cep_empresa_usuario>();
        }
    
        public int ID_CODIGO_BAIRRO_EMPRESA_USUARIO { get; set; }
        public int ID_CODIGO_CIDADE_EMPRESA_USUARIO { get; set; }
        public string NOME_BAIRRO_EMPRESA_USUARIO { get; set; }
    
        public virtual cidades_empresa_usuario cidades_empresa_usuario { get; set; }
        public virtual ICollection<enderecos_cep_empresa_usuario> enderecos_cep_empresa_usuario { get; set; }
    }
}
