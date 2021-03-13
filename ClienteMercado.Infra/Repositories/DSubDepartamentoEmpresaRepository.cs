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
