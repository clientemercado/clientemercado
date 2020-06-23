﻿using System;
using System.Web;
using System.Net.Mail;
using System.Net.Mime;

namespace ClienteMercado.Utils.Mail
{
    public class EnviarEmailSolicitandoConfirmacaoDaCotacao
    {
        public bool EnviandoEmail(string _empresa, string _usuario, string _email1_Empresa, string _email2_Empresa, string _email1_UsuarioContato, string _email2_UsuarioContato,
            string _nomeCentralDeCompras, string dataLimiteAnexarCotacao)
        {
            //Montando Link de Acesso ao site
            string comandoHref = "<a href=";
            string link1 = comandoHref + "http://www.clientemercado.com.br/";
            string link2 = "></a>";

            //Montando o corpo do e-mail
            string linked = (link1 + link2);
            string assunto = "";
            string mensagem = "";

            assunto = "CONFERIR RESPOSTA COTAÇÃO no CLIENTEMERCADO.COM - PARA PEDIDO";
            mensagem = @"<html>" +
                        "<body><tr><td><table width='80%' align='center' bgcolor='#E8E8E8'><tr><td><img src='cid:Imagem1' /></td>" +
                        "<tr><td align='center'><h2><b>!!! ATENÇÃO !!!</b></h2></td></tr><tr><td><br>&nbsp;&nbsp;Sr(a) " + _usuario + ", <br><br>" +
                        "&nbsp;&nbsp;Sua Central de Compras <b>" + _nomeCentralDeCompras + "</b> na plataforma ClienteMercado.com, teve a COTAÇÃO RESPONDIDA pelos fornecedores.</td></tr>" +
                        "<tr><td align='center'>Acesse o site e confira os preços respondidos.</td></tr>" +
                        "<tr><td align='center'>Após a conferir, clique em CONFIRMAR, para garantir o preço negociado.</td></tr>" +
                        "<tr><td></td></tr>" +
                        "<tr><td align='center'><br>Não perca o prazo!<br><br>" +
                        "Acesse o site " + linked + " .</td></tr>" +
                        "<tr><td><br><br>&nbsp;&nbsp;Atenciosamente,<br></td></tr><tr><td><br>&nbsp;&nbsp;" +
                        "Equipe ClienteMercado<br></td></tr>" +
                        "<tr><td>&nbsp;</td></tr>" +
                        "<tr><td>&nbsp;</td></tr>" +
                        "</table></td></tr>" +
                        "</body></html>";

            //ENVIA o E-MAIL
            try
            {
                MailMessage mail = new MailMessage();

                if (!string.IsNullOrEmpty(_email1_Empresa))
                {
                    mail.To.Add(_email1_Empresa);
                }

                if (!string.IsNullOrEmpty(_email2_Empresa))
                {
                    MailAddress copy = new MailAddress(_email2_Empresa);
                    mail.CC.Add(copy);
                }

                if (!string.IsNullOrEmpty(_email1_UsuarioContato))
                {
                    MailAddress copy = new MailAddress(_email1_UsuarioContato);
                    mail.CC.Add(copy);
                }

                if (!string.IsNullOrEmpty(_email2_UsuarioContato))
                {
                    MailAddress copy = new MailAddress(_email2_UsuarioContato);
                    mail.CC.Add(copy);
                }

                mail.From = new MailAddress("contato@clientemercado.com.br");
                mail.Subject = assunto;
                string Body = mensagem;

                //--
                AlternateView view = AlternateView.CreateAlternateViewFromString(Body, null, MediaTypeNames.Text.Html);

                //Header do e-mail - Obs: É o mesmo para todos
                string caminhoImagem1 = HttpContext.Current.Server.MapPath("~/Content/images/header-email_clientemercado.jpg");
                LinkedResource resource = new LinkedResource(caminhoImagem1);
                resource.ContentId = "Imagem1";
                view.LinkedResources.Add(resource);

                mail.AlternateViews.Add(view);
                //--

                mail.Body = Body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "mail.clientemercado.com.br";
                //smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("administrador@clientemercado.com.br", "24689012ed");
                smtp.EnableSsl = false;
                //smtp.Send(mail);      <== DESCOMENTAR ANTES DE IR AO AR...

                return true;
            }
            catch (Exception erro)
            {
                throw erro;
            }

        }
    }
}
