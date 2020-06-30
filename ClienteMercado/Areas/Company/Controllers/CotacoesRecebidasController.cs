﻿using ClienteMercado.Data.Entities;
using ClienteMercado.Domain.Services;
using ClienteMercado.Models;
using ClienteMercado.UI.Core.ViewModel;
using ClienteMercado.Utils.Net;
using ClienteMercado.Utils.Utilitarios;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Services;

namespace ClienteMercado.Areas.Company.Controllers
{
    public class CotacoesRecebidasController : Controller
    {
        //
        // GET: /Company/CotacoesRecebidas/

        public ActionResult Index()
        {
            try
            {
                if (Sessao.IdEmpresaUsuario > 0)
                {
                    DateTime dataHoje = DateTime.Today;

                    //Montando o dia por extenso a se exibido no site (* Não se usa mais isso)     <== ESTE CÓDIGO FOI COPIADO e EXISTE GRANDE PROBABILIDADE DE SER MODIFICAO
                    string diaDaSemana = new CultureInfo("pt-BR").DateTimeFormat.GetDayName(dataHoje.DayOfWeek);
                    diaDaSemana = char.ToUpper(diaDaSemana[0]) + diaDaSemana.Substring(1);

                    int diaDoMes = dataHoje.Day;
                    int mesDoAno = dataHoje.Month;
                    string mesAtual = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(dataHoje.Month);
                    mesAtual = char.ToUpper(mesAtual[0]) + mesAtual.Substring(1);
                    int anoAtual = dataHoje.Year;

                    NEmpresaUsuarioService negociosEmpresaUsuario = new NEmpresaUsuarioService();
                    NUsuarioEmpresaService negociosUsuarioEmpresa = new NUsuarioEmpresaService();
                    NGruposAtividadesEmpresaProfissionalService negociosGrupoAtividadesEmpresa = new NGruposAtividadesEmpresaProfissionalService();
                    NEnderecoEmpresaUsuarioService negociosLocalizacao = new NEnderecoEmpresaUsuarioService();

                    AcompanharCotacaoesViewModel viewModelCC = new AcompanharCotacaoesViewModel();

                    empresa_usuario dadosEmpresa = negociosEmpresaUsuario.ConsultarDadosDaEmpresa(new empresa_usuario { ID_CODIGO_EMPRESA = Convert.ToInt32(Sessao.IdEmpresaUsuario) });
                    usuario_empresa dadosUsuarioEmpresa = negociosUsuarioEmpresa.ConsultarDadosDoUsuarioDaEmpresa(Convert.ToInt32(Sessao.IdEmpresaUsuario));

                    grupo_atividades_empresa dadosGruposAtividadesEmpresa =
                        negociosGrupoAtividadesEmpresa.ConsultarDadosDoGrupoDeAtividadesDaEmpresa(new grupo_atividades_empresa { ID_GRUPO_ATIVIDADES = dadosEmpresa.ID_GRUPO_ATIVIDADES });
                    List<ListaDadosDeLocalizacaoViewModel> dadosLocalizacao =
                        negociosLocalizacao.ConsultarDadosDaLocalizacaoPeloCodigo(dadosEmpresa.ID_CODIGO_ENDERECO_EMPRESA_USUARIO);

                    //POPULAR VIEW MODEL
                    viewModelCC.NOME_FANTASIA_EMPRESA = dadosEmpresa.NOME_FANTASIA_EMPRESA.ToUpper();
                    viewModelCC.NOME_USUARIO = dadosUsuarioEmpresa.NOME_USUARIO;

                    viewModelCC.inEmpresaLogadaAdmCC = dadosEmpresa.NOME_FANTASIA_EMPRESA.ToUpper();
                    viewModelCC.inCidadeEmpresaAdmCC = dadosLocalizacao[0].CIDADE_EMPRESA_USUARIO + "-" + dadosLocalizacao[0].UF_EMPRESA_USUARIO;
                    viewModelCC.inNomeUsuarioRespEmpresaAdmCC = dadosUsuarioEmpresa.NOME_USUARIO;
                    viewModelCC.inDataCriacaoCC = diaDoMes + "/" + mesDoAno + "/" + anoAtual;
                    viewModelCC.inRamoAtividadeEmpresaAdm = dadosGruposAtividadesEmpresa.DESCRICAO_ATIVIDADE;
                    viewModelCC.inListaTiposDeFiltros = ListaDeTiposDeFiltros();
                    viewModelCC.inListaDeRamosDeAtividade = ListaDeGruposDeAtividades();
                    viewModelCC.inListaTiposDeCotacao = ListagemTiposDeCotacao();

                    //VIEWBAGS
                    ViewBag.dataHoje = diaDaSemana + ", " + diaDoMes + " de " + mesAtual + " de " + anoAtual;
                    ViewBag.ondeEstouAgora = "Cotações Recebidas";

                    return View(viewModelCC);
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

        public ActionResult VisualizarResponderCotacao(int cCC, int iCM, int iCCF)
        {
            try
            {
                if ((Sessao.IdEmpresaUsuario > 0) && (cCC > 0) && (iCM > 0) && (iCCF > 0))
                {
                    int valor = 0;
                    decimal somaValoresCotados = 0;
                    //decimal percentualIdealConfirmado = 0;
                    string mensagem = "";
                    string respondeuContraProposta = "";

                    DateTime dataHoje = DateTime.Today;

                    //Montando o dia por extenso a se exibido no site (* Não se usa mais isso)
                    string diaDaSemana = new CultureInfo("pt-BR").DateTimeFormat.GetDayName(dataHoje.DayOfWeek);
                    diaDaSemana = char.ToUpper(diaDaSemana[0]) + diaDaSemana.Substring(1);

                    int diaDoMes = dataHoje.Day;
                    string mesAtual = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(dataHoje.Month);
                    mesAtual = char.ToUpper(mesAtual[0]) + mesAtual.Substring(1);
                    int anoAtual = dataHoje.Year;
                    string textoMsgStatus = "PEDIDO";
                    string idsPedidos = "";

                    NCentralDeComprasService negociosCentralDeCompras = new NCentralDeComprasService();
                    NEmpresaUsuarioService negociosEmpresaUsuario = new NEmpresaUsuarioService();
                    NUsuarioEmpresaService negociosUsuarioEmpresa = new NUsuarioEmpresaService();
                    NGruposAtividadesEmpresaProfissionalService negociosGrupoAtividadesEmpresa = new NGruposAtividadesEmpresaProfissionalService();
                    NEnderecoEmpresaUsuarioService negociosLocalizacao = new NEnderecoEmpresaUsuarioService();
                    NCotacaoMasterCentralDeComprasService negociosCotacaoMaster = new NCotacaoMasterCentralDeComprasService();
                    NCotacaoFilhaCentralDeComprasService negociosCotacaoFilhaCC = new NCotacaoFilhaCentralDeComprasService();
                    NChatCotacaoCentralComprasService negociosChatCotacaoCentralCompras = new NChatCotacaoCentralComprasService();
                    NPedidoCentralComprasService negociosPedidosCentralCompras = new NPedidoCentralComprasService();
                    NItensCotacaoFilhaNegociacaoCentralDeComprasService negociosItensCotacaoFilhaCentralCompras = new NItensCotacaoFilhaNegociacaoCentralDeComprasService();
                    NItensPedidoCentralComprasService negociosItensPedidosCentralCompras = new NItensPedidoCentralComprasService();

                    //CARREGAR DADOS da COTAÇÃO MASTER
                    cotacao_master_central_compras dadosDaCotacaoMaster = negociosCotacaoMaster.ConsultarDadosDaCotacaoMasterCC(iCM);

                    //CARREGAR os DADOS da CENTRAL de COMPRAS
                    central_de_compras dadosDaCentralDeCompras = negociosCentralDeCompras.ConsultarDadosGeraisSobreACentralDeComprasPeloID(cCC);

                    empresa_usuario dadosEmpresaADM =
                        negociosEmpresaUsuario.ConsultarDadosDaEmpresa(new empresa_usuario { ID_CODIGO_EMPRESA = dadosDaCentralDeCompras.ID_CODIGO_EMPRESA_ADM_CENTRAL_COMPRAS });
                    usuario_empresa dadosUsuarioEmpresaADM =
                        negociosUsuarioEmpresa.ConsultarDadosDoUsuarioDaEmpresa(dadosDaCentralDeCompras.ID_CODIGO_USUARIO_ADM_CENTRAL_COMPRAS);

                    empresa_usuario dadosEmpresaLogada =
                        negociosEmpresaUsuario.ConsultarDadosDaEmpresa(new empresa_usuario { ID_CODIGO_EMPRESA = Convert.ToInt32(Sessao.IdEmpresaUsuario) });
                    usuario_empresa dadosUsuarioEmpresaLogada =
                        negociosUsuarioEmpresa.ConsultarDadosDoUsuarioDaEmpresa(Convert.ToInt32(Sessao.IdEmpresaUsuario));

                    grupo_atividades_empresa dadosGruposAtividadesCC =
                        negociosGrupoAtividadesEmpresa.ConsultarDadosDoGrupoDeAtividadesDaEmpresa(new grupo_atividades_empresa { ID_GRUPO_ATIVIDADES = dadosDaCentralDeCompras.ID_GRUPO_ATIVIDADES });
                    List<ListaDadosDeLocalizacaoViewModel> dadosLocalizacaoEmpresaADM =
                        negociosLocalizacao.ConsultarDadosDaLocalizacaoPeloCodigo(dadosEmpresaADM.ID_CODIGO_ENDERECO_EMPRESA_USUARIO);

                    //CONSULTAR DADOS da COTAÇÃO FILHA
                    cotacao_filha_central_compras dadosCotacaoFilha = negociosCotacaoFilhaCC.ConsultarDadosDaCotacaoFilhaCC(iCM, iCCF);

                    //CONSULTAR LISTA de ITENS da COTACAO FILHA
                    List<itens_cotacao_filha_negociacao_central_compras> listaDeItensDaCotacaoFilha = negociosItensCotacaoFilhaCentralCompras.ConsultarItensDaCotacaoDaCC(iCCF);

                    //Busca os dados do CHAT entre a EMPRESA COTANTE e a EMPRESA FORNECEDORA
                    List<chat_cotacao_central_compras> listaConversasApuradasNoChat =
                        negociosChatCotacaoCentralCompras.BuscarChatEntreEmpresaCotanteEFornecedor(dadosCotacaoFilha.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS);

                    valor = listaConversasApuradasNoChat.Count;

                    if (valor > 0)
                    {
                        if (valor % 2 == 0)
                        {
                            //Par - TEM NOVA MENSAGEM
                            mensagem = "tem";
                        }
                        else
                        {
                            //Impar - NÃO TEM NOVA MENSAGEM
                            mensagem = "naoTem";
                        }
                    }
                    else
                    {
                        mensagem = "naoTem";
                    }

                    DadosDaCotacaoViewModel viewModelEnviarResposta = new DadosDaCotacaoViewModel();

                    empresa_usuario dadosEmpresa =
                        negociosEmpresaUsuario.ConsultarDadosDaEmpresa(new empresa_usuario { ID_CODIGO_EMPRESA = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    usuario_empresa dadosUsuarioEmpresa =
                        negociosUsuarioEmpresa.ConsultarDadosDoUsuarioDaEmpresa(Convert.ToInt32(Session["IdUsuarioLogado"]));

                    //POPULAR VIEW MODEL
                    viewModelEnviarResposta.cCC = cCC;
                    viewModelEnviarResposta.iCM = iCM;
                    viewModelEnviarResposta.iCCF = iCCF;
                    viewModelEnviarResposta.inNomeCentralCompras = dadosDaCentralDeCompras.NOME_CENTRAL_COMPRAS;
                    viewModelEnviarResposta.inCodEmpresaADM = dadosDaCentralDeCompras.ID_CODIGO_EMPRESA_ADM_CENTRAL_COMPRAS;
                    viewModelEnviarResposta.inCodUsuariomEmpresaADM = dadosDaCentralDeCompras.ID_CODIGO_USUARIO_ADM_CENTRAL_COMPRAS;
                    viewModelEnviarResposta.ineACriptografado = MD5Crypt.Criptografar(dadosDaCentralDeCompras.ID_CODIGO_EMPRESA_ADM_CENTRAL_COMPRAS.ToString());
                    viewModelEnviarResposta.inEmpresaLogadaAdmCC = dadosEmpresaADM.NOME_FANTASIA_EMPRESA.ToUpper();
                    viewModelEnviarResposta.inCodEmpresaLogada = (int)Sessao.IdEmpresaUsuario;
                    viewModelEnviarResposta.NOME_FANTASIA_EMPRESA = dadosEmpresaLogada.NOME_FANTASIA_EMPRESA.ToUpper();
                    viewModelEnviarResposta.inNomeUsuarioLogado = dadosUsuarioEmpresaLogada.NOME_USUARIO;
                    viewModelEnviarResposta.NOME_USUARIO = dadosUsuarioEmpresaLogada.NOME_USUARIO;
                    viewModelEnviarResposta.inRamoAtividadeCC = dadosGruposAtividadesCC.DESCRICAO_ATIVIDADE;
                    viewModelEnviarResposta.inCodRamoAtividadeCC = dadosGruposAtividadesCC.ID_GRUPO_ATIVIDADES;
                    viewModelEnviarResposta.inNomEmpresaAdmCC = dadosEmpresaADM.NOME_FANTASIA_EMPRESA;
                    viewModelEnviarResposta.inNomeUsuarioRespEmpresaAdmCC = dadosUsuarioEmpresaADM.NOME_USUARIO;
                    viewModelEnviarResposta.inCidadeEmpresaAdmCC = dadosLocalizacaoEmpresaADM[0].CIDADE_EMPRESA_USUARIO;
                    viewModelEnviarResposta.inIdEmpresaCotada = dadosEmpresaLogada.ID_CODIGO_EMPRESA;
                    viewModelEnviarResposta.idUsuarioEmpresaFornecedoraCotada = dadosUsuarioEmpresaLogada.ID_CODIGO_USUARIO;
                    //respondeuContraProposta = listaDeItensDaCotacaoFilha.Where(m => (m.PRECO_UNITARIO_ITENS_CONTRA_PROPOSTA_CENTRAL_COMPRAS > 0)).ToList().Count() > 0 ? "sim" : "nao";
                    //viewModelEnviarResposta.inRespondeuContraProposta = respondeuContraProposta;

                    //----------------------------------------------------------------------
                    //VERIFICA SE TODOS OS ITENS FORAM OU NÃO RESPONDIDOS 
                    for (int v = 0; v < listaDeItensDaCotacaoFilha.Count; v++)
                    {
                        somaValoresCotados = (somaValoresCotados + listaDeItensDaCotacaoFilha[v].PRECO_UNITARIO_ITENS_COTACAO_CENTRAL_COMPRAS);
                    }

                    viewModelEnviarResposta.naoCotouTodosOsItens = somaValoresCotados == 0 ? "sim" : "nao";
                    //----------------------------------------------------------------------

                    if (dadosCotacaoFilha.RESPONDIDA_COTACAO_FILHA_CENTRAL_COMPRAS)
                    {
                        viewModelEnviarResposta.cotacaoRespondida = "sim";
                    }
                    else
                    {
                        viewModelEnviarResposta.cotacaoRespondida = "nao";
                    }

                    if (dadosCotacaoFilha.RECEBEU_CONTRA_PROPOSTA == true)
                    {
                        viewModelEnviarResposta.inRecebeuContraProposta = "sim";

                        if (dadosCotacaoFilha.ACEITOU_CONTRA_PROPOSTA == true)
                        {
                            viewModelEnviarResposta.inAceitouContraProposta = "sim";
                            viewModelEnviarResposta.mensagemStatus = "VOCÊ ACEITOU A CONTRA-PROPOSTA PARA ESTA COTAÇÃO";

                            viewModelEnviarResposta.inRespondeuContraProposta = "sim";
                        }
                        else if (dadosCotacaoFilha.ACEITOU_CONTRA_PROPOSTA == false)
                        {
                            viewModelEnviarResposta.inAceitouContraProposta = "nao";
                            viewModelEnviarResposta.mensagemStatus = "VOCÊ NÃO ACEITOU A CONTRA-PROPOSTA PARA ESTA COTAÇÃO";

                            viewModelEnviarResposta.inRespondeuContraProposta = "nao";
                        }
                    }
                    else
                    {
                        viewModelEnviarResposta.inRecebeuContraProposta = "nao";
                        viewModelEnviarResposta.inRespondeuContraProposta = "nao";
                    }

                    //---------------------------------------------------------------------------------------------------------------------------
                    //VERIFICAR TODOS os PEDIDOS para ESTA COTAÇÃO
                    List<pedido_central_compras> listaDePedidosParaACotacao = negociosPedidosCentralCompras.BuscarTodosOsPedidosParaACotacao(iCM);

                    for (int i = 0; i < listaDePedidosParaACotacao.Count; i++)
                    {
                        if (idsPedidos == "")
                        {
                            idsPedidos = listaDePedidosParaACotacao[i].ID_CODIGO_PEDIDO_CENTRAL_COMPRAS.ToString();
                        }
                        else
                        {
                            idsPedidos = (idsPedidos + "," + listaDePedidosParaACotacao[i].ID_CODIGO_PEDIDO_CENTRAL_COMPRAS.ToString());
                        }
                    }

                    //BUSCAR LISTA de ITENS PEDIDOS
                    List<itens_pedido_central_compras> listaDeItensPedidos = new List<itens_pedido_central_compras>();

                    if (idsPedidos != "")
                    {
                        listaDeItensPedidos = negociosItensPedidosCentralCompras.ConsultarListaDeItensJahPedidos(idsPedidos);
                    }

                    if (listaDeItensPedidos.Count == listaDeItensDaCotacaoFilha.Count)
                    {
                        ViewBag.TodosItensPedidos = "sim";
                    }
                    else
                    {
                        ViewBag.TodosItensPedidos = "nao";

                        textoMsgStatus = "PEDIDO PARCIAL";
                    }
                    //---------------------------------------------------------------------------------------------------------------------------

                    //VIEWBAGS
                    ViewBag.idPedido = 0;
                    ViewBag.dataHoje = diaDaSemana + ", " + diaDoMes + " de " + mesAtual + " de " + anoAtual;
                    ViewBag.ondeEstouAgora = "Cotações Recebidas > Visualizar / Responder Cotação";

                    //VERIFICAR se o FORNECEDOR recebeu PEDIDO para ESTA COTAÇÃO
                    pedido_central_compras estaCotacaoRecebeuPedido = negociosPedidosCentralCompras.VerificarSeEstaCotacaoRecebeuPedido(iCM, iCCF);

                    if (estaCotacaoRecebeuPedido != null)
                    {
                        ViewBag.idPedido = estaCotacaoRecebeuPedido.ID_CODIGO_PEDIDO_CENTRAL_COMPRAS;
                    }

                    if ((dadosCotacaoFilha.RECEBEU_PEDIDO) && (dadosCotacaoFilha.CONFIRMOU_PEDIDO == false))
                    {
                        viewModelEnviarResposta.mensagemStatus = textoMsgStatus + " RECEBIDO - CLIQUE em CONFIRMAR PEDIDO, GARANTINDO o RECEBIMENTO e ACEITAÇÃO do PEDIDO";
                    }
                    else if ((dadosCotacaoFilha.RECEBEU_PEDIDO) && (dadosCotacaoFilha.CONFIRMOU_PEDIDO))
                    {
                        viewModelEnviarResposta.mensagemStatus = textoMsgStatus + " RECEBIDO e CONFIRMADO - AGILIZE a ENTREGA. VOCÊ SERÁ AVALIADO POR ISSO NO SISTEMA";
                    }

                    return View(viewModelEnviarResposta);
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

        public ActionResult EmpresasParticipantesDaCotacaoCentralCompras(int cCC, int iCM, int iCCF)
        {
            try
            {
                if ((Sessao.IdEmpresaUsuario > 0) && (cCC > 0) && (iCM > 0) && (iCCF > 0))
                {
                    int valor = 0;
                    decimal percentualIdealConfirmado = 0;
                    string mensagem = "";

                    DateTime dataHoje = DateTime.Today;

                    //Montando o dia por extenso a se exibido no site (* Não se usa mais isso)
                    string diaDaSemana = new CultureInfo("pt-BR").DateTimeFormat.GetDayName(dataHoje.DayOfWeek);
                    diaDaSemana = char.ToUpper(diaDaSemana[0]) + diaDaSemana.Substring(1);

                    int diaDoMes = dataHoje.Day;
                    string mesAtual = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(dataHoje.Month);
                    mesAtual = char.ToUpper(mesAtual[0]) + mesAtual.Substring(1);
                    int anoAtual = dataHoje.Year;

                    NCentralDeComprasService negociosCentralDeCompras = new NCentralDeComprasService();
                    NEmpresaUsuarioService negociosEmpresaUsuario = new NEmpresaUsuarioService();
                    NUsuarioEmpresaService negociosUsuarioEmpresa = new NUsuarioEmpresaService();
                    NGruposAtividadesEmpresaProfissionalService negociosGrupoAtividadesEmpresa = new NGruposAtividadesEmpresaProfissionalService();
                    NEnderecoEmpresaUsuarioService negociosLocalizacao = new NEnderecoEmpresaUsuarioService();
                    NCotacaoMasterCentralDeComprasService negociosCotacaoMaster = new NCotacaoMasterCentralDeComprasService();
                    NCotacaoFilhaCentralDeComprasService negociosCotacaoFilhaCC = new NCotacaoFilhaCentralDeComprasService();
                    NChatCotacaoCentralComprasService negociosChatCotacaoCentralCompras = new NChatCotacaoCentralComprasService();

                    //CARREGAR DADOS da COTAÇÃO MASTER
                    cotacao_master_central_compras dadosDaCotacaoMaster = negociosCotacaoMaster.ConsultarDadosDaCotacaoMasterCC(iCM);

                    //CARREGAR os DADOS da CENTRAL de COMPRAS
                    central_de_compras dadosDaCentralDeCompras = negociosCentralDeCompras.ConsultarDadosGeraisSobreACentralDeComprasPeloID(cCC);

                    empresa_usuario dadosEmpresaADM =
                        negociosEmpresaUsuario.ConsultarDadosDaEmpresa(new empresa_usuario { ID_CODIGO_EMPRESA = dadosDaCentralDeCompras.ID_CODIGO_EMPRESA_ADM_CENTRAL_COMPRAS });
                    usuario_empresa dadosUsuarioEmpresaADM =
                        negociosUsuarioEmpresa.ConsultarDadosDoUsuarioDaEmpresa(dadosDaCentralDeCompras.ID_CODIGO_USUARIO_ADM_CENTRAL_COMPRAS);

                    empresa_usuario dadosEmpresaLogada =
                        negociosEmpresaUsuario.ConsultarDadosDaEmpresa(new empresa_usuario { ID_CODIGO_EMPRESA = Convert.ToInt32(Sessao.IdEmpresaUsuario) });
                    usuario_empresa dadosUsuarioEmpresaLogada =
                        negociosUsuarioEmpresa.ConsultarDadosDoUsuarioDaEmpresa(Convert.ToInt32(Sessao.IdEmpresaUsuario));

                    grupo_atividades_empresa dadosGruposAtividadesCC =
                        negociosGrupoAtividadesEmpresa.ConsultarDadosDoGrupoDeAtividadesDaEmpresa(new grupo_atividades_empresa { ID_GRUPO_ATIVIDADES = dadosDaCentralDeCompras.ID_GRUPO_ATIVIDADES });
                    List<ListaDadosDeLocalizacaoViewModel> dadosLocalizacaoEmpresaADM =
                        negociosLocalizacao.ConsultarDadosDaLocalizacaoPeloCodigo(dadosEmpresaADM.ID_CODIGO_ENDERECO_EMPRESA_USUARIO);

                    //CONSULTAR DADOS da COTAÇÃO FILHA
                    cotacao_filha_central_compras dadosCotacaoFilha = negociosCotacaoFilhaCC.ConsultarDadosDaCotacaoFilhaCC(iCM, iCCF);

                    //---------------------------------------------------------------------------------------------
                    //Busca os dados do CHAT entre a EMPRESA COTANTE e a EMPRESA FORNECEDORA
                    List<chat_cotacao_central_compras> listaConversasApuradasNoChat =
                        negociosChatCotacaoCentralCompras.BuscarChatEntreEmpresaCotanteEFornecedor(dadosCotacaoFilha.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS);

                    valor = listaConversasApuradasNoChat.Count;

                    if (valor > 0)
                    {
                        if (valor % 2 == 0)
                        {
                            //Par - TEM NOVA MENSAGEM
                            mensagem = "tem";
                        }
                        else
                        {
                            //Impar - NÃO TEM NOVA MENSAGEM
                            mensagem = "naoTem";
                        }
                    }
                    else
                    {
                        mensagem = "naoTem";
                    }
                    //---------------------------------------------------------------------------------------------

                    DadosDaCotacaoViewModel viewModelEnviarResposta = new DadosDaCotacaoViewModel();

                    empresa_usuario dadosEmpresa = negociosEmpresaUsuario.ConsultarDadosDaEmpresa(new empresa_usuario { ID_CODIGO_EMPRESA = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    usuario_empresa dadosUsuarioEmpresa = negociosUsuarioEmpresa.ConsultarDadosDoUsuarioDaEmpresa(Convert.ToInt32(Session["IdUsuarioLogado"]));

                    //POPULAR VIEW MODEL
                    viewModelEnviarResposta.cCC = cCC;
                    viewModelEnviarResposta.iCM = iCM;
                    viewModelEnviarResposta.iCCF = iCCF;
                    viewModelEnviarResposta.inNomeCentralCompras = dadosDaCentralDeCompras.NOME_CENTRAL_COMPRAS;
                    viewModelEnviarResposta.inCodEmpresaADM = dadosDaCentralDeCompras.ID_CODIGO_EMPRESA_ADM_CENTRAL_COMPRAS;
                    viewModelEnviarResposta.ineACriptografado = MD5Crypt.Criptografar(dadosDaCentralDeCompras.ID_CODIGO_EMPRESA_ADM_CENTRAL_COMPRAS.ToString());
                    viewModelEnviarResposta.inEmpresaLogadaAdmCC = dadosEmpresaADM.NOME_FANTASIA_EMPRESA.ToUpper();
                    viewModelEnviarResposta.inCodEmpresaLogada = (int)Sessao.IdEmpresaUsuario;
                    viewModelEnviarResposta.NOME_FANTASIA_EMPRESA = dadosEmpresaLogada.NOME_FANTASIA_EMPRESA.ToUpper();
                    viewModelEnviarResposta.inNomeUsuarioLogado = dadosUsuarioEmpresaLogada.NOME_USUARIO;
                    viewModelEnviarResposta.NOME_USUARIO = dadosUsuarioEmpresaLogada.NOME_USUARIO;
                    viewModelEnviarResposta.inRamoAtividadeCC = dadosGruposAtividadesCC.DESCRICAO_ATIVIDADE;
                    viewModelEnviarResposta.inCodRamoAtividadeCC = dadosGruposAtividadesCC.ID_GRUPO_ATIVIDADES;
                    viewModelEnviarResposta.inNomEmpresaAdmCC = dadosEmpresaADM.NOME_FANTASIA_EMPRESA;
                    viewModelEnviarResposta.inNomeUsuarioRespEmpresaAdmCC = dadosUsuarioEmpresaLogada.NOME_USUARIO;
                    viewModelEnviarResposta.inCidadeEmpresaAdmCC = dadosLocalizacaoEmpresaADM[0].CIDADE_EMPRESA_USUARIO;

                    //VIEWBAGS
                    ViewBag.dataHoje = diaDaSemana + ", " + diaDoMes + " de " + mesAtual + " de " + anoAtual;
                    ViewBag.ondeEstouAgora = "Cotações Recebidas > Empresas Cotantes da Central";

                    return View(viewModelEnviarResposta);
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

        //POPULAR LISTA de GRUPOS de ATIVIDADES
        private static List<SelectListItem> ListaDeGruposDeAtividades()
        {
            try
            {
                //Buscar GRUPOS de ATIVIDADES
                NGruposAtividadesEmpresaProfissionalService negociosGruposAtividades = new NGruposAtividadesEmpresaProfissionalService();
                List<grupo_atividades_empresa> listaGruposDeAtividades = negociosGruposAtividades.ListaGruposAtividadesEmpresaProfissional();

                List<SelectListItem> listRamosAtividades = new List<SelectListItem>();

                listRamosAtividades.Add(new SelectListItem { Text = "Selecione...", Value = "0" });

                foreach (var grupoAtividades in listaGruposDeAtividades)
                {
                    listRamosAtividades.Add(new SelectListItem
                    {
                        Text = grupoAtividades.DESCRICAO_ATIVIDADE,
                        Value = grupoAtividades.ID_GRUPO_ATIVIDADES.ToString()
                    });
                }

                return listRamosAtividades;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //POPULAR LISTA de GRUPOS de ATIVIDADES
        private static List<SelectListItem> ListagemTiposDeCotacao()
        {
            try
            {
                List<SelectListItem> listTiposDeCotacao = new List<SelectListItem>();

                listTiposDeCotacao.Add(new SelectListItem { Text = "Selecione...", Value = "0" });
                listTiposDeCotacao.Add(new SelectListItem { Text = "Direcionada", Value = "1" });
                listTiposDeCotacao.Add(new SelectListItem { Text = "Avulsa", Value = "2" });

                return listTiposDeCotacao;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //POPULAR LISTA de GRUPOS de ATIVIDADES
        private static List<SelectListItem> ListaDeTiposDeFiltros()
        {
            try
            {
                List<SelectListItem> listTiposFiltros = new List<SelectListItem>();

                listTiposFiltros.Add(new SelectListItem { Text = "Selecione...", Value = "0" });
                listTiposFiltros.Add(new SelectListItem { Text = "Cotação Específica", Value = "1" });
                listTiposFiltros.Add(new SelectListItem { Text = "Ramo Atividade", Value = "2" });
                listTiposFiltros.Add(new SelectListItem { Text = "Central Compras Específica", Value = "3" });
                listTiposFiltros.Add(new SelectListItem { Text = "Cotações Direcionadas", Value = "4" });
                listTiposFiltros.Add(new SelectListItem { Text = "Cotações Avulsas", Value = "5" });

                return listTiposFiltros;
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //CONSULTA do AUTOCOMPLETE da LISTA de COTAÇÕES da CENTRAL de COMPRAS
        [WebMethod]
        public JsonResult BuscarListaDeNomesCotacaoesDaCC(string term)
        {
            //NCentralDeComprasService negociosCentraisCompras = new NCentralDeComprasService();
            NCotacaoMasterCentralDeComprasService negociosCotacaoMaster = new NCotacaoMasterCentralDeComprasService();

            //CARREGA LISTA de COTAÇÕES da CENTRAL de COMPRAS
            List<cotacao_master_central_compras> listaCotacaoMasterCC = negociosCotacaoMaster.CarregarListaAutoCompleteDasCotacoesDaCC(term);

            return Json(listaCotacaoMasterCC, JsonRequestBehavior.AllowGet);
        }

        //CONSULTA do AUTOCOMPLETE da LISTA de CENTRAIS de COMPRAS
        [WebMethod]
        public JsonResult BuscarListaDescricaoCC(string term)
        {
            NCentralDeComprasService negociosCentraisCompras = new NCentralDeComprasService();

            //CARREGA LISTA de CENTRAIS de COMPRAS
            List<ListaCentraisComprasViewModel> listaCC = negociosCentraisCompras.CarregarListaAutoCompleteCentraisDeCompras(term);

            return Json(listaCC, JsonRequestBehavior.AllowGet);
        }

        //BUSCAR LISTA de COTAÇÕES RECEBIDAS da CENTRAL de COMPRAS
        [WebMethod]
        public ActionResult BuscarListaDeCotacoeRecebidas(int tipoFiltragem, int codPesquisar, int idGrupoAtividadesFiltro)
        {
            try
            {
                NCotacaoFilhaCentralDeComprasService negociosCotacaoFilha = new NCotacaoFilhaCentralDeComprasService();

                //BUSCAR LISTA de COTAÇÕES RECEBIDAS FILTRADAS
                List<ListaDeCotacoesRecebidasPeloFornecedorViewModel> listaDeCotacoesFiltradas =
                    negociosCotacaoFilha.BuscarListaDeCotacoesRecebidasPeloFornecedorConformeFiltro(tipoFiltragem, codPesquisar, idGrupoAtividadesFiltro);

                return Json(
                        new
                        {
                            rows = listaDeCotacoesFiltradas,
                            current = 1,
                            rowCount = listaDeCotacoesFiltradas.Count,
                            total = listaDeCotacoesFiltradas.Count,
                            dadosCarregados = "Ok"
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //BUSCAR LISTA de PARTICPANTES e COTANTES da CENTRAL de COMPRAS, nesta COTAÇÃO
        [WebMethod]
        public ActionResult EmpresasParticipantesCotantesDaCC(int iCM, int cCC)
        {
            NCotacaoIndividualEmpresaCentralComprasService negociosCotacaoIndividual = new NCotacaoIndividualEmpresaCentralComprasService();

            //CARREGAR LISTA de EMPRESAS COTANTES participantes desta COTAÇÃO
            List<ListaDeEmpresasCotantesDeUmaCotacaoViewModel> listaDeEmpresasCotantesDestaCotacao =
                negociosCotacaoIndividual.BuscarListaDeEmpresasCotantesNestaCotacao(iCM, cCC);

            return Json(
                    new
                    {
                        rows = listaDeEmpresasCotantesDestaCotacao,
                        current = 1,
                        rowCount = listaDeEmpresasCotantesDestaCotacao.Count,
                        total = listaDeEmpresasCotantesDestaCotacao.Count,
                        dadosCarregados = "Ok"
                    },
                    JsonRequestBehavior.AllowGet);
        }

        //BUSCAR LISTA de PARTICPANTES e COTANTES da CENTRAL de COMPRAS, nesta COTAÇÃO
        [WebMethod]
        public ActionResult CarregarOsItensDaCotacaoDaCC(int iCM, int idEmpresaCotada)
        {
            NCotacaoFilhaCentralDeComprasService negociosCotacaoFilha = new NCotacaoFilhaCentralDeComprasService();
            NItensCotacaoFilhaNegociacaoCentralDeComprasService negociosItensCotacaoFilha = new NItensCotacaoFilhaNegociacaoCentralDeComprasService();

            //DADOS da COTAÇÃO FILHA RECEBIDA pela EMPRESA em questão
            cotacao_filha_central_compras dadosCotacaoFilha =
                negociosCotacaoFilha.ConsultarDadosDaCotacaoFilhaCCPeloCodigoDaEmpresaFornecedora(iCM, idEmpresaCotada);

            //CARREGAR ITENS da COTAÇÃO RECEBIDA
            List<ListaDeItensCotadosViewModel> listaDeItensDaCotacaoFilhaDaCC =
                negociosItensCotacaoFilha.CarregarOsItensDeUmaCotacaoRecebida(dadosCotacaoFilha.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS);

            return Json(
                    new
                    {
                        rows = listaDeItensDaCotacaoFilhaDaCC,
                        current = 1,
                        rowCount = listaDeItensDaCotacaoFilhaDaCC.Count,
                        total = listaDeItensDaCotacaoFilhaDaCC.Count,
                        dadosCarregados = "Ok"
                    },
                    JsonRequestBehavior.AllowGet);
        }

        //Autocomplete EMBALAGENS
        public JsonResult AutoCompleteProdutoEmbalagens(string term, int codProduto)
        {
            try
            {
                //Buscar lista de Fabricantes & Marcas registrados no Sistema
                NEmpresasProdutosEmbalagensService negociosEmpresasProdutosEmbalagens = new NEmpresasProdutosEmbalagensService();
                List<ListaDeEmbalagensDosProdutosViewModel> listaEmbalagens = new List<ListaDeEmbalagensDosProdutosViewModel>();

                //TRAZ EMBALAGENS VINCULADAS AO PRODUTO REGISTRADO
                listaEmbalagens = negociosEmpresasProdutosEmbalagens.ListaDeEmbalagensVinculadasAoProduto(term, codProduto);

                return Json(listaEmbalagens, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //RESPONDER a COTAÇÃO pela PRIMEIRA VEZ
        [WebMethod]
        public ActionResult ResponderACotacaoAosFornecedores(int iCM, int idEmpresaCotada, string codCotacao, string idsItensCotados, string valoresTabelaItensCotados,
            string valoresDiferenciadosItensCotados)
        {
            try
            {
                var resultado = new { cotacaoRespondida = "" };

                //RECEBENDO os ARRAYS
                string[] idsItensCotados2, valoresTabelaItensCotados2, valoresDiferenciadosItensCotados2;

                idsItensCotados2 = idsItensCotados.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                valoresTabelaItensCotados2 = valoresTabelaItensCotados.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                valoresDiferenciadosItensCotados2 = valoresDiferenciadosItensCotados.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                NCotacaoFilhaCentralDeComprasService negociosCotacaoFilha = new NCotacaoFilhaCentralDeComprasService();
                NItensCotacaoFilhaNegociacaoCentralDeComprasService negociosItensCotacaoFilha = new NItensCotacaoFilhaNegociacaoCentralDeComprasService();

                //DADOS da COTAÇÃO FILHA RECEBIDA pela EMPRESA em questão
                cotacao_filha_central_compras dadosCotacaoFilha =
                    negociosCotacaoFilha.ConsultarDadosDaCotacaoFilhaCCPeloCodigoDaEmpresaFornecedora(iCM, idEmpresaCotada);

                //SALVAR PREÇOS RESPONDIDOS para a COTAÇÃO RECEBIDA
                bool cotacaoRespondida =
                    negociosItensCotacaoFilha.ResponderACotacaoPelaPrimeiraVez(dadosCotacaoFilha.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS, idsItensCotados2, 
                    valoresTabelaItensCotados2, valoresDiferenciadosItensCotados2);

                if (cotacaoRespondida)
                {
                    //SETAR ESTA COTAÇÃO como RESPONDIDA por este FORNECEDOR
                    negociosCotacaoFilha.SetarComoRespondidaEstaCotacaoPorEsteFornecedor(iCM, dadosCotacaoFilha.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS);

                    resultado = new { cotacaoRespondida = "sim" };
                }

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //---------------------------------------------------------------------------
        //RESPONDER a COTAÇÃO pela PRIMEIRA VEZ
        [WebMethod]
        public ActionResult CancelarRespostaEnviadaACotacao(int iCM, int idEmpresaCotada, string codCotacao)
        {
            try
            {
                var resultado = new { cotacaoRespondida = "" };

                /*
                 SETAR O CANCELAMENTO DA COTAÇÃO... CONTINUAR AQUI.
                 */

                ////RECEBENDO os ARRAYS
                //string[] idsItensCotados2, valoresTabelaItensCotados2, valoresDiferenciadosItensCotados2;

                //idsItensCotados2 = idsItensCotados.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //valoresTabelaItensCotados2 = valoresTabelaItensCotados.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //valoresDiferenciadosItensCotados2 = valoresDiferenciadosItensCotados.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                //NCotacaoFilhaCentralDeComprasService negociosCotacaoFilha = new NCotacaoFilhaCentralDeComprasService();
                //NItensCotacaoFilhaNegociacaoCentralDeComprasService negociosItensCotacaoFilha = new NItensCotacaoFilhaNegociacaoCentralDeComprasService();

                ////DADOS da COTAÇÃO FILHA RECEBIDA pela EMPRESA em questão
                //cotacao_filha_central_compras dadosCotacaoFilha =
                //    negociosCotacaoFilha.ConsultarDadosDaCotacaoFilhaCCPeloCodigoDaEmpresaFornecedora(iCM, idEmpresaCotada);

                ////SALVAR PREÇOS RESPONDIDOS para a COTAÇÃO RECEBIDA
                //bool cotacaoRespondida =
                //    negociosItensCotacaoFilha.ResponderACotacaoPelaPrimeiraVez(dadosCotacaoFilha.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS, idsItensCotados2,
                //    valoresTabelaItensCotados2, valoresDiferenciadosItensCotados2);

                //if (cotacaoRespondida)
                //{
                //    //SETAR ESTA COTAÇÃO como RESPONDIDA por este FORNECEDOR
                //    negociosCotacaoFilha.SetarComoRespondidaEstaCotacaoPorEsteFornecedor(iCM, dadosCotacaoFilha.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS);

                //    resultado = new { cotacaoRespondida = "sim" };
                //}

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //---------------------------------------------------------------------------

        //SETAR CONTRA-PROPOSTA COMO ACEITA para a COTAÇÃO
        [WebMethod]
        public ActionResult AceitarContraProposta(int iCM, int iCCF)
        {
            try
            {
                var resultado = new { contraPropostaAceita = "" };

                NCotacaoFilhaCentralDeComprasService negociosCotacaoFilha = new NCotacaoFilhaCentralDeComprasService();

                negociosCotacaoFilha.SetarContraPropostaComoAceitaPeloFornecedor(iCM, iCCF);

                resultado = new { contraPropostaAceita = "sim" };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //SETAR CONTRA-PROPOSTA COMO NÃO ACEITA para a COTAÇÃO
        [WebMethod]
        public ActionResult NaoAceitarContraProposta(int iCM, int iCCF)
        {
            try
            {
                var resultado = new { contraPropostaNaoAceita = "" };

                NCotacaoFilhaCentralDeComprasService negociosCotacaoFilha = new NCotacaoFilhaCentralDeComprasService();

                negociosCotacaoFilha.SetarContraPropostaComoNaoAceitaPeloFornecedor(iCM, iCCF);

                resultado = new { contraPropostaNaoAceita = "sim" };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //---------------------------------------------------------------------------
        //SETAR CONFIRMANDO o PEDIDO como ACEITO
        [WebMethod]
        public ActionResult ConfirmarRecebimentoEAcatamentoDoPedido(int iCM, int iCCF, int idPedido)
        {
            try
            {
                var resultado = new { pedidoConfirmado = "" };

                NCotacaoFilhaCentralDeComprasService negociosCotacaoFilhaCC = new NCotacaoFilhaCentralDeComprasService();
                NCotacaoMasterCentralDeComprasService negociosCotacaoMasterCC = new NCotacaoMasterCentralDeComprasService();

                //CONFIRMAR o ACEITE do PEDIDO
                negociosCotacaoFilhaCC.SetarConfirmandoAceiteDoPedido(iCM, iCCF, idPedido);

                //IDENTIFICAR na COTAÇÃO MASTER o FORNECEDOR q RECEBEU PEDIDO
                negociosCotacaoMasterCC.SetarIdFornecedorNaCotacaoMaster(iCM);

                resultado = new { pedidoConfirmado = "sim" };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //---------------------------------------------------------------------------

        //Carrega DIÁLOGOS entre COTANTE e FORNECEDOR
        [WebMethod]
        public JsonResult CarregarDialogoEntreCotanteEFornecedor(int idCotacaoFilha)
        {
            try
            {
                //Carrega CHAT entre USUÁRIO ADM da CENTRAL COMPRAS e FORNECEDOR
                NChatCotacaoUsuarioEmpresaService negociosChatCotacaoUsuarioEmpresa = new NChatCotacaoUsuarioEmpresaService();

                NChatCotacaoCentralComprasService negociosChatCotacaoCC = new NChatCotacaoCentralComprasService();
                NEmpresaUsuarioService negociosEmpresa = new NEmpresaUsuarioService();
                List<ChatEntreUsuarioEFornecedor> listaDeConversasEntreCotanteEFornecedorNoChat = new List<ChatEntreUsuarioEFornecedor>();

                //Busca os dados do CHAT entre o COTANTE e o FORNECEDOR
                List<chat_cotacao_central_compras> listaConversasApuradasNoChat =
                    negociosChatCotacaoCC.BuscarChatEntreUsuarioAdmDaCCEFornecedor(idCotacaoFilha);

                if (listaConversasApuradasNoChat != null)
                {
                    //Montagem da lista de Fornecedores
                    for (int a = 0; a < listaConversasApuradasNoChat.Count; a++)
                    {
                        empresa_usuario dadosEmpresa =
                            negociosEmpresa.ConsultarDadoDaEmpresaPeloCodigoUsuario(listaConversasApuradasNoChat[a].ID_CODIGO_USUARIO_EMPRESA_COTANTE);

                        string[] nomeEmpresa = dadosEmpresa.NOME_FANTASIA_EMPRESA.Split(' ');

                        listaDeConversasEntreCotanteEFornecedorNoChat.Add(
                            new ChatEntreUsuarioEFornecedor(
                                listaConversasApuradasNoChat[a].ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS,
                                listaConversasApuradasNoChat[a].ID_CODIGO_USUARIO_EMPRESA_COTADA,
                                nomeEmpresa[0],
                                listaConversasApuradasNoChat[a].DATA_CHAT_COTACAO_CENTRAL_COMPRAS.ToString(),
                                listaConversasApuradasNoChat[a].TEXTO_CHAT_COTACAO_CENTRAL_COMPRAS,
                                listaConversasApuradasNoChat[a].ORDEM_EXIBICAO_CHAT_COTACAO_CENTRAL_COMPRAS
                                )
                            );
                    }
                }

                return Json(listaDeConversasEntreCotanteEFornecedorNoChat, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        //Carrega DIÁLOGOS entre COTANTE e FORNECEDOR
        public JsonResult GravarDialogoEntreCotanteDaCCEFornecedor(int idCotacaoFilha, int idUsuarioEmpresaCotante, int idUsuarioEmpresaCotada, string textoPerguntaOuResposta)
        {
            try
            {
                string retornoGravacao = "nok";

                var resultado = new { gravacaoChat = "" };

                //USUÁRIO EMPRESA COTANTE
                NChatCotacaoCentralComprasService negociosChatCotacaoCC = new NChatCotacaoCentralComprasService();
                chat_cotacao_central_compras dadosChatCotacaoCC = new chat_cotacao_central_compras();

                int numeroDeOrdemNaExibicao = (negociosChatCotacaoCC.ConsultarNumeroDeOrdemDeExibicaoNoChat(idCotacaoFilha) + 1);

                //Preparando os dados a serem gravados
                dadosChatCotacaoCC.ID_CODIGO_COTACAO_FILHA_CENTRAL_COMPRAS = idCotacaoFilha;
                dadosChatCotacaoCC.ID_CODIGO_USUARIO_EMPRESA_COTANTE = idUsuarioEmpresaCotante;
                dadosChatCotacaoCC.ID_CODIGO_USUARIO_EMPRESA_COTADA = idUsuarioEmpresaCotada;
                dadosChatCotacaoCC.DATA_CHAT_COTACAO_CENTRAL_COMPRAS = DateTime.Now;
                dadosChatCotacaoCC.TEXTO_CHAT_COTACAO_CENTRAL_COMPRAS = textoPerguntaOuResposta;
                dadosChatCotacaoCC.ID_CODIGO_USUARIO_DIALOGANDO = (int)Sessao.IdUsuarioLogado;
                dadosChatCotacaoCC.ORDEM_EXIBICAO_CHAT_COTACAO_CENTRAL_COMPRAS = numeroDeOrdemNaExibicao;

                //Gravar CONVERSA no CHAT - CENTRAL COMPRAS
                chat_cotacao_central_compras gravarPerguntaOuRespostaDoChat = negociosChatCotacaoCC.GravarConversaNoChat(dadosChatCotacaoCC);

                if (gravarPerguntaOuRespostaDoChat != null)
                {
                    retornoGravacao = "ok";
                }

                //Resultado a ser retornado
                resultado = new
                {
                    gravacaoChat = retornoGravacao
                };

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
    }
}
