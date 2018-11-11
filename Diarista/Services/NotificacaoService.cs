using Diarista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Diarista.Services
{
    public class NotificacaoService
    {
        private static EmailConfiguracao Email = new EmailConfiguracao
        {
            From = "x10rodrigo@gmail.com",
            Name = "Rodrigo teste"
        };
        public void NotificarDiarista(Servico servico)
        {
            SmtpClient client = new SmtpClient();
            var body = $"<h2>Olá, {servico.Diarista.Nome}, </h2><p>Você recebeu uma proposta de serviço para trabalhar em: {servico.Casa.Endereco}, acesse o sistema para responder.</p>";
            MailMessage mm = new MailMessage(Email.From, servico.Diarista.Usuario.Email, $"Nova proposta de {servico.Contratante.Nome}", body)
            {
                IsBodyHtml = true
            };
            try
            {
                client.SendAsync(mm, Guid.NewGuid());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public class EmailConfiguracao
        {
            public string From { get; set; }
            public string Name { get; set; }
        }
    }
}