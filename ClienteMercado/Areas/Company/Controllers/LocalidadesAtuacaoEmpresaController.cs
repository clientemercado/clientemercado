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
    public class LocalidadesAtuacaoEmpresaController : Controller
    {
        //
        // GET: /Company/LocalidadesAtuacaoEmpresa/

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

                    dadosDaEmpresaClienteEUsuario.ListagemCidades = ListagemCidades();
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
                NLocalidadesAtendidasEmpresaService serviceLocalidadesEmpresa = new NLocalidadesAtendidasEmpresaService();
                Localidade_CidadeEmpresaCliente dadosNewLocalidadesEmpresa = new Localidade_CidadeEmpresaCliente();

                dadosNewLocalidadesEmpresa.id_CidadeEmpresaCliente = obj.id_CidadeEmpresaCliente;
                dadosNewLocalidadesEmpresa.nomeLocalidade_LocalidadeCidadeEmpresaCliente= obj.nomeLocalidade_LocalidadeCidadeEmpresaCliente;
                dadosNewLocalidadesEmpresa.cepLocalidade_LocalidadeCidadeEmpresaCliente = obj.cepLocalidade_LocalidadeCidadeEmpresaCliente;
                //dadosNewLocalidadesEmpresa.latitude_logitude_cep_UsuarioEmpresaCliente = obj.uf_UsuarioEmpresaCliente;

                //GRAVAR NOVA LOCALIDADE de ATUAÇÃO da EMPRESA CLIENTE
                dadosNewLocalidadesEmpresa = serviceLocalidadesEmpresa.GravarNovaLocalidadeAtuacaoEmpresa(dadosNewLocalidadesEmpresa);

                return Json(new { status = "ok", idRegistroGerado = dadosNewLocalidadesEmpresa.id_LocalidadeCidadeEmpresaCliente }, JsonRequestBehavior.AllowGet);
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
                    NLocalidadesAtendidasEmpresaService serviceLocalidadesEmpresa = new NLocalidadesAtendidasEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    Localidade_CidadeEmpresaCliente dadosLocalidadeEmpresa = 
                        serviceLocalidadesEmpresa.ConsultarDadosLocalidadeEmpresa(new Localidade_CidadeEmpresaCliente { id_LocalidadeCidadeEmpresaCliente = id });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.iLEC = dadosLocalidadeEmpresa.id_LocalidadeCidadeEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.nomeLocalidade_LocalidadeCidadeEmpresaCliente = dadosLocalidadeEmpresa.nomeLocalidade_LocalidadeCidadeEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cepLocalidade_LocalidadeCidadeEmpresaCliente = dadosLocalidadeEmpresa.cepLocalidade_LocalidadeCidadeEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.id_CidadeEmpresaCliente = dadosLocalidadeEmpresa.id_CidadeEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.ListagemCidades = ListagemCidades();
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
                NLocalidadesAtendidasEmpresaService serviceLocalidadesEmpresa = new NLocalidadesAtendidasEmpresaService();
                Localidade_CidadeEmpresaCliente dadosLocalidadesEmpresaAlterar = new Localidade_CidadeEmpresaCliente();

                dadosLocalidadesEmpresaAlterar.id_LocalidadeCidadeEmpresaCliente = obj.iLEC;
                dadosLocalidadesEmpresaAlterar.id_CidadeEmpresaCliente = obj.id_CidadeEmpresaCliente;
                dadosLocalidadesEmpresaAlterar.nomeLocalidade_LocalidadeCidadeEmpresaCliente = obj.nomeLocalidade_LocalidadeCidadeEmpresaCliente;
                dadosLocalidadesEmpresaAlterar.cepLocalidade_LocalidadeCidadeEmpresaCliente = obj.cepLocalidade_LocalidadeCidadeEmpresaCliente;
                //dadosNewLocalidadesEmpresa.latitude_logitude_cep_UsuarioEmpresaCliente = obj.uf_UsuarioEmpresaCliente;

                //SLTERAR DADOS LOCALIDADE de ATUAÇÃO da EMPRESA CLIENTE
                serviceLocalidadesEmpresa.AlterarDadosLocalidadeAtuacaoEmpresa(dadosLocalidadesEmpresaAlterar);

                return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        //Carrega a lista de Estados (Obs: No momento carregrá todos os estados brasileiros. Depois vejo como ficará)
        private List<SelectListItem> ListagemCidades()
        {
            //Buscar lista de Cidades da Empresa
            NCidadesService serviceCidades = new NCidadesService();

            List<Cidade_EmpresaCliente> listaCidades = serviceCidades.BuscarListaCidadesEmpresa();

            List<SelectListItem> listCitys = new List<SelectListItem>();

            listCitys.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var cidades in listaCidades)
            {
                listCitys.Add(new SelectListItem
                {
                    Text = cidades.cidade_CidadeEmpresaCliente,
                    Value = cidades.id_CidadeEmpresaCliente.ToString()
                });
            }

            return listCitys;
        }

        public ActionResult BuscarListaLocalidadesAtuacaoEmpresa(int idPedido)
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
