using System;

namespace BLL.Commands.Create
{
	public class SendMessageTaskCreateCommand : StartTimeCommand
	{
		public SendMessageTaskCreateCommand(DateTime startTime,
											string address,
											string message,
											string theme)
			: base(startTime)
		{
			Address = address;
			Message = message;
			Theme = theme;
		}

		public string Address { get; set; }
		public string Message { get; set; }
		public string Theme { get; set; }
	}
}
