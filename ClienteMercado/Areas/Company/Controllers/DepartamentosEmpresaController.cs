using ClienteMercado.Data.Entities;
using ClienteMercado.Domain.Services;
using ClienteMercado.UI.Core.ViewModel;
using ClienteMercado.Utils.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClienteMercado.Areas.Company.Controllers
{
    public class DepartamentosEmpresaController : Controller
    {
        //
        // GET: /Company/DepartamentosEmpresa/

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

                    EmpresaCliente dadosEmpresaCliente =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaCliente =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeFantasia_EmpresaCliente = dadosEmpresaCliente.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nome_UsuarioEmpresaCliente = dadosUsuEmpresaCliente.nome_UsuarioEmpresaCliente;
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

                    EmpresaCliente dadosEmpresaCliente =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaCliente =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeFantasia_EmpresaCliente = dadosEmpresaCliente.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nome_UsuarioEmpresaCliente = dadosUsuEmpresaCliente.nome_UsuarioEmpresaCliente;
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

                return Json(new { status = "ok", idRegistroGerado = 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        public ActionResult AlterarDados()
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

                    EmpresaCliente dadosEmpresaCliente =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaCliente =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeFantasia_EmpresaCliente = dadosEmpresaCliente.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nome_UsuarioEmpresaCliente = dadosUsuEmpresaCliente.nome_UsuarioEmpresaCliente;
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

    }
}
