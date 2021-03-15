﻿using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Infra.Repositories
{
    public class DLocalidadesAtendidasEmpresaRepository : RepositoryBase<Localidade_CidadeEmpresaCliente>
    {
        /// <summary>
        /// GRAVAR NOVA LOCALIDADE de ATUAÇÃO da EMPRESA CLIENTE
        /// </summary>  
        public Localidade_CidadeEmpresaCliente GravarNovaLocalidadeAtuacaoEmpresa(Localidade_CidadeEmpresaCliente obj)
        {
            try
            {
                Localidade_CidadeEmpresaCliente dadosLocalidadeEmpresa = _contexto.localidadeCidade_empresaCliente.Add(obj);
                _contexto.SaveChanges();

                return dadosLocalidadeEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CONSULTAR DADOS LOCALIDADE de ATUAÇÃO da EMPRESA CLIENTE
        /// </summary>  
        public Localidade_CidadeEmpresaCliente ConsultarDadosLocalidadeEmpresa(Localidade_CidadeEmpresaCliente obj)
        {
            try
            {
                Localidade_CidadeEmpresaCliente dadosLocalidadeEmpresa = 
                    _contexto.localidadeCidade_empresaCliente.FirstOrDefault(m => (m.id_LocalidadeCidadeEmpresaCliente == obj.id_LocalidadeCidadeEmpresaCliente));

                return dadosLocalidadeEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CARREGAR LISTA de LOCALIDADES de ATUAÇÃO da EMPRESA -  GRID
        /// </summary>  
        public List<ListaLocalidadesAtendidasViewModel> BuscarListaDeLocalidadesEmpresa()
        {
            try
            {
                var query = "SELECT LA.id_LocalidadeCidadeEmpresaCliente, LA.id_CidadeEmpresaCliente, " + "" +
                            "LA.nomeLocalidade_LocalidadeCidadeEmpresaCliente AS nomeLocalidade, LA.cepLocalidade_LocalidadeCidadeEmpresaCliente AS cepLocalidade " +
                            "FROM Localidade_CidadeEmpresaCliente LA";
                var listaLocalidades = _contexto.Database.SqlQuery<ListaLocalidadesAtendidasViewModel>(query).ToList();

                return listaLocalidades;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ALTERAR DADOS LOCALIDADE de ATUAÇÃO da EMPRESA CLIENTE
        /// </summary>  
        public void AlterarDadosLocalidadeAtuacaoEmpresa(Localidade_CidadeEmpresaCliente obj)
        {
            try
            {
                Localidade_CidadeEmpresaCliente dadosCidadeEmpClienteAlterar =
                    _contexto.localidadeCidade_empresaCliente.FirstOrDefault(m => (m.id_LocalidadeCidadeEmpresaCliente == obj.id_LocalidadeCidadeEmpresaCliente));

                if (dadosCidadeEmpClienteAlterar != null)
                {
                    dadosCidadeEmpClienteAlterar.id_CidadeEmpresaCliente = obj.id_CidadeEmpresaCliente;
                    dadosCidadeEmpClienteAlterar.nomeLocalidade_LocalidadeCidadeEmpresaCliente = obj.nomeLocalidade_LocalidadeCidadeEmpresaCliente;
                    dadosCidadeEmpClienteAlterar.cepLocalidade_LocalidadeCidadeEmpresaCliente = obj.cepLocalidade_LocalidadeCidadeEmpresaCliente;

                    _contexto.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
