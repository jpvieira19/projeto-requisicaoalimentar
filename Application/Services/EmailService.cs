using System;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using Application.DTO;
using System.Globalization;
using MimeKit.Utils;

namespace Application.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly string _fromEmail;
        private readonly string _logoPath;


        public EmailService()
        {
            _smtpServer = "smtp.office365.com"; // Servidor SMTP do Outlook
            _smtpPort = 587; // Porta do servidor SMTP
            _smtpUser = "servico.imunohemoterapia.ulsge@outlook.pt";
            _smtpPass = "Sih!B@ncoSangue"; // Substitua pela senha do seu e-mail
            _fromEmail = "servico.imunohemoterapia.ulsge@outlook.pt"; // Substitua pelo seu e-mail
            _logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "images", "logo1.jpg");
        }

        public async Task<bool> SendEmailWithPdfAsync(PedidosDTO pedido, byte[] pdfBytes)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Serviço Imunohemoterapia | Banco de Sangue", _fromEmail));

            message.To.AddRange(new[]
            {
                new MailboxAddress("Hugo Santos", "hsantos.silva.nutricionistas@po.itau.pt"),
                new MailboxAddress("Raquel Inácio", "raquel.inacio@ulsge.min-saude.pt"),
                new MailboxAddress("Luis Gomes", "luis.gomes@ulsge.min-saude.pt"),
                new MailboxAddress("Fátima Viana", "fatima.viana@ulsge.min-saude.pt"),
                new MailboxAddress("", "idias@ulsge.min-saude.pt")
                //new MailboxAddress("Pedro Vieira", "pedro.vieira@ulsge.min-saude.pt")
            });

            var bodyBuilder = new BodyBuilder();


            message.Cc.Add(new MailboxAddress("Serviço", "enfermeiros.imunohemoterapia@ulsge.min-saude.pt"));
            message.Cc.Add(new MailboxAddress("Pedro Vieira", "pedro.vieira@ulsge.min-saude.pt"));
            string subject = $"Banco de Sangue | Requisição de Suplementos Alimentares - Requisição para {pedido.DataNecessidade.ToString("yyyy-MM-dd")}";
            message.Subject = subject;

            var image = bodyBuilder.LinkedResources.Add(_logoPath);
            var imageContentId = MimeUtils.GenerateMessageId();
            image.ContentId = imageContentId;

            string emailBodyHtml = $"<div style='color: #000000;'>{pedido.EmailBody.Replace("\n", "<br>")}</div>";

            bodyBuilder.HtmlBody = $"<div style='color: #000000;'>{emailBodyHtml}<br><br>{GetEmailSignature(pedido.Responsavel, imageContentId)}</div>";

            string formattedDate = DateTime.Now.ToString("yyyy-MM-dd_HHmmss", CultureInfo.InvariantCulture);
            string pdfFileName = $"Requisicao_Suplementos_Alimentares_BancoSangue_{formattedDate}.pdf";

            bodyBuilder.Attachments.Add(pdfFileName, pdfBytes, new ContentType("application", "pdf"));

            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpServer, _smtpPort, false);
                    await client.AuthenticateAsync(_smtpUser, _smtpPass);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending email: {ex.Message}");
                return false;
            }
        }

        private string GetEmailSignature(string responsavel, string imageContentId)
        {
            return $@"
<p style='color: #000000;'><small><strong>Não responda a este e-mail.</strong><br>
Esta conta é utilizada apenas como remetente de mensagens.<br>
Para contactar o Serviço de Imunohemoterapia, deverá utilizar o e-mail <a href='mailto:enfermeiro.chefe.imunohemoterapia@ulsge.min-saude.pt'>enfermeiro.chefe.imunohemoterapia@ulsge.min-saude.pt</a></small></p>
<p style='color: #000000;'>Com os melhores cumprimentos,</p>
<p style='color: #000000;'><strong>Serviço de Imunohemoterapia | Banco de Sangue</strong><br>
EXT: 39539</p>
<img src=""cid:{imageContentId}"" alt='Logotipo' style='width: 300px;'><br>
<p style='color: #000000;'><small><strong>NÃO IMPRIMA ESTE E-MAIL, OPTE PELOS SUPORTES DIGITAIS</strong><br>
ULSGE – DISCLAIMER: Este e-mail é de uso exclusivo do destinatário ou destinatários a quem é dirigido, conforme consta na mensagem acima, e poderá conter informação confidencial. Por favor notifique-nos de imediato se este e-mail lhe foi endereçado por erro. This e-mail is intended for the use of only the individual or entity named above to whom it is addressed and may contain personal and/or confidential information. Please notify us immediately if you are not the intended recipient.</small></p>";
        }
    }
}