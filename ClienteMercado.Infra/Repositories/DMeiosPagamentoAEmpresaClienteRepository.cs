using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Infra.Repositories
{
    public class DMeiosPagamentoAEmpresaClienteRepository : RepositoryBase<MeiosPagamento_EmpresaCliente>
    {
        /// <summary>
        /// GRAVAR NOVO MEIO PGTO da EMPRESA CLIENTE
        /// </summary>
        public MeiosPagamento_EmpresaCliente GravarNovoMeioPgtoEmpresa(MeiosPagamento_EmpresaCliente obj)
        {
            try
            {
                MeiosPagamento_EmpresaCliente dadosNovoMeioPgtoEmpresa = _contexto.meiosPagamento_empresaCliente.Add(obj);
                _contexto.SaveChanges();

                return dadosNovoMeioPgtoEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CONSULTAR DADOS MEIO PGTO da EMPRESA CLIENTE
        /// </summary>
        public MeiosPagamento_EmpresaCliente ConsultarDadosMeioPagamento(MeiosPagamento_EmpresaCliente obj)
        {
            try
            {
                MeiosPagamento_EmpresaCliente dadosMeioPgtoEmpresa = 
                    _contexto.meiosPagamento_empresaCliente.FirstOrDefault(m => (m.id_MeiosPagamentoEmpresaCliente == obj.id_MeiosPagamentoEmpresaCliente));

                return dadosMeioPgtoEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ALTERAR DADOS MEIO PGTO da EMPRESA CLIENTE
        /// </summary>
        public void AlterarDadosMeioPgtoEmpresa(MeiosPagamento_EmpresaCliente obj)
        {
            try
            {
                MeiosPagamento_EmpresaCliente dadosMeioPgtoEmpresa =
                    _contexto.meiosPagamento_empresaCliente.FirstOrDefault(m => (m.id_MeiosPagamentoEmpresaCliente == obj.id_MeiosPagamentoEmpresaCliente));

                if (dadosMeioPgtoEmpresa != null)
                {
                    dadosMeioPgtoEmpresa.descricao_MeiosPagamentoEmpresaCliente = obj.descricao_MeiosPagamentoEmpresaCliente;

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
