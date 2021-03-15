using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Domain.Services
{
    public class NDepartamentoEmpresaClienteService
    {
        DDepartamentoEmpresaClienteRepository drepository = new DDepartamentoEmpresaClienteRepository();

        /// <summary>
        /// GRAVAR NOVO DEPTO da EMPRESA CLIENTE
        /// </summary>
        public Departamento_EmpresaCliente GravarNovoDeptoEmpresa(Departamento_EmpresaCliente obj)
        {
            return drepository.GravarNovoDeptoEmpresa(obj);
        }

        /// <summary>
        /// CONSULTAR DADOS do DEPTO da EMPRESA CLIENTE
        /// </summary>
        public Departamento_EmpresaCliente ConsultarDadosDeptoEmpresa(Departamento_EmpresaCliente obj)
        {
            return drepository.ConsultarDadosDeptoEmpresa(obj);
        }

        /// <summary>
        /// ALTERAR DADOS do DEPTO da EMPRESA
        /// </summary>
        public void AlterarDadosDeptoEmpresa(Departamento_EmpresaCliente obj)
        {
            drepository.AlterarDadosDeptoEmpresa(obj);
        }

        /// <summary>
        /// CARREGAR LISTA DE DEPARTAMENTOS DA EMPRESA
        /// </summary>
        public List<Departamento_EmpresaCliente> ListaDepartamentosEmpresa()
        {
            return drepository.ListaDepartamentosEmpresa();
        }

        /// <summary>
        /// CARREGAR LISTA DE DEPARTAMENTOS DA EMPRESA - EXIBIÇÃO GRID
        /// </summary>

        public List<ListaDepartamentosEmpresaViewModel> BuscarListaDepartamentosEmpresa()
        {
            return drepository.BuscarListaDepartamentosEmpresa();
        }
    }
}
