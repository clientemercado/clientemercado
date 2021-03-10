using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using ClienteMercado.Utils.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace ClienteMercado.Infra.Repositories
{
    public class DCidadesRepository : RepositoryBase<cidades_empresa_usuario>
    {
        //Consultar dados da CIDADE
        public cidades_empresa_usuario ConsultarDadosDaCidade(int idCidade)
        {
            cidades_empresa_usuario dadosDaCidade = _contexto.cidades_empresa_usuario.FirstOrDefault(m => (m.ID_CIDADE_EMPRESA_USUARIO.Equals(idCidade)));

            return dadosDaCidade;
        }

        //CARREGA LISTA de CIDADES
        public List<ListaDeCidadesViewModel> CarregarListadeCidades(string term)
        {
            var query = "SELECT C.* FROM cidades_empresa_usuario C WHERE C.CIDADE_EMPRESA_USUARIO LIKE '%" + term + "%'";

            var result = _contexto.Database.SqlQuery<ListaDeCidadesViewModel>(query).ToList();
            return result;
        }

        /// <summary>
        /// CONSULTAR DADOS DA CIDADE ONDE A EMPRESA ATUA
        /// </summary>
        public Cidade_EmpresaCliente ConsultarDadosCidadeEmpresaCliente(Cidade_EmpresaCliente obj)
        {
            try
            {
                Cidade_EmpresaCliente dadosCidadeEmpresa =
                    _contexto.cidade_empresaCliente.FirstOrDefault(m => (m.id_CidadeEmpresaCliente == obj.id_CidadeEmpresaCliente));

                return dadosCidadeEmpresa;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ALTERAR DADOS da CIDADE de ATUAÇÂO da EMPRESA CLIENTE
        /// </summary>
        public void AlterarDadosCidadeEmpresaCliente(Cidade_EmpresaCliente obj)
        {
            try
            {
                Cidade_EmpresaCliente dadosCidadeEmpClienteAlterar = 
                    _contexto.cidade_empresaCliente.FirstOrDefault(m => ((m.id_CidadeEmpresaCliente == obj.id_CidadeEmpresaCliente) 
                    && (m.id_EmpresaCliente == obj.id_EmpresaCliente)));

                if (dadosCidadeEmpClienteAlterar != null)
                {
                    dadosCidadeEmpClienteAlterar.cidade_CidadeEmpresaCliente= obj.cidade_CidadeEmpresaCliente;
                    dadosCidadeEmpClienteAlterar.uf_CidadeEmpresaCliente = obj.uf_CidadeEmpresaCliente;
                    dadosCidadeEmpClienteAlterar.pais_CidadeEmpresaCliente = obj.pais_CidadeEmpresaCliente;

                    _contexto.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// GRAVAR NOVA CIDADE de ATUAÇÃO da EMPRESA CLIENTE
        /// </summary>
        public Cidade_EmpresaCliente GravarNovaCidadeAtuacaoEmpresa(Cidade_EmpresaCliente obj)
        {
            try
            {
                Cidade_EmpresaCliente dadosCidadeEmpresaCliente = _contexto.cidade_empresaCliente.Add(obj);
                _contexto.SaveChanges();

                return dadosCidadeEmpresaCliente;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}
