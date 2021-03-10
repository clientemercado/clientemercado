using ClienteMercado.Data.Entities;
using ClienteMercado.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMercado.Domain.Services
{
    public class NCupomDescontoEmpresaService
    {
        NCupomDescontoEmpresaRepository drepository = new NCupomDescontoEmpresaRepository();

        /// <summary>
        /// GRAVAR NOVA CUPOM da EMPRESA CLIENTE
        /// </summary>
        public CupomDesconto_EmpresaCliente GravarNovaCuponDescontoEmpresa(CupomDesconto_EmpresaCliente obj)
        {
            return drepository.GravarNovaCuponDescontoEmpresa(obj)
        }
    }
}
