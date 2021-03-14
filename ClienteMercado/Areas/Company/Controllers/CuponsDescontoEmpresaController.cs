using ClienteMercado.Data.Entities;
using ClienteMercado.Domain.Services;
using ClienteMercado.UI.Core.ViewModel;
using ClienteMercado.Utils.Net;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClienteMercado.Areas.Company.Controllers
{
    public class CuponsDescontoEmpresaController : Controller
    {
        //
        // GET: /Company/CuponsDescontoEmpresa/

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
                NCupomDescontoEmpresaService serviceCuponDesconto = new NCupomDescontoEmpresaService();
                CupomDesconto_EmpresaCliente dadosNewCuponEmpresa = new CupomDesconto_EmpresaCliente();

                dadosNewCuponEmpresa.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosNewCuponEmpresa.dataCadastroCupon_CupomDescontoEmpresaCliente = DateTime.Now;
                dadosNewCuponEmpresa.idUsuarioCadastrouCupon_CupomDescontoEmpresaCliente = Sessao.IdUsuarioLogado;
                dadosNewCuponEmpresa.nomeCupom_CupomDescontoEmpresaCliente = obj.nomeCupom_CupomDescontoEmpresaCliente;
                dadosNewCuponEmpresa.dataValidade_CupomDescontoEmpresaCliente = Convert.ToDateTime(obj.dataValidade_CupomDescontoEmpresaCliente);
                dadosNewCuponEmpresa.percentualDesconto_CupomDescontoEmpresaCliente = Convert.ToDecimal(obj.percentualDesconto_CupomDescontoEmpresaCliente);
                dadosNewCuponEmpresa.ativoInativo_CupomDescontoEmpresaCliente = true;
                dadosNewCuponEmpresa.idUsuarioAtivou_CupomDescontoEmpresaCliente = Sessao.IdUsuarioLogado;

                //GRAVAR NOVA CUPOM da EMPRESA CLIENTE
                dadosNewCuponEmpresa = serviceCuponDesconto.GravarNovaCuponDescontoEmpresa(dadosNewCuponEmpresa);

                return Json(new { status = "ok", idRegistroGerado = dadosNewCuponEmpresa.id_CuponDescontoEmpresaCliente }, JsonRequestBehavior.AllowGet);
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
                    NCupomDescontoEmpresaService serviceCuponDesconto = new NCupomDescontoEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });
                    CupomDesconto_EmpresaCliente dadosCupomDescontoEmpresa = 
                        serviceCuponDesconto.ConsultarDadosCupomDescontoEmpresa(new CupomDesconto_EmpresaCliente { id_CuponDescontoEmpresaCliente = Convert.ToInt32(id) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.iCDEC = dadosCupomDescontoEmpresa.id_CuponDescontoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.nomeCupom_CupomDescontoEmpresaCliente = dadosCupomDescontoEmpresa.nomeCupom_CupomDescontoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.dataValidade_CupomDescontoEmpresaCliente = 
                        Convert.ToDateTime(dadosCupomDescontoEmpresa.dataValidade_CupomDescontoEmpresaCliente).ToString("dd/MM/yyyy");
                    dadosDaEmpresaClienteEUsuario.percentualDesconto_CupomDescontoEmpresaCliente = 
                        dadosCupomDescontoEmpresa.percentualDesconto_CupomDescontoEmpresaCliente.ToString().Replace(".","");
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
                NCupomDescontoEmpresaService serviceCuponDesconto = new NCupomDescontoEmpresaService();
                CupomDesconto_EmpresaCliente dadosCuponEmpresaAlterar = new CupomDesconto_EmpresaCliente();

                dadosCuponEmpresaAlterar.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosCuponEmpresaAlterar.id_CuponDescontoEmpresaCliente = obj.iCDEC;
                dadosCuponEmpresaAlterar.nomeCupom_CupomDescontoEmpresaCliente = obj.nomeCupom_CupomDescontoEmpresaCliente;
                dadosCuponEmpresaAlterar.dataValidade_CupomDescontoEmpresaCliente = Convert.ToDateTime(obj.dataValidade_CupomDescontoEmpresaCliente);
                dadosCuponEmpresaAlterar.percentualDesconto_CupomDescontoEmpresaCliente = Convert.ToDecimal(obj.percentualDesconto_CupomDescontoEmpresaCliente);
                //dadosCuponEmpresaAlterar.ativoInativo_CupomDescontoEmpresaCliente = true;
                //dadosCuponEmpresaAlterar.idUsuarioAtivou_CupomDescontoEmpresaCliente = Sessao.IdUsuarioLogado;

                //ALTERAR DADOS da CUPOM DESCONTOS da EMPRESA CLIENTE
                serviceCuponDesconto.AlterarDadosCupomDescontosEmpresa(dadosCuponEmpresaAlterar);

                return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        public ActionResult BuscarListaCuponsDescontoEmpresa(int idPedido)
        {
            /*
            MODIFICAR CÓDIGO DE BUSCA ABAIXO... 
             */

            try
            {
                NPedidoClienteEmpresaService servicePedidoCliente = new NPedidoClienteEmpresaService();

                List<ListaItensDoPedidoViewModel> listaProdutosPedido = servicePedidoCliente.BuscarListaDeProdutosDoPedido(idPedido);

                for (int i = 0; i < listaProdutosPedido.Count; i++)
                {
                    listaProdutosPedido[i].quantidadeItemPedido = listaProdutosPedido[i].quantidade_ProdutosPedidoCliente.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
                    listaProdutosPedido[i].dataEntregaItemPedido =
                        Convert.ToDateTime(listaProdutosPedido[i].dataEntregaItemPedido_ProdutosPedidoCliente).ToString("dd/MM/yyyy");
                    listaProdutosPedido[i].valorUnitarioItemPedido = listaProdutosPedido[i].valorUnitario_ProdutosPedidoCliente.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
                    listaProdutosPedido[i].totalProdutoComprado =
                        (listaProdutosPedido[i].quantidade_ProdutosPedidoCliente * listaProdutosPedido[i].valorUnitario_ProdutosPedidoCliente).ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
                }

                return Json(
                    new
                    {
                        rows = listaProdutosPedido,
                        current = 1,
                        rowCount = listaProdutosPedido.Count,
                        total = listaProdutosPedido.Count,
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
