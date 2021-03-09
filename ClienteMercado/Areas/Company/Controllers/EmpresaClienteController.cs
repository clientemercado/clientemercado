using ClienteMercado.Data.Entities;
using ClienteMercado.Domain.Services;
using ClienteMercado.UI.Core.ViewModel;
using ClienteMercado.Utils.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClienteMercado.Areas.Company.Controllers
{
    public class EmpresaClienteController : Controller
    {
        //
        // GET: /Company/EmpresaCliente/

        public ActionResult Index()
        {
            try
            {
                if (Sessao.IdEmpresaUsuario > 0)
                {
                    //----------------------------------------------------------------------------------------------------------------
                    //TRECHO ESSENCIAL PRA EIBIÇÃO DOS DADOS DA EMPRESA CLIENTE E USUÁRIO LOGADOS
                    NEmpresaUsuarioService serviceEmpresaCliente = new NEmpresaUsuarioService();
                    NUsuarioEmpresaService serviceUsuEmpresaCliente = new NUsuarioEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaCliente =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaCliente =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeFantasia_EmpresaCliente = dadosEmpresaCliente.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nome_UsuarioEmpresaCliente = dadosUsuEmpresaCliente.nome_UsuarioEmpresaCliente;
                    //----------------------------------------------------------------------------------------------------------------

                    return View(dadosDaEmpresaClienteEUsuario);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return null;
        }

        public ActionResult Cadastro()
        {
            try
            {
                if (Session["IdUsuarioLogado"] != null)
                {
                    DateTime dataHoje = DateTime.Today;

                    //Montando o dia por extenso a se exibido no site (* Não se usa mais isso)
                    string diaDaSemana = new CultureInfo("pt-BR").DateTimeFormat.GetDayName(dataHoje.DayOfWeek);
                    diaDaSemana = char.ToUpper(diaDaSemana[0]) + diaDaSemana.Substring(1);

                    int diaDoMes = dataHoje.Day;
                    string mesAtual = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(dataHoje.Month);
                    mesAtual = char.ToUpper(mesAtual[0]) + mesAtual.Substring(1);
                    int anoAtual = dataHoje.Year;

                    //----------------------------------------------------------------------------------------------------------------
                    //TRECHO ESSENCIAL PRA EIBIÇÃO DOS DADOS DA EMPRESA CLIENTE E USUÁRIO LOGADOS
                    NEmpresaUsuarioService serviceEmpresaCliente = new NEmpresaUsuarioService();
                    NUsuarioEmpresaService serviceUsuEmpresaCliente = new NUsuarioEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.ListagemPaises = ListagemPaises();
                    dadosDaEmpresaClienteEUsuario.ListagemEstados = ListagemEstados();
                    //----------------------------------------------------------------------------------------------------------------

                    //VIEWBAGS
                    ViewBag.dataHoje = diaDaSemana + ", " + diaDoMes + " de " + mesAtual + " de " + anoAtual;
                    ViewBag.ondeEstouAgora = "";

                    return View(dadosDaEmpresaClienteEUsuario);
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { area = "" });
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //----------------------------------------------------------------------------------
        //GRAVAR REGISTRO
        public JsonResult GravarRegistro(DadosEmpresaClienteViewModel obj)
        {
            try
            {
                NEmpresaUsuarioService serviceEmpresaCliente = new NEmpresaUsuarioService();
                EmpresaCliente dadosNewEmpresaCliente = new EmpresaCliente();

                dadosNewEmpresaCliente.cnpj_EmpresaCliente = obj.cnpj_EmpresaCliente.Replace(".", "").Replace("/","").Replace("-","");
                dadosNewEmpresaCliente.razaoSocial_EmpresaCliente = obj.razaoSocial_EmpresaCliente;
                dadosNewEmpresaCliente.nomeFantasia_EmpresaCliente = obj.nomeFantasia_EmpresaCliente;
                dadosNewEmpresaCliente.email1_EmpresaCliente = obj.email1_EmpresaCliente;
                dadosNewEmpresaCliente.telefone1_EmpresaCliente = obj.telefone1_EmpresaCliente;
                dadosNewEmpresaCliente.pais_EmpresaCliente = obj.pais_EmpresaCliente;
                dadosNewEmpresaCliente.cepEndereco_EmpresaCliente = Convert.ToInt64(obj.cepEndereco_EmpresaCliente.ToString().Replace(".","").Replace("-",""));
                dadosNewEmpresaCliente.endereco_EmpresaCliente = obj.endereco_EmpresaCliente;
                dadosNewEmpresaCliente.complementoEndereco_EmpresaCliente = obj.complementoEndereco_EmpresaCliente;
                dadosNewEmpresaCliente.bairro_EmpresaCliente = obj.bairro_EmpresaCliente;
                dadosNewEmpresaCliente.cidade_EmpresaCliente = obj.cidade_EmpresaCliente;
                dadosNewEmpresaCliente.uf_EmpresaCliente = obj.uf_EmpresaCliente;

                //GRAVAR NOVA EMPRESA CLIENTE
                dadosNewEmpresaCliente = serviceEmpresaCliente.GravarNovaEmpresaCliente(dadosNewEmpresaCliente);

                return Json(new { status = "ok", idRegistroGerado = dadosNewEmpresaCliente.id_EmpresaCliente }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        public ActionResult AlterarDados(int id)
        {
            try
            {
                if (Session["IdUsuarioLogado"] != null)
                {
                    DateTime dataHoje = DateTime.Today;

                    //Montando o dia por extenso a se exibido no site (* Não se usa mais isso)
                    string diaDaSemana = new CultureInfo("pt-BR").DateTimeFormat.GetDayName(dataHoje.DayOfWeek);
                    diaDaSemana = char.ToUpper(diaDaSemana[0]) + diaDaSemana.Substring(1);

                    int diaDoMes = dataHoje.Day;
                    string mesAtual = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(dataHoje.Month);
                    mesAtual = char.ToUpper(mesAtual[0]) + mesAtual.Substring(1);
                    int anoAtual = dataHoje.Year;

                    //----------------------------------------------------------------------------------------------------------------
                    //TRECHO ESSENCIAL PRA EIBIÇÃO DOS DADOS DA EMPRESA CLIENTE E USUÁRIO LOGADOS
                    NEmpresaUsuarioService serviceEmpresaCliente = new NEmpresaUsuarioService();
                    NUsuarioEmpresaService serviceUsuEmpresaCliente = new NUsuarioEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });
                    EmpresaCliente dadosEmpresaCliente = 
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(id) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.nomeFantasia_EmpresaCliente = dadosEmpresaCliente.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.cnpj_EmpresaCliente = dadosEmpresaCliente.cnpj_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.razaoSocial_EmpresaCliente = dadosEmpresaCliente.razaoSocial_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.logomarca_EmpresaCliente = dadosEmpresaCliente.logomarca_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.endereco_EmpresaCliente = dadosEmpresaCliente.endereco_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.bairro_EmpresaCliente = dadosEmpresaCliente.bairro_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.complementoEndereco_EmpresaCliente = dadosEmpresaCliente.complementoEndereco_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cepEndereco_EmpresaCliente = dadosEmpresaCliente.cepEndereco_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.pais_EmpresaCliente = dadosEmpresaCliente.pais_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.telefone1_EmpresaCliente = dadosEmpresaCliente.telefone1_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.telefone2_EmpresaCliente = dadosEmpresaCliente.telefone2_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.email1_EmpresaCliente = dadosEmpresaCliente.email1_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.email2_EmpresaCliente = dadosEmpresaCliente.email2_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.receberEmails_EmpresaCliente = dadosEmpresaCliente.receberEmails_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.aceitacaoTermosPolitica_EmpresaCliente = dadosEmpresaCliente.aceitacaoTermosPolitica_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.dataCadastro_EmpresaCliente = dadosEmpresaCliente.dataCadastro_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.ativaInativa_EmpresaCliente = dadosEmpresaCliente.ativaInativa_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.ListagemPaises = ListagemPaises();
                    dadosDaEmpresaClienteEUsuario.ListagemEstados = ListagemEstados();
                    //----------------------------------------------------------------------------------------------------------------

                    //VIEWBAGS
                    ViewBag.dataHoje = diaDaSemana + ", " + diaDoMes + " de " + mesAtual + " de " + anoAtual;
                    ViewBag.ondeEstouAgora = "";

                    return View(dadosDaEmpresaClienteEUsuario);
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { area = "" });
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //----------------------------------------------------------------------------------
        //ATUALIZAR REGISTRO
        public JsonResult AtualizarRegistro(DadosEmpresaClienteViewModel obj)
        {
            try
            {
                //string saldoAtualizado = "";

                //AtividadeService serviceAtividade = new AtividadeService();
                //Data.Entities.Atividade novaAtividade = new Data.Entities.Atividade();

                ////POPULAR MODELO P/ GRAVAÇÃO
                //novaAtividade.AtiIndice = novoIndiceAtividades(0, 1, obj.orcCodItemContrato, obj.frenteServFilho);

                //if (obj.AtiCodigoPai > 0)
                //{
                //    novaAtividade.AtiCodigoPai = obj.AtiCodigoPai;
                //}

                //novaAtividade.AtiDescricao = obj.descAtivPrincPai;
                //novaAtividade.AtiQtda = Convert.ToDouble(obj.quantidadeNew);
                //novaAtividade.AtiUnidade = obj.unidAtivPrinc;
                //novaAtividade.ATISALDO = Convert.ToDecimal(obj.saldoApurado);
                //novaAtividade.ATIPREV = Convert.ToDecimal(obj.previstoAtivPrinc);
                //novaAtividade.ATIFATOR = Convert.ToDecimal(obj.fatorXPrinc);
                //novaAtividade.FreSerCodigo = obj.frenteServFilho;
                //novaAtividade.CenCusCodigo = obj.CenCusCodigo;
                //novaAtividade.OrcCodigo = obj.orcCodItemContrato;
                //novaAtividade.ConEmpCodigo = obj.ConEmpCodigo;
                //novaAtividade.OrcSerIndice = obj.indiceItemContrato;
                //novaAtividade.ORCSERCODIGO = Convert.ToInt32(obj.codServItemContrato);
                //novaAtividade.EmpCodigo = idEmpresa;

                ////GRAVAR NOVA AVIVIDADE 
                //novaAtividade = serviceAtividade.GravarNovaAtividade(novaAtividade);

                ////CARREGAR ULTIMA ATIVIDADE FILHA REGISTRADA - PEGAR SALDO
                //ListaDeAtividadesViewModel ultimaAtividadeFilhaRegs = serviceAtividade.BuscarUltimaAtividadeFilhaRegistrada(novaAtividade.AtiCodigo);

                //if (ultimaAtividadeFilhaRegs != null)
                //{
                //    saldoAtualizado = ultimaAtividadeFilhaRegs.ATISALDO.ToString();
                //}

                return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        //Carrega lista de Países atendidos pelo ClienteMercado
        private static List<SelectListItem> ListagemPaises()
        {
            //Buscar lista de Países
            NPaisesService negocioPaises = new NPaisesService();

            List<paises_empresa_usuario> listaPaises = negocioPaises.ListaPaises();

            List<SelectListItem> listPaises = new List<SelectListItem>();

            listPaises.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var grupoPaises in listaPaises)
            {
                listPaises.Add(new SelectListItem
                {
                    Text = grupoPaises.PAIS_EMPRESA_USUARIO,
                    Value = grupoPaises.ID_PAISES_EMPRESA_USUARIO.ToString()
                });
            }

            return listPaises;
        }

        //Carrega a lista de Estados (Obs: No momento carregrá todos os estados brasileiros. Depois vejo como ficará)
        private List<SelectListItem> ListagemEstados()
        {
            //Buscar lista de Estados brasileiros
            NEstadosService negocioEstados = new NEstadosService();

            List<estados_empresa_usuario> listaEstados = negocioEstados.ListaEstados();

            List<SelectListItem> listEstados = new List<SelectListItem>();

            listEstados.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var grupoEstados in listaEstados)
            {
                listEstados.Add(new SelectListItem
                {
                    Text = grupoEstados.UF_EMPRESA_USUARIO,
                    Value = grupoEstados.ID_ESTADOS_EMPRESA_USUARIO.ToString()
                });
            }

            return listEstados;
        }

        //FORMATAR CPF/CNPJ
        public static string FormatarCpfCnpj(string strCpfCnpj)
        {
            if (strCpfCnpj.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(strCpfCnpj, 11));

                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(strCpfCnpj, 11));

                return mtpCnpj.ToString();
            }
        }

        //ACRESCENTA ZEROS À ESQUERDA
        public static string ZerosEsquerda(string strString, int intTamanho)
        {
            string strResult = "";

            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }

            return strResult + strString;
        }

    }
}
