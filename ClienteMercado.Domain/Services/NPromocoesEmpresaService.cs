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
    public class NPromocoesEmpresaService
    {
        DPromocoesEmpresaRepository drepository = new DPromocoesEmpresaRepository();

        /// <summary>
        /// GRAVAR NOVA PROMOCAO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>
        public PromocaoVenda_EmpresaCliente GravarNovaPromocaoEmpresa(PromocaoVenda_EmpresaCliente obj)
        {
            return drepository.GravarNovaPromocaoEmpresa(obj);
        }

        /// <summary>
        /// CONSULTAR DADOS da PROMOCAO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>
        public PromocaoVenda_EmpresaCliente ConsultarDadosPromocaoEmpresa(PromocaoVenda_EmpresaCliente obj)
        {
            return drepository.ConsultarDadosPromocaoEmpresa(obj);
        }

        /// <summary>
        /// ALTERAR DADOS da PROMOCAO da EMPRESA CLIENTE
        /// </summary>
        /// <returns></returns>
        public void AlterarDadosPromocaoEmpresa(PromocaoVenda_EmpresaCliente obj)
        {
            drepository.AlterarDadosPromocaoEmpresa(obj);
        }

        /// <summary>
        /// CARREGA LISTA de PROMOCOES ATIVAS PRATICADAS pela EMPRESA
        /// </summary>
        /// <returns></returns>
        public List<PromocaoVenda_EmpresaCliente> ListaPromocoesDaEmpresa()
        {
            return drepository.ListaPromocoesDaEmpresa();
        }

        /// <summary>
        /// CARREGA LISTA de PROMOCOES ATIVAS PRATICADAS pela EMPRESA
        /// </summary>
        /// <returns></returns>
        public List<ListaPromocoesEmpresaViewModel> BuscarListaDePromocoesDaEmpresa()
        {
            return drepository.BuscarListaDePromocoesDaEmpresa();
        }
    }
}
