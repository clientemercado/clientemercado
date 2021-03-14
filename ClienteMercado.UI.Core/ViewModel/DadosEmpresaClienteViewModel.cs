﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ClienteMercado.Data.Entities;

namespace ClienteMercado.UI.Core.ViewModel
{
    public class DadosEmpresaClienteViewModel : Usuario_EmpresaCliente
    {
        public int iEC { get; set; }
        public int iUEC { get; set; }
        public int iUCE { get; set; }
        public int iCEC { get; set; }
        public int iCDEC { get; set; }
        public int iDEC { get; set; }
        public int iSDEC { get; set; }
        public int iPVDEC { get; set; }
        public int iEFM { get; set; }
        public int iLEC { get; set; }
        public int iMPEC { get; set; }
        public int iPEC { get; set; }
        public string nomeEmpresaLogada { get; set; }
        public string nomeUsuarioEmpresaLogada { get; set; }
        public string cnpj_EmpresaCliente { get; set; }
        public string razaoSocial_EmpresaCliente { get; set; }
        public string nomeFantasia_EmpresaCliente { get; set; }
        public string logomarca_EmpresaCliente { get; set; }
        public string endereco_EmpresaCliente { get; set; }
        public string bairro_EmpresaCliente { get; set; }
        public string complementoEndereco_EmpresaCliente { get; set; }
        public string cepEndereco_EmpresaCliente { get; set; }
        public string cidade_EmpresaCliente { get; set; }
        public string uf_EmpresaCliente { get; set; }
        public string pais_EmpresaCliente { get; set; }
        public string telefone1_EmpresaCliente { get; set; }
        public string telefone2_EmpresaCliente { get; set; }
        public string email1_EmpresaCliente { get; set; }
        public string email2_EmpresaCliente { get; set; }
        public bool receberEmails_EmpresaCliente { get; set; }
        public bool aceitacaoTermosPolitica_EmpresaCliente { get; set; }
        public DateTime? dataCadastro_EmpresaCliente { get; set; }
        public bool ativaInativa_EmpresaCliente { get; set; }
        public DateTime? dataInativou_EmpresaCliente { get; set; }
        public int idUsuarioInativou_EmpresaCliente { get; set; }
        public float? latitude_logitude_cep_EmpresaCliente { get; set; }
        public List<SelectListItem> ListagemPaises { get; set; }
        public List<SelectListItem> ListagemEstados { get; set; }

        public string nomeCupom_CupomDescontoEmpresaCliente { get; set; }
        public string dataValidade_CupomDescontoEmpresaCliente { get; set; }
        public string percentualDesconto_CupomDescontoEmpresaCliente { get; set; }
        public string ativoInativo_CupomDescontoEmpresaCliente { get; set; }

        public string descricao_DepartamentoEmpresaCliente { get; set; }
        public string ativoInativo_DepartamentoEmpresaCliente { get; set; }

        public string descricao_SubDepartamentoEmpresaCliente { get; set; }
        public int id_DepartamentoEmpresaCliente { get; set; }
        public List<SelectListItem> ListagemDepartamentos { get; set; }

        public string nomeOferta_PromocaoVendaEmpresaCliente { get; set; }
        public string dataValidade_PromocaoVendaEmpresaCliente { get; set; }
        public string percentualOffOferta_PromocaoVendaEmpresaCliente { get; set; }
        public string ativoInativo_PromocaoVendaEmpresaCliente { get; set; }        
        public string bannerOferta_PromocaoVendaEmpresaCliente { get; set; }

        public string descricao_EmpresaFabricantesMarcas { get; set; }

        public int? id_CidadeEmpresaCliente { get; set; }
        public string nomeLocalidade_LocalidadeCidadeEmpresaCliente { get; set; }
        public string cepLocalidade_LocalidadeCidadeEmpresaCliente { get; set; }
        public List<SelectListItem> ListagemCidades { get; set; }

        public string descricao_MeiosPagamentoEmpresaCliente { get; set; }

        public int? idPedidoCliente { get; set; }
        public string inCodControlePedidoClienteEntrega { get; set; }
        public string nomeClienteEntrega { get; set; }
        public string cidadeClienteEntrega { get; set; }
        public string ufClienteEntrega { get; set; }
        public string localidadeClienteEntrega { get; set; }
        public string modoPagamentoClienteEntrega { get; set; }
        public string cuponDescontoClienteEntrega { get; set; }
        public string valorPedido_PedidoClienteEmpresaCliente { get; set; }
        public string pedidoEntregue_PedidoClienteEmpresaCliente { get; set; }
        public int? idUsuarioEmpresaEntregou_ClienteEmpresaCliente { get; set; }
        public string nomeUsuarioFuncionarioEntrega { get; set; }
        public string dataEntregaPedido_ClienteEmpresaCliente { get; set; }
        public List<SelectListItem> ListagemOpcoesSimNao { get; set; }


        public string descricao_ProdutoEmpresaCliente { get; set; }
        public string tipoEmbalagem_ProdutoEmpresaCliente { get; set; }
        public string pesoEmbalagem_ProdutoEmpresaCliente { get; set; }
        public string unidadePesoEmbalagem_ProdutoEmpresaCliente { get; set; }
        public string valorVenda_ProdutoEmpresaCliente { get; set; }
        public int? id_SubDepartamentoEmpresaCliente { get; set; }
        public int? id_EmpresaFabricantesMarcas { get; set; }
        public int? id_PromocaoVendaEmpresaCliente { get; set; }
        public string ativoInativo_ProdutoEmpresaCliente { get; set; }
        public List<SelectListItem> ListagemSubDepartamentos { get; set; }
        public List<SelectListItem> ListagemFabricantesMarcas { get; set; }
        public List<SelectListItem> ListagemPromocoesAtivas { get; set; }
    }
}
