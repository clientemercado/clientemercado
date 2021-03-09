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
    public class CadastroClientesEmpresaController : Controller
    {
        //
        // GET: /Company/CadastroClientesEmpresa/

        public ActionResult Index()
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
                NUsuarioEmpresaService serviceUsuarioEmpCliente = new NUsuarioEmpresaService();
                Cliente_EmpresaCliente dadosNewUsuarioCliente = new Cliente_EmpresaCliente();
                DadosLogin_ClienteEmpresaCliente dadosLogonUsuarioCliente = new DadosLogin_ClienteEmpresaCliente();

                dadosNewUsuarioCliente.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosNewUsuarioCliente.cpf_ClienteEmpresaCliente = obj.cpf_UsuarioEmpresaCliente.Replace(".", "").Replace("/", "").Replace("-", "");
                dadosNewUsuarioCliente.nome_ClienteEmpresaCliente = obj.nome_UsuarioEmpresaCliente;
                dadosNewUsuarioCliente.eMail1_ClienteEmpresaCliente= obj.eMail1_UsuarioEmpresaCliente;
                dadosNewUsuarioCliente.telefone1_ClienteEmpresaCliente = obj.telefone1_UsuarioEmpresaCliente;
                dadosNewUsuarioCliente.pais_ClienteEmpresaCliente = obj.pais_UsuarioEmpresaCliente;
                dadosNewUsuarioCliente.cepEndereco_ClienteEmpresaCliente= obj.cepEndereco_UsuarioEmpresaCliente.Replace(".", "").Replace("-", "");
                dadosNewUsuarioCliente.endereco_ClienteEmpresaCliente = obj.endereco_UsuarioEmpresaCliente;
                dadosNewUsuarioCliente.complementoEndereco_ClienteEmpresaCliente = obj.complementoEndereco_UsuarioEmpresaCliente;
                dadosNewUsuarioCliente.bairro_ClienteEmpresaCliente = obj.bairro_UsuarioEmpresaCliente;
                dadosNewUsuarioCliente.cidade_ClienteEmpresaCliente= obj.cidade_UsuarioEmpresaCliente;
                dadosNewUsuarioCliente.uf_ClienteEmpresaCliente= obj.uf_UsuarioEmpresaCliente;
                dadosNewUsuarioCliente.receberEmails_ClienteEmpresaCliente = true;
                dadosNewUsuarioCliente.dataCadastro_ClienteEmpresaCliente = DateTime.Now;
                dadosNewUsuarioCliente.ativoInativo_ClienteEmpresaCliente = true;

                //GRAVAR NOVO USUÁRIO CLIENTE da EMPRESA
                dadosNewUsuarioCliente = serviceUsuarioEmpCliente.GravarNovoUsuarioClienteEmpresa(dadosNewUsuarioCliente);

                dadosLogonUsuarioCliente.id_ClienteEmpresaCliente = dadosNewUsuarioCliente.id_ClienteEmpresaCliente;
                dadosLogonUsuarioCliente.Lg_DadosLoginClienteEmpresaCliente = "spmkt" + dadosNewUsuarioCliente.id_ClienteEmpresaCliente;
                dadosLogonUsuarioCliente.Pw_DadosLoginClienteEmpresaCliente = "spmkt" + dadosNewUsuarioCliente.id_ClienteEmpresaCliente;
                dadosLogonUsuarioCliente.eMail1_DadosLoginClienteEmpresaCliente = obj.eMail1_UsuarioEmpresaCliente;

                //GERAR DADOS DE LOGON PARA O USUÁRIO CLIENTE
                serviceUsuarioEmpCliente.GerarDadosLogonUsuarioClienteEmpresa(dadosLogonUsuarioCliente);

                /*
                 OBS: DISPARAR E-MAIL PARA O USUÁRIO INFORMANDO O LOGIN E SENHA...
                 */

                return Json(new { status = "ok", idRegistroGerado = dadosNewUsuarioCliente.id_ClienteEmpresaCliente }, JsonRequestBehavior.AllowGet);
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

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;
                    Cliente_EmpresaCliente dadosUsuarioClienteEmpresa = 
                        serviceUsuEmpresaCliente.ConsultarDadosClienteEmpresa(new Cliente_EmpresaCliente { id_ClienteEmpresaCliente = Convert.ToInt32(id) });

                    dadosDaEmpresaClienteEUsuario.iUCE = dadosUsuarioClienteEmpresa.id_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cpf_UsuarioEmpresaCliente = FormatarCpfCnpj(dadosUsuarioClienteEmpresa.cpf_ClienteEmpresaCliente);
                    dadosDaEmpresaClienteEUsuario.nome_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.nome_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.eMail1_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.eMail1_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.telefone1_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.telefone1_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cepEndereco_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.cepEndereco_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.endereco_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.endereco_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.complementoEndereco_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.complementoEndereco_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.bairro_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.bairro_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cidade_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.cidade_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cpf_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.cpf_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.uf_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.uf_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.pais_UsuarioEmpresaCliente = dadosUsuarioClienteEmpresa.pais_ClienteEmpresaCliente;

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
                NUsuarioEmpresaService serviceUsuarioEmpCliente = new NUsuarioEmpresaService();
                Cliente_EmpresaCliente dadosAlteracaoUsuarioClienteEmpresa = new Cliente_EmpresaCliente();

                dadosAlteracaoUsuarioClienteEmpresa.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosAlteracaoUsuarioClienteEmpresa.id_ClienteEmpresaCliente = obj.id_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioClienteEmpresa.cpf_ClienteEmpresaCliente = obj.cpf_UsuarioEmpresaCliente.Replace(".", "").Replace("/", "").Replace("-", "");
                dadosAlteracaoUsuarioClienteEmpresa.nome_ClienteEmpresaCliente = obj.nome_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioClienteEmpresa.eMail1_ClienteEmpresaCliente = obj.eMail1_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioClienteEmpresa.telefone1_ClienteEmpresaCliente = obj.telefone1_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioClienteEmpresa.pais_ClienteEmpresaCliente = obj.pais_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioClienteEmpresa.cepEndereco_ClienteEmpresaCliente = obj.cepEndereco_UsuarioEmpresaCliente.Replace(".", "").Replace("-", "");
                dadosAlteracaoUsuarioClienteEmpresa.endereco_ClienteEmpresaCliente = obj.endereco_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioClienteEmpresa.complementoEndereco_ClienteEmpresaCliente = obj.complementoEndereco_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioClienteEmpresa.bairro_ClienteEmpresaCliente = obj.bairro_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioClienteEmpresa.cidade_ClienteEmpresaCliente = obj.cidade_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioClienteEmpresa.uf_ClienteEmpresaCliente = obj.uf_UsuarioEmpresaCliente;
                //dadosAlteracaoUsuarioClienteEmpresa.receberEmails_UsuarioEmpresaCliente = true;
                //dadosAlteracaoUsuarioClienteEmpresa.dataCadastro_UsuarioEmpresaCliente = DateTime.Now;
                //dadosAlteracaoUsuarioClienteEmpresa.ativoInativo_UsuarioEmpresaCliente = true;

                //ALTERAR DADOS DO USUÁRIO CIENTE da EMPRESA
                serviceUsuarioEmpCliente.AlterarDadosUsuarioClienteEmpresa(dadosAlteracaoUsuarioClienteEmpresa);

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
    }
}
