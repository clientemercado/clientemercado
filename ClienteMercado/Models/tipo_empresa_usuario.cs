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
    
    public partial class tipo_empresa_usuario
    {
        public tipo_empresa_usuario()
        {
            this.empresa_usuario = new HashSet<empresa_usuario>();
        }
    
        public int ID_CODIGO_TIPO_EMPRESA_USUARIO { get; set; }
        public string DESCRICAO_TIPO_EMPRESA_USUARIO { get; set; }
        public string FISICA_JURIDICA_EMPRESA_USUARIO { get; set; }
    
        public virtual ICollection<empresa_usuario> empresa_usuario { get; set; }
    }
}
