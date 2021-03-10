using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Domain.Services
{
    public class NSubDepartamentoEmpresaService
    {
        DSubDepartamentoEmpresaRepository drepository = new DSubDepartamentoEmpresaRepository();

        /// <summary>
        /// CARREGAR LISTA DE DEPARTAMENTOS DA EMPRESA
        /// </summary>
        public List<Departamento_EmpresaCliente> ListaDepartamentosEmpresa()
        {
            return drepository.ListaDepartamentosEmpresa();
        }

        /// <summary>
        /// GRAVAR NOVO SUB-DEPTO da EMPRESA CLIENTE
        /// </summary>
        public SubDepartamento_EmpresaCliente GravarNovoSubDeptoEmpresa(SubDepartamento_EmpresaCliente obj)
        {
            return drepository.GravarNovoSubDeptoEmpresa(obj);
        }

        /// <summary>
        /// CONSULTAR DADOS SUB-DEPTO da EMPRESA CLIENTE
        /// </summary>
        public SubDepartamento_EmpresaCliente ConsultarDadosSubDeptoEmpresa(SubDepartamento_EmpresaCliente obj)
        {
            return drepository.ConsultarDadosSubDeptoEmpresa(obj);
        }
    }
}
