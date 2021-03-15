using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using ClienteMercado.Utils.Net;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Infra.Repositories
{
    public class DCupomDescontoEmpresaRepository : RepositoryBase<CupomDesconto_EmpresaCliente>
    {
        int? idEmpresa = Sessao.IdEmpresaUsuario;

        /// <summary>
        /// GRAVAR NOVA CUPOM da EMPRESA CLIENTE
        /// </summary>
        public CupomDesconto_EmpresaCliente GravarNovaCuponDescontoEmpresa(CupomDesconto_EmpresaCliente obj)
        {
            try
            {
                CupomDesconto_EmpresaCliente dadosNovoCupomDescontoEmpresa = _contexto.cuponDesconto_empresaCliente.Add(obj);
                _contexto.SaveChanges();

                return dadosNovoCupomDescontoEmpresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// CONSULTAR DADOS do CUPOM DESCONTOS da EMPRESA CLIENTE
        /// </summary>
        public CupomDesconto_EmpresaCliente ConsultarDadosCupomDescontoEmpresa(CupomDesconto_EmpresaCliente obj)
        {
            CupomDesconto_EmpresaCliente dadosCupomDesconto = 
                _contexto.cuponDesconto_empresaCliente.FirstOrDefault(m => (m.id_CuponDescontoEmpresaCliente == obj.id_CuponDescontoEmpresaCliente));

            return dadosCupomDesconto;
        }

        /// <summary>
        /// ALTERAR DADOS da CUPOM DESCONTOS da EMPRESA CLIENTE
        /// </summary>
        public void AlterarDadosCupomDescontosEmpresa(CupomDesconto_EmpresaCliente obj)
        {
            try
            {
                CupomDesconto_EmpresaCliente dadosCupomDescontoEmpresa = 
                    _contexto.cuponDesconto_empresaCliente.FirstOrDefault(m => ((m.id_CuponDescontoEmpresaCliente == obj.id_CuponDescontoEmpresaCliente) 
                    && (m.id_EmpresaCliente == obj.id_EmpresaCliente)));

                if (dadosCupomDescontoEmpresa != null)
                {
                    dadosCupomDescontoEmpresa.nomeCupom_CupomDescontoEmpresaCliente = obj.nomeCupom_CupomDescontoEmpresaCliente;
                    dadosCupomDescontoEmpresa.dataValidade_CupomDescontoEmpresaCliente = obj.dataValidade_CupomDescontoEmpresaCliente;
                    dadosCupomDescontoEmpresa.percentualDesconto_CupomDescontoEmpresaCliente = obj.percentualDesconto_CupomDescontoEmpresaCliente;

                    _contexto.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// ALTERAR DADOS da CUPOM DESCONTOS da EMPRESA CLIENTE
        /// </summary>
        public List<ListaCuponsDescontoViewModel> BuscarListaDeCuponsDesconto()
        {
            try
            {
                var query = "SELECT CD.id_CuponDescontoEmpresaCliente AS idCuponDescontoEmpresaCliente, CD.nomeCupom_CupomDescontoEmpresaCliente AS nomeCupom, " +
                            "CD.dataCadastroCupon_CupomDescontoEmpresaClientE, CD.dataValidade_CupomDescontoEmpresaCliente, " +
                            "CD.percentualDesconto_CupomDescontoEmpresaCliente, CD.ativoInativo_CupomDescontoEmpresaCliente, " +
                            "CD.idUsuarioCadastrouCupon_CupomDescontoEmpresaCliente, CD.idUsuarioAtivou_CupomDescontoEmpresaCliente " +
                            "FROM CupomDesconto_EmpresaCliente CD " +
                            "WHERE CD.id_EmpresaCliente = " + idEmpresa;
                var listaCuponsDesconto = _contexto.Database.SqlQuery<ListaCuponsDescontoViewModel>(query).ToList();

                return listaCuponsDesconto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}