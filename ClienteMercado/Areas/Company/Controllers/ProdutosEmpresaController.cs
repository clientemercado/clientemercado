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
    public class ProdutosEmpresaController : Controller
    {
        //
        // GET: /Company/ProdutosEmpresa/

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

                    dadosDaEmpresaClienteEUsuario.ListagemSubDepartamentos = ListagemSubDepartamentos();
                    dadosDaEmpresaClienteEUsuario.ListagemFabricantesMarcas = ListagemFabricantesMarcas();
                    dadosDaEmpresaClienteEUsuario.ListagemPromocoesAtivas = ListagemPromocoesEmpresa();
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
                NProdutoEmpresaService serviceProdutoEmpresa = new NProdutoEmpresaService();
                Produto_EmpresaCliente dadosNewProdutoEmpresa = new Produto_EmpresaCliente();

                dadosNewProdutoEmpresa.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosNewProdutoEmpresa.id_SubDepartamentoEmpresaCliente = Convert.ToInt32(obj.id_SubDepartamentoEmpresaCliente);
                dadosNewProdutoEmpresa.id_EmpresaFabricantesMarcas = Convert.ToInt32(obj.id_EmpresaFabricantesMarcas);

                if (obj.id_PromocaoVendaEmpresaCliente > 0)
                    dadosNewProdutoEmpresa.id_PromocaoVendaEmpresaCliente = Convert.ToInt32(obj.id_PromocaoVendaEmpresaCliente);

                dadosNewProdutoEmpresa.descricao_ProdutoEmpresaCliente = obj.descricao_ProdutoEmpresaCliente;
                dadosNewProdutoEmpresa.tipoEmbalagem_ProdutoEmpresaCliente = obj.tipoEmbalagem_ProdutoEmpresaCliente;
                dadosNewProdutoEmpresa.pesoEmbalagem_ProdutoEmpresaCliente= Convert.ToDecimal(obj.pesoEmbalagem_ProdutoEmpresaCliente.Replace(".", ","));
                dadosNewProdutoEmpresa.unidadePesoEmbalagem_ProdutoEmpresaCliente= obj.unidadePesoEmbalagem_ProdutoEmpresaCliente;
                dadosNewProdutoEmpresa.valorVenda_ProdutoEmpresaCliente = Convert.ToDecimal(obj.valorVenda_ProdutoEmpresaCliente.Replace(".", ","));
                dadosNewProdutoEmpresa.id_SubDepartamentoEmpresaCliente = Convert.ToInt32(obj.id_SubDepartamentoEmpresaCliente);
                dadosNewProdutoEmpresa.id_EmpresaFabricantesMarcas = Convert.ToInt32(obj.id_EmpresaFabricantesMarcas);
                dadosNewProdutoEmpresa.id_PromocaoVendaEmpresaCliente = Convert.ToInt32(obj.id_PromocaoVendaEmpresaCliente);
                dadosNewProdutoEmpresa.ativoInativo_ProdutoEmpresaCliente = obj.ativoInativo_ProdutoEmpresaCliente == "Sim" ? true : false;

                //GRAVAR NOVO PRODUTO da EMPRESA CLIENTE
                dadosNewProdutoEmpresa = serviceProdutoEmpresa.GravarNovoProdutoEmpresaCliente(dadosNewProdutoEmpresa);

                return Json(new { status = "ok", idRegistroGerado = dadosNewProdutoEmpresa.id_ProdutoEmpresaCliente }, JsonRequestBehavior.AllowGet);
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
                    NProdutoEmpresaService serviceProdutoEmpresa = new NProdutoEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });
                    Produto_EmpresaCliente dadosProdutoEmpresa =
                        serviceProdutoEmpresa.ConsultarDadosDoProduto(new Produto_EmpresaCliente { id_ProdutoEmpresaCliente = id });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.iPEC = dadosProdutoEmpresa.id_ProdutoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.id_SubDepartamentoEmpresaCliente = dadosProdutoEmpresa.id_SubDepartamentoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.id_EmpresaFabricantesMarcas = dadosProdutoEmpresa.id_EmpresaFabricantesMarcas;
                    dadosDaEmpresaClienteEUsuario.id_PromocaoVendaEmpresaCliente = dadosProdutoEmpresa.id_PromocaoVendaEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.descricao_ProdutoEmpresaCliente = dadosProdutoEmpresa.descricao_ProdutoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.tipoEmbalagem_ProdutoEmpresaCliente = dadosProdutoEmpresa.tipoEmbalagem_ProdutoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.pesoEmbalagem_ProdutoEmpresaCliente = dadosProdutoEmpresa.pesoEmbalagem_ProdutoEmpresaCliente.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
                    dadosDaEmpresaClienteEUsuario.unidadePesoEmbalagem_ProdutoEmpresaCliente = dadosProdutoEmpresa.unidadePesoEmbalagem_ProdutoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.valorVenda_ProdutoEmpresaCliente = dadosProdutoEmpresa.valorVenda_ProdutoEmpresaCliente.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
                    dadosDaEmpresaClienteEUsuario.ativoInativo_ProdutoEmpresaCliente = dadosProdutoEmpresa.ativoInativo_ProdutoEmpresaCliente ? "Sim" : "Não";

                    dadosDaEmpresaClienteEUsuario.ListagemSubDepartamentos = ListagemSubDepartamentos();
                    dadosDaEmpresaClienteEUsuario.ListagemFabricantesMarcas = ListagemFabricantesMarcas();
                    dadosDaEmpresaClienteEUsuario.ListagemPromocoesAtivas = ListagemPromocoesEmpresa();
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
        //ATUALIZAR REGISTRO
        public JsonResult AtualizarRegistro(DadosEmpresaClienteViewModel obj)
        {
            try
            {
                NProdutoEmpresaService serviceProdutoEmpresa = new NProdutoEmpresaService();
                Produto_EmpresaCliente dadosAlteracaoProdutoEmpresa = new Produto_EmpresaCliente();

                dadosAlteracaoProdutoEmpresa.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosAlteracaoProdutoEmpresa.id_ProdutoEmpresaCliente = Convert.ToInt32(obj.iPEC);
                dadosAlteracaoProdutoEmpresa.id_SubDepartamentoEmpresaCliente = Convert.ToInt32(obj.id_SubDepartamentoEmpresaCliente);
                dadosAlteracaoProdutoEmpresa.id_EmpresaFabricantesMarcas = Convert.ToInt32(obj.id_EmpresaFabricantesMarcas);

                if (obj.id_PromocaoVendaEmpresaCliente > 0)
                    dadosAlteracaoProdutoEmpresa.id_PromocaoVendaEmpresaCliente = Convert.ToInt32(obj.id_PromocaoVendaEmpresaCliente);

                dadosAlteracaoProdutoEmpresa.descricao_ProdutoEmpresaCliente = obj.descricao_ProdutoEmpresaCliente;
                dadosAlteracaoProdutoEmpresa.tipoEmbalagem_ProdutoEmpresaCliente = obj.tipoEmbalagem_ProdutoEmpresaCliente;
                dadosAlteracaoProdutoEmpresa.pesoEmbalagem_ProdutoEmpresaCliente = Convert.ToDecimal(obj.pesoEmbalagem_ProdutoEmpresaCliente.Replace(".", ","));
                dadosAlteracaoProdutoEmpresa.unidadePesoEmbalagem_ProdutoEmpresaCliente = obj.unidadePesoEmbalagem_ProdutoEmpresaCliente;
                dadosAlteracaoProdutoEmpresa.valorVenda_ProdutoEmpresaCliente = Convert.ToDecimal(obj.valorVenda_ProdutoEmpresaCliente.Replace(".", ","));
                dadosAlteracaoProdutoEmpresa.id_SubDepartamentoEmpresaCliente = Convert.ToInt32(obj.id_SubDepartamentoEmpresaCliente);
                dadosAlteracaoProdutoEmpresa.id_EmpresaFabricantesMarcas = Convert.ToInt32(obj.id_EmpresaFabricantesMarcas);
                dadosAlteracaoProdutoEmpresa.id_PromocaoVendaEmpresaCliente = Convert.ToInt32(obj.id_PromocaoVendaEmpresaCliente);
                dadosAlteracaoProdutoEmpresa.ativoInativo_ProdutoEmpresaCliente = obj.ativoInativo_ProdutoEmpresaCliente == "Sim" ? true : false;

                //ALTERAR DADOS do PEDIDO do CLIENTE da EMPRESA
                serviceProdutoEmpresa.AlterarDadosProdutoEmpresa(dadosAlteracaoProdutoEmpresa);

                return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        //CARREGA LISTA de SUB-DEPTOS da EMPRESA
        private List<SelectListItem> ListagemSubDepartamentos()
        {
            //Buscar lista de SubDepartamentos da Empresa
            NSubDepartamentoEmpresaService serviceSubDeptoEmpresa = new NSubDepartamentoEmpresaService();

            List<SubDepartamento_EmpresaCliente> listaSubDepartamentos = serviceSubDeptoEmpresa.ListaSubDepartamentosEmpresa();

            List<SelectListItem> listSubDeptos = new List<SelectListItem>();

            listSubDeptos.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var grupoSubDeptos in listaSubDepartamentos)
            {
                listSubDeptos.Add(new SelectListItem
                {
                    Text = grupoSubDeptos.descricao_SubDepartamentoEmpresaCliente,
                    Value = grupoSubDeptos.id_SubDepartamentoEmpresaCliente.ToString()
                });
            }

            return listSubDeptos;
        }

        //CARREGA LISTA de FABRICANTES e MARCAS COMERCICALIZADAS pela EMPRESA
        private List<SelectListItem> ListagemFabricantesMarcas()
        {
            NEmpresasFabricantesMarcasService serviceFabricantesMarcasEmpresa = new NEmpresasFabricantesMarcasService();

            List<Empresa_FabricantesMarcas> listaFabricantesMarcas = serviceFabricantesMarcasEmpresa.ListaGeralFabricantesEMarcas();

            List<SelectListItem> listFabricantesMarcas = new List<SelectListItem>();

            listFabricantesMarcas.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var grupoSubDeptos in listaFabricantesMarcas)
            {
                listFabricantesMarcas.Add(new SelectListItem
                {
                    Text = grupoSubDeptos.descricao_EmpresaFabricantesMarcas,
                    Value = grupoSubDeptos.id_EmpresaFabricantesMarcas.ToString()
                });
            }

            return listFabricantesMarcas;
        }

        //CARREGA LISTA de PROMOCOES ATIVAS PRATICADAS pela EMPRESA
        private List<SelectListItem> ListagemPromocoesEmpresa()
        {
            NPromocoesEmpresaService servicePromocoesEmpresa = new NPromocoesEmpresaService();

            List<PromocaoVenda_EmpresaCliente> listaPromocoesEmpresa = servicePromocoesEmpresa.ListaPromocoesDaEmpresa();

            List<SelectListItem> listPromocoes = new List<SelectListItem>();

            listPromocoes.Add(new SelectListItem { Text = "", Value = "0" });

            foreach (var grupoPromocoes in listaPromocoesEmpresa)
            {
                listPromocoes.Add(new SelectListItem
                {
                    Text = grupoPromocoes.nomeOferta_PromocaoVendaEmpresaCliente,
                    Value = grupoPromocoes.id_PromocaoVendaEmpresaCliente.ToString()
                });
            }

            return listPromocoes;
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

        public ActionResult BuscarListaProdutosEmpresa()
        {
            try
            {
                NProdutoEmpresaService serviceProdutoEmpresa = new NProdutoEmpresaService();

                List<ListaProdutosEmpresaViewModel> listaProdutosEmpresa = serviceProdutoEmpresa.BuscarListaDeProdutosDaEmpresa();

                for (int i = 0; i < listaProdutosEmpresa.Count; i++)
                {
                    listaProdutosEmpresa[i].idCodProduto = listaProdutosEmpresa[i].id_ProdutoEmpresaCliente.ToString();
                    listaProdutosEmpresa[i].valorVendaProduto = listaProdutosEmpresa[i].valorVenda_ProdutoEmpresaCliente.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
                    listaProdutosEmpresa[i].pesoProduto = listaProdutosEmpresa[i].pesoEmbalagem_ProdutoEmpresaCliente.ToString("C2", CultureInfo.CurrentCulture).Replace("R$ ", "");
                    listaProdutosEmpresa[i].ativoInativoProduto = listaProdutosEmpresa[i].ativoInativo_ProdutoEmpresaCliente ? "Sim" : "Não";
                }

                return Json(
                    new
                    {
                        rows = listaProdutosEmpresa,
                        current = 1,
                        rowCount = listaProdutosEmpresa.Count,
                        total = listaProdutosEmpresa.Count,
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
