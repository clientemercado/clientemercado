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
    
    public partial class itens_pedido
    {
        public int ID_CODIGO_ITENS_PEDIDO { get; set; }
        public int ID_CODIGO_PEDIDO { get; set; }
        public int ID_CODIGO_COTACAO_NEGOCIACAO { get; set; }
    
        public virtual cotacao_negociacao cotacao_negociacao { get; set; }
        public virtual pedido pedido { get; set; }
    }
}
