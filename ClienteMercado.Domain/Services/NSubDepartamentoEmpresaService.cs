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

        /// <summary>
        /// ATUALIZAR DADOS do SUB-DEPTO da EMPRESA CLIENTE
        /// </summary>
        public void AlterarDadosSubDeptoEmpresa(SubDepartamento_EmpresaCliente obj)
        {
            drepository.AlterarDadosSubDeptoEmpresa(obj);
        }

        /// <summary>
        /// CARREGAR LISTA de SUB-DEPTO da EMPRESA CLIENTE
        /// </summary>
        public List<SubDepartamento_EmpresaCliente> ListaSubDepartamentosEmpresa()
        {
            return drepository.ListaSubDepartamentosEmpresa();
        }
    }
}
