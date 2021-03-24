using ClienteMercado.Data.Entities;
using ClienteMercado.Domain.Services;
using ClienteMercado.UI.Core.ViewModel;
using ClienteMercado.Utils.Net;
using ClienteMercado.Utils.Utilitarios;
using ClienteMercado.Utils.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
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

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;
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

        public ActionResult Cadastro()
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

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;
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

        //----------------------------------------------------------------------------------
        //GRAVAR REGISTRO
        public JsonResult GravarRegistro(DadosEmpresaClienteViewModel obj)
        {
            try
            {
                NDepartamentoEmpresaClienteService serviceDepartamentoEmpresa = new NDepartamentoEmpresaClienteService();
                Departamento_EmpresaCliente dadosNewDeptoEmpresa = new Departamento_EmpresaCliente();

                dadosNewDeptoEmpresa.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosNewDeptoEmpresa.descricao_DepartamentoEmpresaCliente = obj.descricao_DepartamentoEmpresaCliente;
                dadosNewDeptoEmpresa.ativoInativo_DepartamentoEmpresaCliente = true;

                //GRAVAR NOVO DEPTO da EMPRESA CLIENTE
                dadosNewDeptoEmpresa = serviceDepartamentoEmpresa.GravarNovoDeptoEmpresa(dadosNewDeptoEmpresa);

                return Json(new { status = "ok", idRegistroGerado = dadosNewDeptoEmpresa.id_DepartamentoEmpresaCliente }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        public ActionResult AlterarDados(int id)
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
                    NDepartamentoEmpresaClienteService serviceDepartamentoEmpresa = new NDepartamentoEmpresaClienteService();
                    DadosEmpresaClienteViewModel dadosDaEmpresaClienteEUsuario = new DadosEmpresaClienteViewModel();

                    EmpresaCliente dadosEmpresaLogada =
                        serviceEmpresaCliente.ConsultarDadosDaEmpresaCliente(new EmpresaCliente { id_EmpresaCliente = Convert.ToInt32(Session["IdEmpresaUsuario"]) });
                    Usuario_EmpresaCliente dadosUsuEmpresaLogada =
                        serviceUsuEmpresaCliente.ConsultarDadosUsuarioEmpresaCliente(new Usuario_EmpresaCliente { id_UsuarioEmpresaCliente = Convert.ToInt32(Session["IdUsuarioLogado"]) });
                    Departamento_EmpresaCliente dadosDeptoEmpresa = 
                        serviceDepartamentoEmpresa.ConsultarDadosDeptoEmpresa(new Departamento_EmpresaCliente { id_DepartamentoEmpresaCliente = Convert.ToInt32(id) });

                    //POPULAR VIEW MODEL
                    dadosDaEmpresaClienteEUsuario.nomeEmpresaLogada = dadosEmpresaLogada.nomeFantasia_EmpresaCliente.ToUpper();
                    dadosDaEmpresaClienteEUsuario.nomeUsuarioEmpresaLogada = dadosUsuEmpresaLogada.nome_UsuarioEmpresaCliente;

                    dadosDaEmpresaClienteEUsuario.iDEC = dadosDeptoEmpresa.id_DepartamentoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.descricao_DepartamentoEmpresaCliente = dadosDeptoEmpresa.descricao_DepartamentoEmpresaCliente;
                    dadosDaEmpresaClienteEUsuario.ativoInativo_DepartamentoEmpresaCliente = 
                        dadosDeptoEmpresa.ativoInativo_DepartamentoEmpresaCliente.ToString();
                    dadosDaEmpresaClienteEUsuario.imagem_DepartamentoEmpresaCliente = dadosDeptoEmpresa.imagem_DepartamentoEmpresaCliente;
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

        //----------------------------------------------------------------------------------
        //ATUALIZAR REGISTRO
        public JsonResult AtualizarRegistro(DadosEmpresaClienteViewModel obj)
        {
            try
            {
                NDepartamentoEmpresaClienteService serviceDepartamentoEmpresa = new NDepartamentoEmpresaClienteService();
                Departamento_EmpresaCliente dadosDeptoEmpresaAlterar = new Departamento_EmpresaCliente();

                dadosDeptoEmpresaAlterar.id_EmpresaCliente = Convert.ToInt32(Sessao.IdEmpresaUsuario);
                dadosDeptoEmpresaAlterar.id_DepartamentoEmpresaCliente = obj.iDEC;
                dadosDeptoEmpresaAlterar.descricao_DepartamentoEmpresaCliente = obj.descricao_DepartamentoEmpresaCliente;
                //dadosDeptoEmpresaAlterar.ativoInativo_DepartamentoEmpresaCliente = true;

                //ALTERAR DADOS do DEPTO da EMPRESA
                dadosDeptoEmpresaAlterar = serviceDepartamentoEmpresa.AlterarDadosDeptoEmpresa(dadosDeptoEmpresaAlterar);

                return Json(new { status = "ok", idRegistroAtualizado = dadosDeptoEmpresaAlterar.id_DepartamentoEmpresaCliente }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        //----------------------------------------------------------------------------------

        public ActionResult BuscarListaDepartamentosEmpresa(string descricaoFiltro)
        {
            try
            {
                NDepartamentoEmpresaClienteService serviceDepartamentoEmpresa = new NDepartamentoEmpresaClienteService();

                List<ListaDepartamentosEmpresaViewModel> listaDeptosEmpresa = serviceDepartamentoEmpresa.BuscarListaDepartamentosEmpresa();

                if (String.IsNullOrEmpty(descricaoFiltro) == false)
                {
                    listaDeptosEmpresa = listaDeptosEmpresa.Where(m => (m.nomeDepartamentoEmpresa.ToUpper().Contains(descricaoFiltro.ToUpper()))).ToList();
                }

                for (int i = 0; i < listaDeptosEmpresa.Count; i++)
                {
                    listaDeptosEmpresa[i].idDepartamentoEmpresa = listaDeptosEmpresa[i].id_DepartamentoEmpresaCliente.ToString();
                    listaDeptosEmpresa[i].ativoInativoDeptoEmpresa = listaDeptosEmpresa[i].ativoInativo_DepartamentoEmpresaCliente ? "Sim" : "Não";
                }

                return Json(
                    new
                    {
                        rows = listaDeptosEmpresa,
                        current = 1,
                        rowCount = listaDeptosEmpresa.Count,
                        total = listaDeptosEmpresa.Count,
                        dadosCarregados = "Ok"
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //========================================================================
        //GRAVAR IMAGEM LOGOMARCA da EQUIPE de TRABALHO
        public ActionResult GravarImagemLogoMarcaDepto(int idDepto, string imagem, string nome, string extensao)
        {
            string[] extensoesValidas = new string[] { "image/jpg", "image/jpeg", "image/png", "image/JPG", "image/JPEG", "image/PNG" };

            var status = new { status = "ok" };

            //Se foi carregada alguma foto
            if (nome != null)
            {
                if (!extensoesValidas.Contains(extensao))
                {
                    //IMAGEM de upload NÃO É VÁLIDA
                    status = new { status = "Nok" };
                }
                else
                {
                    string caminhoImagemPh = AppDomain.CurrentDomain.BaseDirectory.ToString(); //Pega o caminho físico do PROJETO, para ser usado na montagem do caminho real de armaz.

                    //Montando o caminho de armazenamento a ser confirmada existência
                    caminhoImagemPh = (caminhoImagemPh + "Content/imagensDeptos/");

                    //Verifica se a estrutura de pastas de armazenamento está criada. Se não existir, cria imediatamente
                    DirectoryInfo dirEmpresa = new DirectoryInfo(caminhoImagemPh);

                    //Cria as PASTAS
                    if (dirEmpresa.Exists == false)
                    {
                        //Criando a pasta para armazenar as imagens
                        dirEmpresa.Create();
                    }

                    //Pesquisando dados do DEPARTAMENTO

                    NDepartamentoEmpresaClienteService serviceDeptos = new NDepartamentoEmpresaClienteService();
                    Departamento_EmpresaCliente dadosDepto = 
                        serviceDeptos.ConsultarDadosDeptoEmpresa(new Departamento_EmpresaCliente { id_DepartamentoEmpresaCliente =  idDepto});
                    string logomarca = dadosDepto.imagem_DepartamentoEmpresaCliente;

                    //Exclui a imagem se já existir
                    if (!string.IsNullOrEmpty(logomarca))
                    {
                        string caminhoImagem = (caminhoImagemPh + logomarca);

                        //Verifica se a imagem existe na pasta
                        DirectoryInfo dirImagem = new DirectoryInfo(caminhoImagem);

                        //Verificando se existe a imagem
                        if (dirEmpresa.Exists == true)
                        {
                            //Exclui a imagem
                            FileInfo arquivoADeletar = new FileInfo(Server.MapPath("~/Content/imagensDeptos/" + logomarca));
                            arquivoADeletar.Delete();
                        }
                    }

                    //Processo de gravação da nova Imagem
                    //string novoNomeDoArquivo = CodificarNomeDeArquivo.renomearComHash(nome);
                    string novoNomeDoArquivo = nome;
                    //novoNomeDoArquivo = codEquipe.ToString() + "_" + novoNomeDoArquivo;
                    string convert = imagem.Replace("data:" + extensao + ";base64,", String.Empty);

                    byte[] image64 = Convert.FromBase64String(convert);
                    string savedFileName = Path.Combine(Server.MapPath("~/Content/imagensDeptos/"), Path.GetFileName(novoNomeDoArquivo));
                    using (var ms = new MemoryStream(image64))
                    {
                        Image img = Image.FromStream(ms);
                        //System.Drawing.Image img = image.GetThumbnailImage(500, 500, null, IntPtr.Zero);
                        img.Save(savedFileName, ImageFormat.Jpeg);
                    }

                    //Atualiza o nome da Logomarca no banco
                    dadosDepto.imagem_DepartamentoEmpresaCliente = novoNomeDoArquivo;
                    serviceDeptos.AlterarDadosDeptoEmpresa(dadosDepto);
                }
            }

            return Json(status, "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        //========================================================================
    }
}
