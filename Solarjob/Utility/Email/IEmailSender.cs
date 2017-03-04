using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Email
{
	public interface IEmailSender
	{
		void Send(EmailMessage emailMessage);
	}

	public class EmailSender : IEmailSender
	{
		private string SenderEmail;
		private string SenderName;
		private string Smpt;
		private int Port;
		string SmtpUserName;
		private string SmtpPassword;

		public EmailSender()
		{
			SenderEmail = ConfigurationManager.AppSettings["SenderEmail"];
			SenderName = ConfigurationManager.AppSettings["SenderName"];
			Smpt = ConfigurationManager.AppSettings["Smpt"];
			int.TryParse( ConfigurationManager.AppSettings["Port"],out Port);
			SmtpUserName = ConfigurationManager.AppSettings["SmtpUserName"];
			SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
		}

		public void Send(EmailMessage emailMessage)
		{
			try
			{

				using (var message = new MailMessage { From = new MailAddress(SenderEmail, SenderName) })
				{
					//Формирование письма
					message.To.Add(new MailAddress(emailMessage.Receiver));
					
					message.Subject = emailMessage.Header;
					message.Body = emailMessage.Body;
					message.IsBodyHtml = true;
					

					//Авторизация на SMTP сервере
					using (var smtp = new SmtpClient(Smpt, Port))
					{
						smtp.EnableSsl = false;
						smtp.Credentials = new NetworkCredential(SmtpUserName, SmtpPassword);
						
							// Отключаем проверку сертификата совсем
							ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
						
						smtp.Send(message); //отправка
						
					}
				}
			}
			catch (Exception e)
			{
				//todo обработать исключение.
			}
		}
	}
}
