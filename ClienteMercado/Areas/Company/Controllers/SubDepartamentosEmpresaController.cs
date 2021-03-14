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
    public class SubDepartamentosEmpresaController : Controller
    {
        //
        // GET: /Company/SubDepartamentosEmpresa/

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

                    dadosDaEmpresaClienteEUsuario.ListagemDepartamentos = ListagemDepartamentos();
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
                NSubDepartamentoEmpresaService serviceSubDepartamentoEmpresa = new NSubDepartamentoEmpresaService();
                SubDepartamento_EmpresaCliente dadosNewSubDeptoEmpresa = new SubDepartamento_EmpresaCliente();

                dadosNewSubDeptoEmpresa.id_DepartamentoEmpresaCliente= obj.id_DepartamentoEmpresaCliente;
                dadosNewSubDeptoEmpresa.descricao_SubDepartamentoEmpresaCliente= obj.descricao_SubDepartamentoEmpresaCliente;
                dadosNewSubDeptoEmpresa.ativoInativo_SubDepartamentoEmpresaCliente= true;

                //GRAVAR NOVO SUB-DEPTO da EMPRESA CLIENTE
                dadosNewSubDeptoEmpresa = serviceSubDepartamentoEmpresa.GravarNovoSubDeptoEmpresa(dadosNewSubDeptoEmpresa);

                return Json(new { status = "ok", idRegistroGerado = dadosNewSubDeptoEmpresa.id_SubDepartamentoEmpresaCliente }, JsonRequestBehavior.AllowGet);
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
                    NSubDepartamentoEmpresaService serviceSubDepartamentoEmpresa = new NSubDepartamentoEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });
                    SubDepartamento_EmpresaCliente dadosSubDeptoEmpresa = 
                        serviceSubDepartamentoEmpresa.ConsultarDadosSubDeptoEmpresa(new SubDepartamento_EmpresaCliente { id_SubDepartamentoEmpresaCliente = Convert.ToInt32(id) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.iSDEC = dadosSubDeptoEmpresa.id_SubDepartamentoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.id_DepartamentoEmpresaCliente = dadosSubDeptoEmpresa.id_DepartamentoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.descricao_DepartamentoEmpresaCliente = dadosSubDeptoEmpresa.descricao_SubDepartamentoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.ativoInativo_DepartamentoEmpresaCliente = 
                        dadosSubDeptoEmpresa.ativoInativo_SubDepartamentoEmpresaCliente.ToString();

                    dadosDaEmpresaClienteEUsuario.ListagemDepartamentos = ListagemDepartamentos();
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
                NSubDepartamentoEmpresaService serviceSubDepartamentoEmpresa = new NSubDepartamentoEmpresaService();
                SubDepartamento_EmpresaCliente dadosSubDeptoEmpresaAlterar = new SubDepartamento_EmpresaCliente();

                dadosSubDeptoEmpresaAlterar.id_SubDepartamentoEmpresaCliente = obj.iSDEC;
                dadosSubDeptoEmpresaAlterar.id_DepartamentoEmpresaCliente = obj.id_DepartamentoEmpresaCliente;
                dadosSubDeptoEmpresaAlterar.descricao_SubDepartamentoEmpresaCliente = obj.descricao_SubDepartamentoEmpresaCliente;
                dadosSubDeptoEmpresaAlterar.ativoInativo_SubDepartamentoEmpresaCliente = true;

                //ATUALIZAR DADOS do SUB-DEPTO da EMPRESA CLIENTE
                serviceSubDepartamentoEmpresa.AlterarDadosSubDeptoEmpresa(dadosSubDeptoEmpresaAlterar);

                return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        //Carrega a lista de DEPARTAMENTOS da EMPRESA
        private List<SelectListItem> ListagemDepartamentos()
        {
            //Buscar lista de Departamentos da Empresa
            NDepartamentoEmpresaClienteService servicebDeptoEmpresa = new NDepartamentoEmpresaClienteService();

            List<Departamento_EmpresaCliente> listaDepartamentos = servicebDeptoEmpresa.ListaDepartamentosEmpresa();

            List<SelectListItem> listDeptos = new List<SelectListItem>();

            listDeptos.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var grupoDeptos in listaDepartamentos)
            {
                listDeptos.Add(new SelectListItem
                {
                    Text = grupoDeptos.descricao_DepartamentoEmpresaCliente,
                    Value = grupoDeptos.id_DepartamentoEmpresaCliente.ToString()
                });
            }

            return listDeptos;
        }

        public ActionResult BuscarListaClientesEmpresa(int idPedido)
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
