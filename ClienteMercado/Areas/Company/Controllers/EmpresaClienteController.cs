using ClienteMercado.Data.Entities;
using ClienteMercado.Domain.Services;
using ClienteMercado.UI.Core.ViewModel;
using ClienteMercado.Utils.Net;
using ClienteMercado.Utils.ViewModel;
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

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;
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
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
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
                dadosNewEmpresaCliente.cepEndereco_EmpresaCliente = obj.cepEndereco_EmpresaCliente.Replace(".","").Replace("-","");
                dadosNewEmpresaCliente.endereco_EmpresaCliente = obj.endereco_EmpresaCliente;
                dadosNewEmpresaCliente.complementoEndereco_EmpresaCliente = obj.complementoEndereco_EmpresaCliente;
                dadosNewEmpresaCliente.bairro_EmpresaCliente = obj.bairro_EmpresaCliente;
                dadosNewEmpresaCliente.cidade_EmpresaCliente = obj.cidade_EmpresaCliente;
                dadosNewEmpresaCliente.uf_EmpresaCliente = obj.uf_EmpresaCliente;
                dadosNewEmpresaCliente.receberEmails_EmpresaCliente = true;
                dadosNewEmpresaCliente.aceitacaoTermosPolitica_EmpresaCliente = true;
                dadosNewEmpresaCliente.dataCadastro_EmpresaCliente = DateTime.Now;
                dadosNewEmpresaCliente.ativaInativa_EmpresaCliente = true;

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
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.iEC = dadosEmpresaCliente.id_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.nomeFantasia_EmpresaCliente = dadosEmpresaCliente.nomeFantasia_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cnpj_EmpresaCliente = FormatarCpfCnpj(dadosEmpresaCliente.cnpj_EmpresaCliente);
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
                    dadosDaEmpresaClienteEUsuario.cidade_EmpresaCliente = dadosEmpresaCliente.cidade_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.uf_EmpresaCliente = dadosEmpresaCliente.uf_EmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.pais_EmpresaCliente = dadosEmpresaCliente.pais_EmpresaCliente;
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
                NEmpresaUsuarioService serviceEmpresaCliente = new NEmpresaUsuarioService();
                EmpresaCliente dadosAlteracaoEmpresaCliente = new EmpresaCliente();

                dadosAlteracaoEmpresaCliente.id_EmpresaCliente = obj.iEC;
                dadosAlteracaoEmpresaCliente.razaoSocial_EmpresaCliente = obj.razaoSocial_EmpresaCliente;
                dadosAlteracaoEmpresaCliente.nomeFantasia_EmpresaCliente = obj.nomeFantasia_EmpresaCliente;
                dadosAlteracaoEmpresaCliente.email1_EmpresaCliente = obj.email1_EmpresaCliente;
                dadosAlteracaoEmpresaCliente.telefone1_EmpresaCliente = obj.telefone1_EmpresaCliente;
                dadosAlteracaoEmpresaCliente.pais_EmpresaCliente = obj.pais_EmpresaCliente;
                dadosAlteracaoEmpresaCliente.cepEndereco_EmpresaCliente = obj.cepEndereco_EmpresaCliente.Replace(".", "").Replace("-", "");
                dadosAlteracaoEmpresaCliente.endereco_EmpresaCliente = obj.endereco_EmpresaCliente;
                dadosAlteracaoEmpresaCliente.complementoEndereco_EmpresaCliente = obj.complementoEndereco_EmpresaCliente;
                dadosAlteracaoEmpresaCliente.bairro_EmpresaCliente = obj.bairro_EmpresaCliente;
                dadosAlteracaoEmpresaCliente.cidade_EmpresaCliente = obj.cidade_EmpresaCliente;
                dadosAlteracaoEmpresaCliente.uf_EmpresaCliente = obj.uf_EmpresaCliente;
                //dadosAlteracaoEmpresaCliente.receberEmails_EmpresaCliente = true;
                //dadosAlteracaoEmpresaCliente.aceitacaoTermosPolitica_EmpresaCliente = true;
                //dadosAlteracaoEmpresaCliente.dataCadastro_EmpresaCliente = DateTime.Now;
                //dadosAlteracaoEmpresaCliente.ativaInativa_EmpresaCliente = true;

                //ALTERAR DADOS EMPRESA CLIENTE
                serviceEmpresaCliente.AlterarDadosEmpresaCliente(dadosAlteracaoEmpresaCliente);

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
                    Value = grupoPaises.PAIS_EMPRESA_USUARIO
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
                    Value = grupoEstados.UF_EMPRESA_USUARIO
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

        public ActionResult BuscarListaEmpresasCliente(string descricaoFiltro)
        {
            try
            {
                NEmpresaUsuarioService serviceEmpresaCliente = new NEmpresaUsuarioService();

                List<ListaEmpresasClientesViewModel> listaEmpresasCliente = serviceEmpresaCliente.BuscarListaDeEmpresasClientes();

                if (String.IsNullOrEmpty(descricaoFiltro) == false)
                {
                    listaEmpresasCliente = listaEmpresasCliente.Where(m => (m.nomeFantasiaEmpresa.ToUpper().Contains(descricaoFiltro.ToUpper()))).ToList();
                }

                for (int i = 0; i < listaEmpresasCliente.Count; i++)
                {
                    listaEmpresasCliente[i].idEmpresa = listaEmpresasCliente[i].id_EmpresaCliente.ToString();
                    listaEmpresasCliente[i].dataCadastroEmpresa = Convert.ToDateTime(listaEmpresasCliente[i].dataCadastro_EmpresaCliente).ToString("dd/MM/yyyy");
                    listaEmpresasCliente[i].ativaInativaEmpresa = listaEmpresasCliente[i].ativaInativa_EmpresaCliente ? "Sim" : "Não";
                }

                return Json(
                    new
                    {
                        rows = listaEmpresasCliente,
                        current = 1,
                        rowCount = listaEmpresasCliente.Count,
                        total = listaEmpresasCliente.Count,
                        dadosCarregados = "Ok"
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
