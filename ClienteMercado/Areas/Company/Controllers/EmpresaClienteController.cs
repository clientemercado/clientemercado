using ClienteMercado.Data.Entities;
using ClienteMercado.Domain.Services;
using ClienteMercado.UI.Core.ViewModel;
using ClienteMercado.Utils.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClienteMercado.Areas.Company.Controllers
{
    public class EmpresaClienteController : Controller
    {
        //
        // GET: /Company/EmpresaCliente/

        public ActionResult Index()
        {
            try
            {
                if (Sessao.IdEmpresaUsuario > 0)
                {
                    /*
                     ALTERAR ESTES DADOS AQUI CONSIDERANDO AS TABELAS DO SUPERMARKET_ON..
                     */

                    NEmpresaUsuarioService negociosEmpresaUsuario = new NEmpresaUsuarioService();
                    NUsuarioEmpresaService negociosUsuarioEmpresa = new NUsuarioEmpresaService();

                    NossasCentraisDeComprasViewModel viewModelCC = new NossasCentraisDeComprasViewModel();

                    empresa_usuario dadosEmpresa = negociosEmpresaUsuario.ConsultarDadosDaEmpresa(new empresa_usuario { ID_CODIGO_EMPRESA = Convert.ToInt32(Sessao.IdEmpresaUsuario) });
                    usuario_empresa dadosUsuarioEmpresa = negociosUsuarioEmpresa.ConsultarDadosDoUsuarioDaEmpresa(Convert.ToInt32(Sessao.IdEmpresaUsuario));

                    return View(viewModelCC);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return null;
        }

    }
}
