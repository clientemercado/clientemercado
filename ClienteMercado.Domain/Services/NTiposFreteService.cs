using ClienteMercado.Infra.Repositories;
using ClienteMercado.Data.Entities;
using System.Collections.Generic;

namespace ClienteMercado.Domain.Services
{
    public class NTiposFreteService
    {
        DTiposFreteRepository dtiposdecotacao = new DTiposFreteRepository();

        //Consultar os TIPOS de FRETE
        public List<tipos_frete> ListaDeTiposDeFrete()
        {
            return dtiposdecotacao.ListaDeTiposDeFrete();
        }
    }
}
