using ClienteMercado.Infra.Repositories;
using ClienteMercado.Data.Entities;

namespace ClienteMercado.Domain.Services
{
    public class NCardsUsuarioProfissionalService
    {
        //Gravando dados do cartão de crédito do usuário profissional de serviços
        public cards_usuario_profissional GravarDadosCartaoUsuarioProfissional(cards_usuario_profissional obj)
        {
            DCardsUsuarioProfissionalRepository dcardsusuarioprofissional = new DCardsUsuarioProfissionalRepository();

            return dcardsusuarioprofissional.GravarDadosCartaoUsuarioProfissional(obj);
        }
    }
}
