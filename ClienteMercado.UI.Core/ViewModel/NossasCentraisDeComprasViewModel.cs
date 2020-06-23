using System.Collections.Generic;
using System.Web.Mvc;

namespace ClienteMercado.UI.Core.ViewModel
{
    public class NossasCentraisDeComprasViewModel : NovaCentralDeComprasViewModel
    {
        public int cCC { get; set; }

        public int iCM { get; set; }

        public int iCI { get; set; }

        public int iPCC { get; set; }

        public string inCotacaoAnexada { get; set; }

        public int inCodEmpresaADM { get; set; }

        public int inCodEmpresaLogada { get; set; }

        public string inNomeUsuarioLogado { get; set; }

        public string inConviteAceitoCC { get; set; }

        public string inNomeCotacao { get; set; }

        public string inTipo { get; set; }

        public string nomeCotacaoMaster { get; set; }

        public string inDataSugeridaEncerramentoCotacao { get; set; }

        public string inDataMinimaEncerramentoCotacao { get; set; }

        public string inDataMaximaEncerramentoCotacao { get; set; }

        public int inIDUFEmpresaUsuario { get; set; }

        public List<SelectListItem> inListaDeUFs { get; set; }

        public string telefoneContato { get; set; }

        public List<SelectListItem> inQuantosFornecedores { get; set; }

        public string inCotacaoMasterEnviada { get; set; }

        public int quantidadeEmpresasParticipantesDaCC { get; set; }

        public int quantidadeEmpresasJahAnexaramCotacao { get; set; }

        public int quantosFaltamAnexar { get; set; }

        public string corStatusDaQuantidadeAnexada { get; set; }

        public string statusAlerta { get; set; }

        public int quantosDiasFaltam { get; set; }

        public List<SelectListItem> inListaDeRamosDeAtividadeParaComprasDaCC { get; set; }
    }
}
