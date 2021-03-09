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
    public class UsuarioEmpresaClienteController : Controller
    {
        //
        // GET: /Company/UsuarioEmpresaCliente/

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
                Usuario_EmpresaCliente dadosNewUsuarioEmpCliente = new Usuario_EmpresaCliente();
                DadosLogin_UsuarioEmpresaCliente dadosLogonUsuario = new DadosLogin_UsuarioEmpresaCliente();

                dadosNewUsuarioEmpCliente.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosNewUsuarioEmpCliente.cpf_UsuarioEmpresaCliente = obj.cpf_UsuarioEmpresaCliente.Replace(".", "").Replace("/", "").Replace("-", "");
                dadosNewUsuarioEmpCliente.nome_UsuarioEmpresaCliente = obj.nome_UsuarioEmpresaCliente;
                dadosNewUsuarioEmpCliente.eMail1_UsuarioEmpresaCliente = obj.eMail1_UsuarioEmpresaCliente;
                dadosNewUsuarioEmpCliente.telefone1_UsuarioEmpresaCliente = obj.telefone1_UsuarioEmpresaCliente;
                dadosNewUsuarioEmpCliente.pais_UsuarioEmpresaCliente = obj.pais_UsuarioEmpresaCliente;
                dadosNewUsuarioEmpCliente.cepEndereco_UsuarioEmpresaCliente= obj.cepEndereco_UsuarioEmpresaCliente.Replace(".", "").Replace("-", "");
                dadosNewUsuarioEmpCliente.endereco_UsuarioEmpresaCliente = obj.endereco_UsuarioEmpresaCliente;
                dadosNewUsuarioEmpCliente.complementoEndereco_UsuarioEmpresaCliente = obj.complementoEndereco_UsuarioEmpresaCliente;
                dadosNewUsuarioEmpCliente.bairro_UsuarioEmpresaCliente= obj.bairro_UsuarioEmpresaCliente;
                dadosNewUsuarioEmpCliente.cidade_UsuarioEmpresaCliente = obj.cidade_UsuarioEmpresaCliente;
                dadosNewUsuarioEmpCliente.uf_UsuarioEmpresaCliente = obj.uf_UsuarioEmpresaCliente;
                dadosNewUsuarioEmpCliente.receberEmails_UsuarioEmpresaCliente = true;
                dadosNewUsuarioEmpCliente.dataCadastro_UsuarioEmpresaCliente = DateTime.Now;
                dadosNewUsuarioEmpCliente.ativoInativo_UsuarioEmpresaCliente= true;

                //GRAVAR NOVA USUÁRIO da EMPRESA CLIENTE
                dadosNewUsuarioEmpCliente = serviceUsuarioEmpCliente.GravarNovoUsuarioEmpresaCliente(dadosNewUsuarioEmpCliente);

                dadosLogonUsuario.id_UsuarioEmpresaCliente = dadosNewUsuarioEmpCliente.id_UsuarioEmpresaCliente;
                dadosLogonUsuario.Lg_DadosLoginUsuarioEmpresaCliente = "spmkt" + dadosNewUsuarioEmpCliente.id_UsuarioEmpresaCliente;
                dadosLogonUsuario.Pw_DadosLoginUsuarioEmpresaCliente = "spmkt" + dadosNewUsuarioEmpCliente.id_UsuarioEmpresaCliente;
                dadosLogonUsuario.eMail1_DadosLoginUsuarioEmpresaCliente = obj.eMail1_UsuarioEmpresaCliente;

                //GERAR DADOS DE LOGON PARA O USUÁRIO
                serviceUsuarioEmpCliente.GerarDadosLogonUsuarioEmpCliente(dadosLogonUsuario);

                /*
                 OBS: DISPARAR E-MAIL PARA O USUÁRIO INFORMANDO O LOGIN E SENHA...
                 */

                return Json(new { status = "ok", idRegistroGerado = dadosNewUsuarioEmpCliente.id_UsuarioEmpresaCliente }, JsonRequestBehavior.AllowGet);
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
                    Usuario_EmpresaCliente dadosUsuarioEmpCliente = 
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(id) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.iUEC = dadosUsuarioEmpCliente.id_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cpf_UsuarioEmpresaCliente = FormatarCpfCnpj(dadosUsuarioEmpCliente.cpf_UsuarioEmpresaCliente);
                    dadosDaEmpresaClienteEUsuario.nome_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.nome_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.eMail1_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.eMail1_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.telefone1_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.telefone1_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cepEndereco_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.cepEndereco_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.endereco_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.endereco_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.complementoEndereco_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.complementoEndereco_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.bairro_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.bairro_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cidade_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.cidade_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cpf_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.cpf_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.uf_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.uf_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.pais_UsuarioEmpresaCliente = dadosUsuarioEmpCliente.pais_UsuarioEmpresaCliente;

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
                Usuario_EmpresaCliente dadosAlteracaoUsuarioEmpCliente = new Usuario_EmpresaCliente();

                dadosAlteracaoUsuarioEmpCliente.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosAlteracaoUsuarioEmpCliente.id_UsuarioEmpresaCliente = obj.id_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioEmpCliente.cpf_UsuarioEmpresaCliente = obj.cpf_UsuarioEmpresaCliente.Replace(".", "").Replace("/", "").Replace("-", "");
                dadosAlteracaoUsuarioEmpCliente.nome_UsuarioEmpresaCliente = obj.nome_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioEmpCliente.eMail1_UsuarioEmpresaCliente = obj.eMail1_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioEmpCliente.telefone1_UsuarioEmpresaCliente = obj.telefone1_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioEmpCliente.pais_UsuarioEmpresaCliente = obj.pais_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioEmpCliente.cepEndereco_UsuarioEmpresaCliente = obj.cepEndereco_UsuarioEmpresaCliente.Replace(".", "").Replace("-", "");
                dadosAlteracaoUsuarioEmpCliente.endereco_UsuarioEmpresaCliente = obj.endereco_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioEmpCliente.complementoEndereco_UsuarioEmpresaCliente = obj.complementoEndereco_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioEmpCliente.bairro_UsuarioEmpresaCliente = obj.bairro_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioEmpCliente.cidade_UsuarioEmpresaCliente = obj.cidade_UsuarioEmpresaCliente;
                dadosAlteracaoUsuarioEmpCliente.uf_UsuarioEmpresaCliente = obj.uf_UsuarioEmpresaCliente;
                //dadosAlteracaoUsuarioEmpCliente.receberEmails_UsuarioEmpresaCliente = true;
                //dadosAlteracaoUsuarioEmpCliente.dataCadastro_UsuarioEmpresaCliente = DateTime.Now;
                //dadosAlteracaoUsuarioEmpCliente.ativoInativo_UsuarioEmpresaCliente = true;

                //ALTERAR DADOS DO USUÁRIO da EMPRESA CLIENTE
                serviceUsuarioEmpCliente.AlterarDadosUsuarioEmpresaCliente(dadosAlteracaoUsuarioEmpCliente);

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
