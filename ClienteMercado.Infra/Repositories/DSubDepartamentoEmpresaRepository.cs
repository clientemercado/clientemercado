using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using ClienteMercado.Utils.Net;
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
