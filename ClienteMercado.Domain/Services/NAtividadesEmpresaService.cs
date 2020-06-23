using ClienteMercado.Infra.Repositories;
using ClienteMercado.Data.Entities;

namespace ClienteMercado.Domain.Services
{
    public class NAtividadesEmpresaService
    {
        //Gravar Atividades particulares da Empresa
        public atividades_empresa GravarAtividadeProdutoServicoEmpresa(atividades_empresa obj)
        {
            DAtividadesEmpresaRepository datividadesempresa = new DAtividadesEmpresaRepository();

            return datividadesempresa.GravarAtividadeProdutoServicoEmpresa(obj);
        }
    }
}
