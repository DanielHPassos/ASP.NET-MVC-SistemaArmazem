using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SistemaArmazem.Models.Negocio
{
    public class EnviarMensagem
    {
        private string meuEmail;
        private string emailPara;
        private string titulo;
        private string msg;
        private string senha = "lfha kull jdue ukvf";
        private SmtpClient client;
        private MailMessage mensagem;

        public EnviarMensagem(string meuEmail, string emailPara, string titulo, string msg, string namefrom)
        {
            this.meuEmail = meuEmail;
            this.emailPara = emailPara;
            this.titulo = titulo;
            this.msg = msg;
            client = new SmtpClient();
            client.UseDefaultCredentials = false;
            mensagem = new MailMessage();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(meuEmail, senha);
            mensagem.Sender = new MailAddress(meuEmail, "Daniel");
            mensagem.From = new MailAddress(meuEmail, namefrom);
            mensagem.To.Add(new MailAddress(emailPara, "daniel"));
            mensagem.Subject = titulo;
            mensagem.Body = msg;
            mensagem.IsBodyHtml = false;
            mensagem.Priority = MailPriority.High;
            mensagem.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            mensagem.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

        }
        public void SubmeterEmail()
        {

            client.Send(mensagem);
        }
    }
}