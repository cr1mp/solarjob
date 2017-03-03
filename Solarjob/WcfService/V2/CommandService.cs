using System;
using BLL.Commands.Create;
using WcfServer.Results;
using WcfServer.V1.Dtos;
using WcfServer.V2.Dtos;

namespace WcfServer.V2
{

	public class CommandService : V1.CommandService, ICommandService
	{
		public Result AddTask(NewCommandDto command)
		{
			_taskCommandHandlers.Handle(new NewTaskCreateCommand(command.StartTime, command.TaskName));
			return new Result();
		}

		public override Result<CommandDto> GetCommand(string clientName)
		{
			return new Result<CommandDto>(GetCommand(2, clientName));
		}
	}
}
