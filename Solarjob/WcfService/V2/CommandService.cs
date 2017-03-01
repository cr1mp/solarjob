using System;
using BLL.Commands.Create;
using BLL.Components;
using WcfServer.V2.Dtos;

namespace WcfServer.V2
{
	
	public class CommandService : V1.CommandService, ICommandService
	{
		public CommandService(TaskCommandHandlers taskCommandHandlers, TaskQueryHandlers taskQueryHandlers) 
			: base(taskCommandHandlers, taskQueryHandlers)
		{
		}

		public void AddTask(NewCommandDto command)
		{
			_taskCommandHandlers.Handle(new NewTaskCreateCommand(command.StartTime));
		}

		
	}
}
