using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Domain.Services
{
    public class NLocalidadesAtendidasEmpresaService
    {
        DLocalidadesAtendidasEmpresaRepository drepository = new DLocalidadesAtendidasEmpresaRepository();

        /// <summary>
        /// GRAVAR NOVA LOCALIDADE de ATUAÇÃO da EMPRESA CLIENTE
        /// </summary>  
        public Localidade_CidadeEmpresaCliente GravarNovaLocalidadeAtuacaoEmpresa(Localidade_CidadeEmpresaCliente obj)
        {
            return drepository.GravarNovaLocalidadeAtuacaoEmpresa(obj);
        }

        /// <summary>
        /// CONSULTAR DADOS LOCALIDADE de ATUAÇÃO da EMPRESA CLIENTE
        /// </summary>  
        public Localidade_CidadeEmpresaCliente ConsultarDadosLocalidadeEmpresa(Localidade_CidadeEmpresaCliente obj)
        {
            return drepository.ConsultarDadosLocalidadeEmpresa(obj);
        }

        /// <summary>
        /// ALTERAR DADOS LOCALIDADE de ATUAÇÃO da EMPRESA CLIENTE
        /// </summary>  
        public void AlterarDadosLocalidadeAtuacaoEmpresa(Localidade_CidadeEmpresaCliente obj)
        {
            drepository.AlterarDadosLocalidadeAtuacaoEmpresa(obj);
        }
    }
}
