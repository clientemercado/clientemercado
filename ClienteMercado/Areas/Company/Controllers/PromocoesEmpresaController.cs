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
    public class PromocoesEmpresaController : Controller
    {
        //
        // GET: /Company/PromocoesEmpresa/

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
                NPromocoesEmpresaService servicePromocoesEmpresa = new NPromocoesEmpresaService();
                PromocaoVenda_EmpresaCliente dadosNewPromocaoEmpresa = new PromocaoVenda_EmpresaCliente();

                dadosNewPromocaoEmpresa.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosNewPromocaoEmpresa.dataCadastroOferta_PromocaoVendaEmpresaCliente = DateTime.Now;
                dadosNewPromocaoEmpresa.idUsuarioCadastrouOferta_PromocaoVendaEmpresaCliente = Sessao.IdUsuarioLogado;
                dadosNewPromocaoEmpresa.nomeOferta_PromocaoVendaEmpresaCliente = obj.nomeOferta_PromocaoVendaEmpresaCliente;
                dadosNewPromocaoEmpresa.dataValidade_PromocaoVendaEmpresaCliente = Convert.ToDateTime(obj.dataValidade_PromocaoVendaEmpresaCliente);
                dadosNewPromocaoEmpresa.percentualOffOferta_PromocaoVendaEmpresaCliente = Convert.ToDecimal(obj.percentualOffOferta_PromocaoVendaEmpresaCliente);
                dadosNewPromocaoEmpresa.bannerOferta_PromocaoVendaEmpresaCliente = obj.bannerOferta_PromocaoVendaEmpresaCliente;
                dadosNewPromocaoEmpresa.ativoInativo_PromocaoVendaEmpresaCliente = true;

                //GRAVAR NOVA PROMOCAO da EMPRESA CLIENTE
                dadosNewPromocaoEmpresa = servicePromocoesEmpresa.GravarNovaPromocaoEmpresa(dadosNewPromocaoEmpresa);

                return Json(new { status = "ok", idRegistroGerado = dadosNewPromocaoEmpresa.id_PromocaoVendaEmpresaCliente }, JsonRequestBehavior.AllowGet);
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
                    NPromocoesEmpresaService servicePromocoesEmpresa = new NPromocoesEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    PromocaoVenda_EmpresaCliente dadosPromocaoVendaEmpresa =
                        servicePromocoesEmpresa.ConsultarDadosPromocaoEmpresa(new PromocaoVenda_EmpresaCliente { id_PromocaoVendaEmpresaCliente = Convert.ToInt32(id) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.iPVDEC = dadosPromocaoVendaEmpresa.id_PromocaoVendaEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.nomeOferta_PromocaoVendaEmpresaCliente = dadosPromocaoVendaEmpresa.nomeOferta_PromocaoVendaEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.dataValidade_PromocaoVendaEmpresaCliente =
                        Convert.ToDateTime(dadosPromocaoVendaEmpresa.dataValidade_PromocaoVendaEmpresaCliente).ToString("dd/MM/yyyy");
                    dadosDaEmpresaClienteEUsuario.percentualOffOferta_PromocaoVendaEmpresaCliente =
                        dadosPromocaoVendaEmpresa.percentualOffOferta_PromocaoVendaEmpresaCliente.ToString().Replace(".", "");
                    dadosDaEmpresaClienteEUsuario.bannerOferta_PromocaoVendaEmpresaCliente = dadosPromocaoVendaEmpresa.bannerOferta_PromocaoVendaEmpresaCliente;
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
                NPromocoesEmpresaService servicePromocoesEmpresa = new NPromocoesEmpresaService();
                PromocaoVenda_EmpresaCliente dadosPromocaoEmpresaAlterar = new PromocaoVenda_EmpresaCliente();

                dadosPromocaoEmpresaAlterar.id_PromocaoVendaEmpresaCliente = obj.iPVDEC;
                dadosPromocaoEmpresaAlterar.nomeOferta_PromocaoVendaEmpresaCliente = obj.nomeOferta_PromocaoVendaEmpresaCliente;
                dadosPromocaoEmpresaAlterar.dataValidade_PromocaoVendaEmpresaCliente = Convert.ToDateTime(obj.dataValidade_PromocaoVendaEmpresaCliente);
                dadosPromocaoEmpresaAlterar.percentualOffOferta_PromocaoVendaEmpresaCliente = Convert.ToDecimal(obj.percentualOffOferta_PromocaoVendaEmpresaCliente);
                dadosPromocaoEmpresaAlterar.bannerOferta_PromocaoVendaEmpresaCliente = obj.bannerOferta_PromocaoVendaEmpresaCliente;
                //dadosPromocaoEmpresaAlterar.ativoInativo_PromocaoVendaEmpresaCliente = true;

                //ALTERAR DADOS da PROMOCAO da EMPRESA CLIENTE
                servicePromocoesEmpresa.AlterarDadosPromocaoEmpresa(dadosPromocaoEmpresaAlterar);

                return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        public ActionResult BuscarListaPromocoesEmpresa(int idPedido)
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
