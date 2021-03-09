using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;
using System;
using System.Collections.Generic;

namespace ClienteMercado.Domain.Services
{
    public class NUsuarioEmpresaService
    {
        DUsuarioEmpresaRepository dusuarioempresa = new DUsuarioEmpresaRepository();

        //Consultar Usuários ligados à Empresa
        public List<usuario_empresa> ConsultarUsuariosLigadosAEmpresa(usuario_empresa obj)
        {
            return dusuarioempresa.ConsultarUsuariosLigadosAEmpresa(obj);
        }

        //Consultar dados dos Vendedores que receberão aviso de cotação
        public List<usuario_empresa> ConsultarDadosDosUsuariosVendedoresQueReceberaoAvisoDeCotacao(string[] listaIDsFornecedores)
        {
            return dusuarioempresa.ConsultarDadosDosUsuariosVendedoresQueReceberaoAvisoDeCotacao(listaIDsFornecedores);
        }

        //Consultar dados de um Usuário da Empresa
        public usuario_empresa ConsultarDadosDoUsuarioDaEmpresa(int idUsuario)
        {
            return dusuarioempresa.ConsultarDadosDoUsuarioDaEmpresa(idUsuario);
        }

        //GRAVAR DADOS ATUALIZADOS do USUÁRIO
        public usuario_empresa AtualizarDadosCadastrais(usuario_empresa obj)
        {
            return dusuarioempresa.AtualizarDadosCadastrais(obj);
        }

        //CONSULTAR DADOS do USUÁRIO da EMPRESA FORNECEDORA pelo ID da EMPRESA
        public usuario_empresa ConsultarDadosDoUsuarioDaEmpresaFornecedoraPeloIdDaEmpresa(int idEmpresa)
        {
            return dusuarioempresa.ConsultarDadosDoUsuarioDaEmpresaFornecedoraPeloIdDaEmpresa(idEmpresa);
        }

        /// <summary>
        /// Consultar dados do Usuário Funcionário da Empresa Cliente - SUPERMARKET_ON
        /// </summary>
        /// <returns></returns>
        public Usuario_EmpresaCliente ConsultarDadosUsuarioEmpresaCliente(Usuario_EmpresaCliente obj)
        {
            return dusuarioempresa.ConsultarDadosUsuarioEmpresaCliente(obj);
        }

        //GRAVAR NOVA UAUÁRIO da EMPRESA CLIENTE
        public Usuario_EmpresaCliente GravarNovoUsuarioEmpresaCliente(Usuario_EmpresaCliente obj)
        {
            return dusuarioempresa.GravarNovoUsuarioEmpresaCliente(obj);
        }

        /// <summary>
        /// ALTERAR DADOS DO USUÁRIO da EMPRESA CLIENTE - SUPERMARKET_ON
        /// </summary>
        /// <returns></returns>
        public void AlterarDadosUsuarioEmpresaCliente(Usuario_EmpresaCliente obj)
        {
            dusuarioempresa.AlterarDadosUsuarioEmpresaCliente(obj);
        }

        /// <summary>
        /// GERAR DADOS DE LOGON PARA O USUÁRIO
        /// </summary>
        /// <returns></returns>
        public void GerarDadosLogonUsuarioEmpCliente(DadosLogin_UsuarioEmpresaCliente obj)
        {
            dusuarioempresa.GerarDadosLogonUsuarioEmpCliente(obj);
        }
    }
}
