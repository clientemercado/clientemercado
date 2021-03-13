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
    public class DPromocoesEmpresaRepository : RepositoryBase<PromocaoVenda_EmpresaCliente>
    {
        int? idEmpresa = Sessao.IdEmpresaUsuario;

        /// <summary>
        /// GRAVAR NOVA PROMOCAO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>
        public PromocaoVenda_EmpresaCliente GravarNovaPromocaoEmpresa(PromocaoVenda_EmpresaCliente obj)
        {
            try
            {
                PromocaoVenda_EmpresaCliente promocaoVendaEmpresa = 
                    _contexto.promocaoVenda_empresaCliente.Add(obj);
                _contexto.SaveChanges();

                return promocaoVendaEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CONSULTAR DADOS da PROMOCAO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>
        public PromocaoVenda_EmpresaCliente ConsultarDadosPromocaoEmpresa(PromocaoVenda_EmpresaCliente obj)
        {
            try
            {
                PromocaoVenda_EmpresaCliente dadosPromocao = 
                    _contexto.promocaoVenda_empresaCliente.FirstOrDefault(m => ((m.id_PromocaoVendaEmpresaCliente == obj.id_PromocaoVendaEmpresaCliente) 
                    && (m.id_EmpresaCliente == idEmpresa)));

                return dadosPromocao;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CARREGA LISTA de PROMOCOES ATIVAS PRATICADAS pela EMPRESA
        /// </summary>
        /// <returns></returns>
        public List<PromocaoVenda_EmpresaCliente> ListaPromocoesDaEmpresa()
        {
            try
            {
                List<PromocaoVenda_EmpresaCliente> listaPromocoes = 
                    _contexto.promocaoVenda_empresaCliente.Where(m => (m.id_EmpresaCliente == idEmpresa)).ToList();

                return listaPromocoes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ALTERAR DADOS da PROMOCAO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>
        public void AlterarDadosPromocaoEmpresa(PromocaoVenda_EmpresaCliente obj)
        {
            PromocaoVenda_EmpresaCliente dadosPromocaoVendaEmpresa =
                _contexto.promocaoVenda_empresaCliente.FirstOrDefault(m => ((m.id_PromocaoVendaEmpresaCliente == obj.id_PromocaoVendaEmpresaCliente)
                && (m.id_EmpresaCliente == idEmpresa)));

            if (dadosPromocaoVendaEmpresa != null)
            {
                dadosPromocaoVendaEmpresa.nomeOferta_PromocaoVendaEmpresaCliente = obj.nomeOferta_PromocaoVendaEmpresaCliente;
                dadosPromocaoVendaEmpresa.dataValidade_PromocaoVendaEmpresaCliente = obj.dataValidade_PromocaoVendaEmpresaCliente;
                dadosPromocaoVendaEmpresa.percentualOffOferta_PromocaoVendaEmpresaCliente = obj.percentualOffOferta_PromocaoVendaEmpresaCliente;
                dadosPromocaoVendaEmpresa.bannerOferta_PromocaoVendaEmpresaCliente = obj.bannerOferta_PromocaoVendaEmpresaCliente;

                _contexto.SaveChanges();
            }
        }
    }
}
