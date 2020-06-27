using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;

namespace ClienteMercado.Domain.Services
{
    public class NCnpjService
    {
        DCnpjRepository dcnpj = new DCnpjRepository();

        //Consulta CNPJ da Empresa
        public empresa_usuario ConsultarCnpj(empresa_usuario obj)
        {
            return dcnpj.ConsultarCnpj(obj);
        }

    }
}
