using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Params;
using Utility.Email;
using WsClient.BLL.Infrastructure;

namespace WsClient.BLL.CommandHandlers
{
	public class SendMessageCommandHandler:ICommandHandler<SendMessage>
	{
		private readonly IEmailSender _emailSender;

		public SendMessageCommandHandler(IEmailSender emailSender)
		{
			_emailSender = emailSender;
		}

		public void Handle(SendMessage command)
		{
			_emailSender.Send(new EmailMessage(command.Address, command.Theme, command.Message));
		}
	}
}
