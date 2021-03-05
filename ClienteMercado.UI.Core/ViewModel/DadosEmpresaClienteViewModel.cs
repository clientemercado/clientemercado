﻿using System;
using ClienteMercado.Data.Entities;

namespace ClienteMercado.UI.Core.ViewModel
{
    public class DadosEmpresaClienteViewModel : Usuario_EmpresaCliente
    {
        public string cnpj_EmpresaCliente { get; set; }
        public string razaoSocial_EmpresaCliente { get; set; }
        public string nomeFantasia_EmpresaCliente { get; set; }
        public string logomarca_EmpresaCliente { get; set; }
        public string endereco_EmpresaCliente { get; set; }
        public string bairro_EmpresaCliente { get; set; }
        public string complementoEndereco_EmpresaCliente { get; set; }
        public float cepEndereco_EmpresaCliente { get; set; }
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
    }
}