using System.Linq;
using ClienteMercado.Data.Contexto;
using ClienteMercado.Data.Entities;

namespace ClienteMercado.Infra.Repositories
{
    public class DCnpjRepository
    {
        public empresa_usuario ConsultarCnpj(empresa_usuario obj)
        {
            //Consulta CNPJ da Empresa
            using (cliente_mercadoContext _contexto = new cliente_mercadoContext())
            {
                empresa_usuario cnpj =
                    _contexto.empresa_usuario.FirstOrDefault(
                        m => m.CNPJ_EMPRESA_USUARIO.Equals(obj.CNPJ_EMPRESA_USUARIO));

                return cnpj;
            }
        }
    }
}
