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
    public class DDepartamentoEmpresaClienteRepository : RepositoryBase<Departamento_EmpresaCliente>
    {
        int? idEmpresa = Sessao.IdEmpresaUsuario;

        /// <summary>
        /// GRAVAR NOVO DEPTO da EMPRESA CLIENTE
        /// </summary>
        public Departamento_EmpresaCliente GravarNovoDeptoEmpresa(Departamento_EmpresaCliente obj)
        {
            try
            {
                Departamento_EmpresaCliente dadosNovoDepartamentoEmpresa = _contexto.departamenento_empresaCliente.Add(obj);
                _contexto.SaveChanges();

                return dadosNovoDepartamentoEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CONSULTAR DADOS do DEPTO da EMPRESA CLIENTE
        /// </summary>
        public Departamento_EmpresaCliente ConsultarDadosDeptoEmpresa(Departamento_EmpresaCliente obj)
        {
            try
            {
                Departamento_EmpresaCliente dadosDeptoEmpresa = 
                    _contexto.departamenento_empresaCliente.FirstOrDefault(m => (m.id_DepartamentoEmpresaCliente == obj.id_DepartamentoEmpresaCliente));

                return dadosDeptoEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CARREGAR LISTA DE DEPARTAMENTOS DA EMPRESA - EXIBIÇÃO GRID
        /// </summary>
        public List<ListaDepartamentosEmpresaViewModel> BuscarListaDepartamentosEmpresa()
        {
            try
            {
                var query = "SELECT DEC.id_DepartamentoEmpresaCliente, " +
                            "DEC.descricao_DepartamentoEmpresaCliente AS nomeDepartamentoEmpresa, DEC.ativoInativo_DepartamentoEmpresaCliente " +
                            "FROM Departamento_EmpresaCliente DEC " +
                            "WHERE DEC.id_EmpresaCliente = " + idEmpresa;
                var listaCuponsDesconto = _contexto.Database.SqlQuery<ListaDepartamentosEmpresaViewModel>(query).ToList();

                return listaCuponsDesconto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CARREGAR LISTA DE DEPARTAMENTOS DA EMPRESA
        /// </summary>
        public List<Departamento_EmpresaCliente> ListaDepartamentosEmpresa()
        {
            try
            {
                List<Departamento_EmpresaCliente> listaDeptoEmpresa =
                    _contexto.departamenento_empresaCliente.Where(m => (m.id_EmpresaCliente == idEmpresa)).ToList();

                return listaDeptoEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ALTERAR DADOS do DEPTO da EMPRESA
        /// </summary>
        public void AlterarDadosDeptoEmpresa(Departamento_EmpresaCliente obj)
        {
            try
            {
                Departamento_EmpresaCliente dadosDeptoEmpresa =
                    _contexto.departamenento_empresaCliente.FirstOrDefault(m => ((m.id_DepartamentoEmpresaCliente == obj.id_DepartamentoEmpresaCliente)
                    && (m.id_EmpresaCliente == obj.id_EmpresaCliente)));

                if (dadosDeptoEmpresa != null)
                {
                    dadosDeptoEmpresa.descricao_DepartamentoEmpresaCliente = obj.descricao_DepartamentoEmpresaCliente;
                    //dadosDeptoEmpresa.ativoInativo_DepartamentoEmpresaCliente = obj.ativoInativo_DepartamentoEmpresaCliente;

                    _contexto.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}
