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
                dadosNewProdutoEmpresa.pesoEmbalagem_ProdutoEmpresaCliente= Convert.ToInt32(obj.pesoEmbalagem_ProdutoEmpresaCliente);
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
    }
}
