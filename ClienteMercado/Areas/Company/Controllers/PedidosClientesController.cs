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
using System.Web.Services;

namespace ClienteMercado.Areas.Company.Controllers
{
    public class PedidosClientesController : Controller
    {
        //
        // GET: /Company/PedidosClientes/

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

                    dadosDaEmpresaClienteEUsuario.ListagemOpcoesSimNao = ListagemOpcoes();
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
                    NPedidoClienteEmpresaService servicePedidoCliente = new NPedidoClienteEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });
                    PedidoCliente_EmpresaCliente dadosPedidoClienteEmpresa = 
                        servicePedidoCliente.ConsultarDadosDoPedidoCleinteEmpresa(new PedidoCliente_EmpresaCliente { id_PedidoClienteEmpresaCliente = id });
                    Usuario_EmpresaCliente dadosUsuarioEntregou = 
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(
                            new Usuario_EmpresaCliente { 
                                id_UsuarioEmpresaCliente = Convert.ToInt32(dadosPedidoClienteEmpresa.idUsuarioEmpresaEntregou_ClienteEmpresaCliente) 
                            });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.idPedidoCliente = dadosPedidoClienteEmpresa.id_PedidoClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.inCodControlePedidoClienteEntrega = dadosPedidoClienteEmpresa.codControlePedido_PedidoClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.nomeClienteEntrega = dadosPedidoClienteEmpresa.cliente_empresaCliente.nome_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cidadeClienteEntrega = dadosPedidoClienteEmpresa.cidade_empresaCliente.cidade_CidadeEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.ufClienteEntrega = dadosPedidoClienteEmpresa.cidade_empresaCliente.uf_CidadeEmpresaCliente; ;
                    dadosDaEmpresaClienteEUsuario.localidadeClienteEntrega = 
                        dadosPedidoClienteEmpresa.localidade_empresaCliente.nomeLocalidade_LocalidadeCidadeEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.modoPagamentoClienteEntrega = 
                        dadosPedidoClienteEmpresa.meiosPagamento_empresaCliente.descricao_MeiosPagamentoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.cuponDescontoClienteEntrega = dadosPedidoClienteEmpresa.cuponDesconto_empresaCliente.nomeCupom_CupomDescontoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.valorPedido_PedidoClienteEmpresaCliente = 
                        dadosPedidoClienteEmpresa.valorPedido_PedidoClienteEmpresaCliente.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
                    dadosDaEmpresaClienteEUsuario.pedidoEntregue_PedidoClienteEmpresaCliente = 
                        dadosPedidoClienteEmpresa.pedidoEntregue_PedidoClienteEmpresaCliente ? "Sim" : "Não";
                    dadosDaEmpresaClienteEUsuario.idUsuarioEmpresaEntregou_ClienteEmpresaCliente = dadosPedidoClienteEmpresa.idUsuarioEmpresaEntregou_ClienteEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioFuncionarioEntrega = dadosUsuarioEntregou.nome_UsuarioEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.dataEntregaPedido_ClienteEmpresaCliente = 
                        Convert.ToDateTime(dadosPedidoClienteEmpresa.dataEntregaPedido_ClienteEmpresaCliente).ToString("dd/MM/yyyy");
                    dadosDaEmpresaClienteEUsuario.ListagemOpcoesSimNao = ListagemOpcoes();
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

        //ATUALIZAR REGISTRO
        public JsonResult AtualizarRegistro(DadosEmpresaClienteViewModel obj)
        {
            try
            {
                NPedidoClienteEmpresaService servicePedidoClienteEmpresa = new NPedidoClienteEmpresaService();
                PedidoCliente_EmpresaCliente dadosAlteracaoPedidoEmpresa = new PedidoCliente_EmpresaCliente();

                dadosAlteracaoPedidoEmpresa.id_PedidoClienteEmpresaCliente = Convert.ToInt32(obj.idPedidoCliente);
                dadosAlteracaoPedidoEmpresa.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosAlteracaoPedidoEmpresa.valorPedido_PedidoClienteEmpresaCliente = Convert.ToDecimal(obj.valorPedido_PedidoClienteEmpresaCliente.Replace(".",","));
                dadosAlteracaoPedidoEmpresa.idUsuarioEmpresaEntregou_ClienteEmpresaCliente = obj.idUsuarioEmpresaEntregou_ClienteEmpresaCliente;
                dadosAlteracaoPedidoEmpresa.pedidoEntregue_PedidoClienteEmpresaCliente = obj.pedidoEntregue_PedidoClienteEmpresaCliente == "Sim" ? true : false;
                dadosAlteracaoPedidoEmpresa.dataEntregaPedido_ClienteEmpresaCliente = Convert.ToDateTime(obj.dataEntregaPedido_ClienteEmpresaCliente);

                //ALTERAR DADOS do PEDIDO do CLIENTE da EMPRESA
                servicePedidoClienteEmpresa.AlterarDadosPedidoClienteEmpresa(dadosAlteracaoPedidoEmpresa);

                return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //Carrega a lista de Opções (SIM e NÃO)
        private List<SelectListItem> ListagemOpcoes()
        {
            List<SelectListItem> listOpcoes = new List<SelectListItem>();

            listOpcoes.Add(new SelectListItem { Text = "", Value = "" });
            listOpcoes.Add(new SelectListItem { Text = "Sim", Value = "Sim" });
            listOpcoes.Add(new SelectListItem { Text = "Não", Value = "Não" });

            return listOpcoes;
        }

        //CONSULTA do AUTOCOMPLETE da LISTA de USUÁRIOS FUNCIONÁRIOS da EMPRESA CLIENTE
        [WebMethod]
        public JsonResult BuscarListaUsuariosFuncionariosEmpresaCliente(string term)
        {
            try
            {
                NUsuarioEmpresaService serviceUsuarioEmpCliente = new NUsuarioEmpresaService();

                //CARREGA LISTA de USUÁRIOS FUNCIONÁRIOS
                List<Usuario_EmpresaCliente> listaUsuEmpresa = serviceUsuarioEmpCliente.CarregarListaDeUsuariosFuncionariosEmpresaCliente(term);

                var lista = (from t in listaUsuEmpresa
                             where t.nome_UsuarioEmpresaCliente.ToLower().Contains(term.ToLower())
                             select new { t.id_UsuarioEmpresaCliente, t.nome_UsuarioEmpresaCliente }).Distinct().ToList();

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        public ActionResult BuscarListaDeProdutosDoPedido(int idPedido)
        {
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

                //return Json(listaProdutosPedido, JsonRequestBehavior.AllowGet);
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
