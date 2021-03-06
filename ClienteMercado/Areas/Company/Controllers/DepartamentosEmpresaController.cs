using ClienteMercado.Data.Entities;
using ClienteMercado.Domain.Services;
using ClienteMercado.UI.Core.ViewModel;
using ClienteMercado.Utils.Net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClienteMercado.Areas.Company.Controllers
{
    public class DepartamentosEmpresaController : Controller
    {
        //
        // GET: /Company/DepartamentosEmpresa/

        public ActionResult Index()
        {
            try
            {
                if (Session["IdUsuarioLogado"] != null)
                {
                    DateTime dataHoje = DateTime.Today;

                    //Montando o dia por extenso a se exibido no site (* Não se usa mais isso)
                    string diaDaSemana = new CultureInfo("pt-BR").DateTimeFormat.GetDayName(dataHoje.DayOfWeek);
                    diaDaSemana = char.ToUpper(diaDaSemana[0]) + diaDaSemana.Substring(1);

                    int diaDoMes = dataHoje.Day;
                    string mesAtual = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(dataHoje.Month);
                    mesAtual = char.ToUpper(mesAtual[0]) + mesAtual.Substring(1);
                    int anoAtual = dataHoje.Year;

                    //----------------------------------------------------------------------------------------------------------------
                    //TRECHO ESSENCIAL PRA EIBIÇÃO DOS DADOS DA EMPRESA CLIENTE E USUÁRIO LOGADOS
                    NEmpresaUsuarioService serviceEmpresaCliente = new NEmpresaUsuarioService();
                    NUsuarioEmpresaService serviceUsuEmpresaCliente = new NUsuarioEmpresaService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaCliente =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaCliente =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeFantasia_EmpresaCliente = dadosEmpresaCliente.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nome_UsuarioEmpresaCliente = dadosUsuEmpresaCliente.nome_UsuarioEmpresaCliente;
                    //----------------------------------------------------------------------------------------------------------------

                    //VIEWBAGS
                    ViewBag.dataHoje = diaDaSemana + ", " + diaDoMes + " de " + mesAtual + " de " + anoAtual;
                    ViewBag.ondeEstouAgora = "";

                    return View(dadosDaEmpresaClienteEUsuario);
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { area = "" });
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

    }
}
