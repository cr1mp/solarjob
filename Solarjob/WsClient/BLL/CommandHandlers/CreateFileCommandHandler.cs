using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models.Params;
using WsClient.BLL.Infrastructure;

namespace WsClient.BLL.CommandHandlers
{
	public class CreateFileCommandHandler : ICommandHandler<CreateFile>
	{
		public void Handle(CreateFile command)
		{
			Thread.Sleep(command.Delay);

			File.Create(command.Name);
		}
	}
}
