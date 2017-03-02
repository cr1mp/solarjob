using System;
using BLL.Commands.Create;
using WcfServer.V1.Dtos;
using WcfServer.V2.Dtos;

namespace WcfServer.V2
{
	
	public class CommandService : V1.CommandService, ICommandService
	{
		public void AddTask(NewCommandDto command)
		{
			_taskCommandHandlers.Handle(new NewTaskCreateCommand(command.StartTime));
		}

		public override CommandDto GetCommand()
		{
			return GetCommand(2);
		}
	}
}
