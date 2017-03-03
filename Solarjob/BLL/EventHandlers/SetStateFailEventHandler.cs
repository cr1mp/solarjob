using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Commands.ChangeState;
using BLL.Infrastructure.EventHandlers;
using Utility.Email;

namespace BLL.EventHandlers
{
	class SetStateFailEventHandler : IEventHandler<SetStateFailCommand>
	{
		private readonly IEmailSender _emailSender;

		public SetStateFailEventHandler(IEmailSender emailSender)
		{
			_emailSender = emailSender;
		}

		/// <summary>
		/// Если ошибка то сообщаем админам.
		/// </summary>
		/// <param name="parameter"></param>
		public void Handle(SetStateFailCommand parameter)
		{
			var admins = ConfigurationManager.AppSettings["Admins"]?.Split(',');

			if (admins != null)
			{
				foreach (var admin in admins)
				{
					_emailSender.Send(new EmailMessage(admin, "Ошибка", $"При выполнении задачи {parameter.TaskId} произошла ошибка."));
				}
			}
		}
	}
}
