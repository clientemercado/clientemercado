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
    
    public partial class usuario_historico_empresas
    {
        public usuario_historico_empresas()
        {
            this.usuario_modulo_historico_empresas = new HashSet<usuario_modulo_historico_empresas>();
        }
    
        public int ID_HISTORICO_USUARIO_EMPRESAS { get; set; }
        public int ID_CODIGO_EMPRESA_USUARIO { get; set; }
        public System.DateTime DATA_INICIO_SISTEMA_USUARIO_EMPRESA { get; set; }
        public Nullable<System.DateTime> DATA_FIM_SISTEMA_USUARIO_EMPRESA { get; set; }
    
        public virtual empresa_usuario empresa_usuario { get; set; }
        public virtual ICollection<usuario_modulo_historico_empresas> usuario_modulo_historico_empresas { get; set; }
    }
}
