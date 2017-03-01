using System;
using BLL.Commands.ChangeState;
using BLL.Commands.Create;
using BLL.Components;
using BLL.Queries;
using WcfServer.V1.Dtos;

namespace WcfServer.V1
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
	public class CommandService : ICommandService
	{
		protected readonly TaskCommandHandlers _taskCommandHandlers;
		protected readonly TaskQueryHandlers _taskQueryHandlers;

		public CommandService(TaskCommandHandlers taskCommandHandlers, TaskQueryHandlers taskQueryHandlers )
		{
			_taskCommandHandlers = taskCommandHandlers;
			_taskQueryHandlers = taskQueryHandlers;
		}

		public CommandDto GetCommand()
		{
			var task = _taskQueryHandlers.Handle(new GetNewTaskQuery(1));
			if (task == null)
			{
				return null;
			}

			_taskCommandHandlers.Handle(new SetStateInProcessCommand(task.Id));

			return new CommandDto();
		}

		public void Done(Guid commandId)
		{
			_taskCommandHandlers.Handle(new SetStateDoneCommand(commandId));
		}

		public void Fail(Guid commandId)
		{
			_taskCommandHandlers.Handle(new SetStateNewCommand(commandId));
		}

		public void AddTask(SendMessageCommandDto command)
		{
			_taskCommandHandlers.Handle(new SendMessageTaskCreateCommand(command.StartTime,
																		command.Address,
																		command.Message,
																		command.Theme));
		}

		public void AddTask(CreateFileCommandDto command)
		{
			_taskCommandHandlers.Handle(new CreateFileTaskCreateCommand(command.StartTime,
																		command.Name));
		}
	}
}
