using ClienteMercado.Data.Contexto;
using ClienteMercado.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ClienteMercado.Infra.Repositories
{
    public class DTiposFreteRepository
    {
        //Consultar os TIPOS de FRETE
        public List<tipos_frete> ListaDeTiposDeFrete()
        {
            using (cliente_mercadoContext _contexto = new cliente_mercadoContext())
            {
                List<tipos_frete> dadosDosTiposDeFrete = _contexto.tipos_frete.ToList();

                return dadosDosTiposDeFrete;
            }
        }
    }
}
