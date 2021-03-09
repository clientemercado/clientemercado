using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClienteMercado.Infra.Repositories
{
    public class DUsuarioEmpresaRepository : RepositoryBase<usuario_empresa>
    {
        //Consultar Usuários ligados à Empresa
        public List<usuario_empresa> ConsultarUsuariosLigadosAEmpresa(usuario_empresa obj)
        {
            List<usuario_empresa> listaUsuariosFornecedores = _contexto.usuario_empresa.Where(m => (m.ID_CODIGO_EMPRESA.Equals(obj.ID_CODIGO_EMPRESA))).ToList();

            return listaUsuariosFornecedores;
        }

        //Consultar dados dos Vendedores que receberão aviso de cotação
        public List<usuario_empresa> ConsultarDadosDosUsuariosVendedoresQueReceberaoAvisoDeCotacao(string[] listaIDsFornecedores)
        {
            List<usuario_empresa> dadosUsuariosVendedores = new List<usuario_empresa>();

            //Consulta o e-mail de cada ID da lista
            for (int i = 0; i < listaIDsFornecedores.Length; i++)
            {
                int idFornecedor = Convert.ToInt32(listaIDsFornecedores[i]);

                usuario_empresa buscaDadosDoUsuarioVendedor = _contexto.usuario_empresa.FirstOrDefault(m => m.ID_CODIGO_USUARIO.Equals(idFornecedor));

                if (buscaDadosDoUsuarioVendedor != null)
                {
                    dadosUsuariosVendedores.Add(buscaDadosDoUsuarioVendedor);
                }
            }

            return dadosUsuariosVendedores;
        }

        //CONSULTAR DADOS do USUÁRIO da EMPRESA FORNECEDORA pelo ID da EMPRESA
        public usuario_empresa ConsultarDadosDoUsuarioDaEmpresaFornecedoraPeloIdDaEmpresa(int idEmpresa)
        {
            usuario_empresa dadosDoUsuario = _contexto.usuario_empresa.FirstOrDefault(m => (m.ID_CODIGO_EMPRESA == idEmpresa));

            return dadosDoUsuario;
        }

        /// <summary>
        /// Consultar dados do Usuário Funcionário da Empresa Cliente - SUPERMARKET_ON
        /// </summary>
        /// <returns></returns>
        public Usuario_EmpresaCliente ConsultarDadosUsuarioEmpresaCliente(Usuario_EmpresaCliente obj)
        {
            Usuario_EmpresaCliente dadosUsuEmpresaCliente = 
                _contexto.usuario_empresaCliente.FirstOrDefault(m => (m.id_UsuarioEmpresaCliente == obj.id_UsuarioEmpresaCliente));

            return dadosUsuEmpresaCliente;
        }

        /// <summary>
        /// ALTERAR DADOS DO USUÁRIO da EMPRESA CLIENTE - SUPERMARKET_ON
        /// </summary>
        /// <returns></returns>
        public void AlterarDadosUsuarioEmpresaCliente(Usuario_EmpresaCliente obj)
        {
            Usuario_EmpresaCliente dadosUsuarioEmpClienteAlterar =
                _contexto.usuario_empresaCliente.FirstOrDefault(m => ((m.id_UsuarioEmpresaCliente == obj.id_UsuarioEmpresaCliente)
                && (m.id_EmpresaCliente == obj.id_EmpresaCliente)));

            if (dadosUsuarioEmpClienteAlterar != null)
            {
                dadosUsuarioEmpClienteAlterar.nome_UsuarioEmpresaCliente = obj.nome_UsuarioEmpresaCliente;
                dadosUsuarioEmpClienteAlterar.eMail1_UsuarioEmpresaCliente = obj.eMail1_UsuarioEmpresaCliente;
                dadosUsuarioEmpClienteAlterar.telefone1_UsuarioEmpresaCliente = obj.telefone1_UsuarioEmpresaCliente;
                dadosUsuarioEmpClienteAlterar.pais_UsuarioEmpresaCliente = obj.pais_UsuarioEmpresaCliente;
                dadosUsuarioEmpClienteAlterar.cepEndereco_UsuarioEmpresaCliente = obj.cepEndereco_UsuarioEmpresaCliente.Replace(".", "").Replace("-", "");
                dadosUsuarioEmpClienteAlterar.endereco_UsuarioEmpresaCliente = obj.endereco_UsuarioEmpresaCliente;
                dadosUsuarioEmpClienteAlterar.complementoEndereco_UsuarioEmpresaCliente = obj.complementoEndereco_UsuarioEmpresaCliente;
                dadosUsuarioEmpClienteAlterar.bairro_UsuarioEmpresaCliente = obj.bairro_UsuarioEmpresaCliente;
                dadosUsuarioEmpClienteAlterar.cidade_UsuarioEmpresaCliente = obj.cidade_UsuarioEmpresaCliente;
                dadosUsuarioEmpClienteAlterar.uf_UsuarioEmpresaCliente = obj.uf_UsuarioEmpresaCliente;
                //dadosAlteracaoUsuarioEmpCliente.receberEmails_UsuarioEmpresaCliente = true;
                //dadosAlteracaoUsuarioEmpCliente.dataCadastro_UsuarioEmpresaCliente = DateTime.Now;
                //dadosAlteracaoUsuarioEmpCliente.ativoInativo_UsuarioEmpresaCliente = true;

                _contexto.SaveChanges();
            }
        }

        /// <summary>
        /// ALTERAR DADOS DO USUÁRIO CIENTE da EMPRESA
        /// </summary>
        /// <returns></returns>
        public void AlterarDadosUsuarioClienteEmpresa(Cliente_EmpresaCliente obj)
        {
            Cliente_EmpresaCliente dadosUsuarioClienteEmpresaAlterar =
                _contexto.cliente_empresaCliente.FirstOrDefault(m => ((m.id_ClienteEmpresaCliente == obj.id_ClienteEmpresaCliente)
                && (m.id_EmpresaCliente == obj.id_EmpresaCliente)));

            if (dadosUsuarioClienteEmpresaAlterar != null)
            {
                dadosUsuarioClienteEmpresaAlterar.nome_ClienteEmpresaCliente = obj.nome_ClienteEmpresaCliente;
                dadosUsuarioClienteEmpresaAlterar.eMail1_ClienteEmpresaCliente = obj.eMail1_ClienteEmpresaCliente;
                dadosUsuarioClienteEmpresaAlterar.telefone1_ClienteEmpresaCliente= obj.telefone1_ClienteEmpresaCliente;
                dadosUsuarioClienteEmpresaAlterar.pais_ClienteEmpresaCliente = obj.pais_ClienteEmpresaCliente;
                dadosUsuarioClienteEmpresaAlterar.cepEndereco_ClienteEmpresaCliente = obj.cepEndereco_ClienteEmpresaCliente.Replace(".", "").Replace("-", "");
                dadosUsuarioClienteEmpresaAlterar.endereco_ClienteEmpresaCliente = obj.endereco_ClienteEmpresaCliente;
                dadosUsuarioClienteEmpresaAlterar.complementoEndereco_ClienteEmpresaCliente = obj.complementoEndereco_ClienteEmpresaCliente;
                dadosUsuarioClienteEmpresaAlterar.bairro_ClienteEmpresaCliente = obj.bairro_ClienteEmpresaCliente;
                dadosUsuarioClienteEmpresaAlterar.cidade_ClienteEmpresaCliente = obj.cidade_ClienteEmpresaCliente;
                dadosUsuarioClienteEmpresaAlterar.uf_ClienteEmpresaCliente = obj.uf_ClienteEmpresaCliente;
                //dadosAlteracaoUsuarioEmpCliente.receberEmails_UsuarioEmpresaCliente = true;
                //dadosAlteracaoUsuarioEmpCliente.dataCadastro_UsuarioEmpresaCliente = DateTime.Now;
                //dadosAlteracaoUsuarioEmpCliente.ativoInativo_UsuarioEmpresaCliente = true;

                _contexto.SaveChanges();
            }
        }

        /// <summary>
        /// BUSCAR DADOS DO CLIENTE DA EMPRESA
        /// </summary>
        /// <returns></returns>
        public Cliente_EmpresaCliente ConsultarDadosClienteEmpresa(Cliente_EmpresaCliente obj)
        {
            Cliente_EmpresaCliente dadosClienteEmpresa = 
                _contexto.cliente_empresaCliente.FirstOrDefault(m => (m.id_ClienteEmpresaCliente == obj.id_ClienteEmpresaCliente));

            return dadosClienteEmpresa;
        }

        /// <summary>
        /// GERAR DADOS DE LOGON PARA O USUÁRIO CLIENTE
        /// </summary>
        /// <returns></returns>
        public void GerarDadosLogonUsuarioClienteEmpresa(DadosLogin_ClienteEmpresaCliente obj)
        {
            try
            {
                DadosLogin_ClienteEmpresaCliente logonUsuarioClienteEmpresa = _contexto.dadosLoginCliente_empresaCliente.Add(obj);
                _contexto.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// //GRAVAR NOVA USUÁRIO CLIENTE da EMPRESA
        /// </summary>
        /// <returns></returns>
        public Cliente_EmpresaCliente GravarNovoUsuarioClienteEmpresa(Cliente_EmpresaCliente obj)
        {
            try
            {
                Cliente_EmpresaCliente dadosUsuClienteEmpresa = _contexto.cliente_empresaCliente.Add(obj);

                return dadosUsuClienteEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// GERAR DADOS DE LOGON PARA O USUÁRIO
        /// </summary>
        /// <returns></returns>
        public void GerarDadosLogonUsuarioEmpCliente(DadosLogin_UsuarioEmpresaCliente obj)
        {
            try
            {
                DadosLogin_UsuarioEmpresaCliente logonUsuarioEmpresaCliente =  
                    _contexto.dadosLoginUsuario_empresaCliente.Add(obj);
                _contexto.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gravar registro do Usuário Funcionário da Empresa Cliente - SUPERMARKET_ON
        /// </summary>
        /// <returns></returns>
        public Usuario_EmpresaCliente GravarNovoUsuarioEmpresaCliente(Usuario_EmpresaCliente obj)
        {
            try
            {
                Usuario_EmpresaCliente usuarioEmpresaCliente = 
                    _contexto.usuario_empresaCliente.Add(obj);
                _contexto.SaveChanges();

                return usuarioEmpresaCliente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //GRAVAR DADOS ATUALIZADOS do USUÁRIO
        public usuario_empresa AtualizarDadosCadastrais(usuario_empresa obj)
        {
            usuario_empresa dadosDoUsuarioASerAtualizado =
                _contexto.usuario_empresa.FirstOrDefault(m => (m.ID_CODIGO_USUARIO == obj.ID_CODIGO_USUARIO));

            if (dadosDoUsuarioASerAtualizado != null)
            {
                dadosDoUsuarioASerAtualizado.NOME_USUARIO = obj.NOME_USUARIO;

                if ((obj.NICK_NAME_USUARIO != null) && (obj.NICK_NAME_USUARIO != ""))
                {
                    dadosDoUsuarioASerAtualizado.NICK_NAME_USUARIO = obj.NICK_NAME_USUARIO;
                }

                if ((obj.CPF_USUARIO_EMPRESA != null) && (obj.CPF_USUARIO_EMPRESA != ""))
                {
                    dadosDoUsuarioASerAtualizado.CPF_USUARIO_EMPRESA = obj.CPF_USUARIO_EMPRESA;
                }

                if (obj.PAIS_USUARIO_EMPRESA > 0)
                {
                    dadosDoUsuarioASerAtualizado.PAIS_USUARIO_EMPRESA = obj.PAIS_USUARIO_EMPRESA;
                }

                dadosDoUsuarioASerAtualizado.ID_CODIGO_ENDERECO_EMPRESA_USUARIO = obj.ID_CODIGO_ENDERECO_EMPRESA_USUARIO;
                dadosDoUsuarioASerAtualizado.TELEFONE1_USUARIO_EMPRESA = obj.TELEFONE1_USUARIO_EMPRESA;
                dadosDoUsuarioASerAtualizado.TELEFONE2_USUARIO_EMPRESA = obj.TELEFONE2_USUARIO_EMPRESA;
                dadosDoUsuarioASerAtualizado.DATA_ULTIMA_ATUALIZACAO_USUARIO = obj.DATA_ULTIMA_ATUALIZACAO_USUARIO;

                _contexto.SaveChanges();
            }

            return dadosDoUsuarioASerAtualizado;
        }

        //Consultar dados de um Usuário da Empresa
        public usuario_empresa ConsultarDadosDoUsuarioDaEmpresa(int idUsuario)
        {
            //usuario_empresa dadosDoUsuario = _contexto.usuario_empresa.FirstOrDefault(m => (m.ID_CODIGO_EMPRESA.Equals(idUsuario)));
            usuario_empresa dadosDoUsuario = _contexto.usuario_empresa.FirstOrDefault(m => (m.ID_CODIGO_USUARIO.Equals(idUsuario)));

            return dadosDoUsuario;
        }
    }
}
