using ClienteMercado.Infra.Repositories;
using ClienteMercado.Data.Entities;

namespace ClienteMercado.Domain.Services
{
    public class NEmpresaUsuarioLoginsService
    {
        DEmpresaUsuarioLoginsRepository dRepository = new DEmpresaUsuarioLoginsRepository();

        //CONSULTAR DADOS de CONTATO do USUÁRIO
        public empresa_usuario_logins ConsultarDadosDeContatoDoUsuario(int idUsuario)
        {
            return dRepository.ConsultarDadosDeContatoDoUsuario(idUsuario);
        }
    }
}
