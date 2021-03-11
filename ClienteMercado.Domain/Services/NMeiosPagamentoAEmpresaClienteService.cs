using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Domain.Services
{
    public class NMeiosPagamentoAEmpresaClienteService
    {
        DMeiosPagamentoAEmpresaClienteRepository drepository = new DMeiosPagamentoAEmpresaClienteRepository();

        /// <summary>
        /// GRAVAR NOVO MEIO PGTO da EMPRESA CLIENTE
        /// </summary>
        public MeiosPagamento_EmpresaCliente GravarNovoMeioPgtoEmpresa(MeiosPagamento_EmpresaCliente obj)
        {
            return drepository.GravarNovoMeioPgtoEmpresa(obj);
        }

        /// <summary>
        /// CONSULTAR DADOS MEIO PGTO da EMPRESA CLIENTE
        /// </summary>
        public MeiosPagamento_EmpresaCliente ConsultarDadosMeioPagamento(MeiosPagamento_EmpresaCliente obj)
        {
            return drepository.ConsultarDadosMeioPagamento(obj);
        }

        /// <summary>
        /// ALTERAR DADOS MEIO PGTO da EMPRESA CLIENTE
        /// </summary>
        public void AlterarDadosMeioPgtoEmpresa(MeiosPagamento_EmpresaCliente obj)
        {
            drepository.AlterarDadosMeioPgtoEmpresa(obj);
        }
    }
}
