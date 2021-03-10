using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Infra.Repositories
{
    public class NCupomDescontoEmpresaRepository : RepositoryBase<CupomDesconto_EmpresaCliente>
    {
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
        }
    }
}
