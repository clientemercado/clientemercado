using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using ClienteMercado.Utils.Net;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Infra.Repositories
{
    public class DSubDepartamentoEmpresaRepository : RepositoryBase<SubDepartamento_EmpresaCliente>
    {
        int? idEmpresa = Sessao.IdEmpresaUsuario;

        /// <summary>
        /// GRAVAR NOVO SUB-DEPTO da EMPRESA CLIENTE
        /// </summary>
        public SubDepartamento_EmpresaCliente GravarNovoSubDeptoEmpresa(SubDepartamento_EmpresaCliente obj)
        {
            try
            {
                SubDepartamento_EmpresaCliente dadosNovoSubDepartamentoEmpresa = 
                    _contexto.subDepartamento_empresaCliente.Add(obj);
                _contexto.SaveChanges();

                return dadosNovoSubDepartamentoEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ATUALIZAR DADOS do SUB-DEPTO da EMPRESA CLIENTE
        /// </summary>
        public void AlterarDadosSubDeptoEmpresa(SubDepartamento_EmpresaCliente obj)
        {
            try
            {
                SubDepartamento_EmpresaCliente dadosSubDeptoEmpresa =
                    _contexto.subDepartamento_empresaCliente.FirstOrDefault(m => ((m.id_SubDepartamentoEmpresaCliente == obj.id_SubDepartamentoEmpresaCliente)));

                if (dadosSubDeptoEmpresa != null)
                {
                    dadosSubDeptoEmpresa.id_DepartamentoEmpresaCliente = obj.id_DepartamentoEmpresaCliente;
                    dadosSubDeptoEmpresa.descricao_SubDepartamentoEmpresaCliente = obj.descricao_SubDepartamentoEmpresaCliente;

                    _contexto.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// BUSCAR ID DO ÚLTIMO DEPTO REGISTRADO PARA O SUB-DEPTO
        /// </summary>
        public int? ConsultarIdUltimoDeptoRegistradoNosSubDeptos()
        {
            try
            {
                var query = "SELECT TOP 1 SD.* " +
                            "FROM SubDepartamento_EmpresaCliente SD " +
                            "ORDER BY SD.id_DepartamentoEmpresaCliente DESC";
                List<SubDepartamento_EmpresaCliente> listaSubDeptos = _contexto.Database.SqlQuery<SubDepartamento_EmpresaCliente>(query).ToList();

                return listaSubDeptos[0].id_DepartamentoEmpresaCliente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CARREGAR LISTA de SUB-DEPTO da EMPRESA CLIENTE - GRID
        /// </summary>
        public List<ListaSubDeptosEmpresaViewModel> BuscarListaSubDepartamentosEmpresa()
        {
            try
            {
                var query = "SELECT SD.id_SubDepartamentoEmpresaCliente, SD.id_DepartamentoEmpresaCliente, " +
                            "SD.descricao_SubDepartamentoEmpresaCliente AS nomeSubDepartamentoEmpresa, SD.ativoInativo_SubDepartamentoEmpresaCliente " +
                            "FROM SubDepartamento_EmpresaCliente SD";
                var listaSubDeptos = _contexto.Database.SqlQuery<ListaSubDeptosEmpresaViewModel>(query).ToList();

                return listaSubDeptos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CARREGAR LISTA de SUB-DEPTO da EMPRESA CLIENTE
        /// </summary>
        public List<SubDepartamento_EmpresaCliente> ListaSubDepartamentosEmpresa()
        {
            try
            {
                List<SubDepartamento_EmpresaCliente> listaSubDeptos = 
                    _contexto.subDepartamento_empresaCliente.Where(m => (m.id_SubDepartamentoEmpresaCliente > 0)).ToList();

                return listaSubDeptos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CONSULTAR DADOS SUB-DEPTO da EMPRESA CLIENTE
        /// </summary>
        public SubDepartamento_EmpresaCliente ConsultarDadosSubDeptoEmpresa(SubDepartamento_EmpresaCliente obj)
        {
            try
            {
                SubDepartamento_EmpresaCliente dadosSubDeptoEmpresa =
                    _contexto.subDepartamento_empresaCliente.FirstOrDefault(m => (m.id_SubDepartamentoEmpresaCliente == obj.id_SubDepartamentoEmpresaCliente));

                return dadosSubDeptoEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
