using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;

namespace ClienteMercado.Domain.Services
{
    public class NCidadesService
    {
        DCidadesRepository dRepository = new DCidadesRepository();

        //Consultar dados da CIDADE
        public cidades_empresa_usuario ConsultarDadosDaCidade(int idCidade)
        {
            return dRepository.ConsultarDadosDaCidade(idCidade);
        }

        //CARREGA LISTA de CIDADES
        public List<ListaDeCidadesViewModel> CarregarListadeCidades(string term)
        {
            return dRepository.CarregarListadeCidades(term);
        }

        //CONSULTAR DADOS DA CIDADE ONDE A EMPRESA ATUA
        public Cidade_EmpresaCliente ConsultarDadosCidadeEmpresaCliente(Cidade_EmpresaCliente obj)
        {
            return dRepository.ConsultarDadosCidadeEmpresaCliente(obj);
        }

        /// <summary>
        /// GRAVAR NOVA CIDADE de ATUAÇÃO da EMPRESA CLIENTE
        /// </summary>
        public Cidade_EmpresaCliente GravarNovaCidadeAtuacaoEmpresa(Cidade_EmpresaCliente obj)
        {
            return dRepository.GravarNovaCidadeAtuacaoEmpresa(obj);
        }

        /// <summary>
        /// ALTERAR DADOS da CIDADE de ATUAÇÂO da EMPRESA CLIENTE
        /// </summary>
        public void AlterarDadosCidadeEmpresaCliente(Cidade_EmpresaCliente obj)
        {
            dRepository.AlterarDadosCidadeEmpresaCliente(obj);
        }

        /// <summary>
        /// BUSCAR LISTA de CIDADES de ATUAÇÂO da EMPRESA CLIENTE
        /// </summary>
        public List<Cidade_EmpresaCliente> BuscarListaCidadesEmpresa()
        {
            return dRepository.BuscarListaCidadesEmpresa();
        }

        /// <summary>
        /// BUSCAR LISTA de CIDADES de ATUAÇÂO da EMPRESA CLIENTE
        /// </summary>
        public List<ListaCidadesAtuacaoEmpresaViewModel> BuscarListaDeCidadesAtuacaoEmpresa()
        {
            return dRepository.BuscarListaDeCidadesAtuacaoEmpresa();
        }
    }
}
