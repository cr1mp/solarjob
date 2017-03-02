using System;
using AutoMapper;
using BLL.Commands.ChangeState;
using BLL.Commands.Create;
using BLL.Components;
using BLL.Queries;
using WcfServer.Startup;
using WcfServer.V1.Dtos;
using Microsoft.Practices.Unity;
using System.Transactions;

namespace WcfServer.V1
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
	public class CommandService : ICommandService
	{
		protected readonly TaskCommandHandlers _taskCommandHandlers;
		protected readonly TaskQueryHandlers _taskQueryHandlers;
		protected readonly IMapper _mapper;

		public CommandService( )
		{
			_mapper = UnityContainerSingelton.Instance.Resolve<IMapper>();

			_taskCommandHandlers = UnityContainerSingelton.Instance.Resolve<TaskCommandHandlers>();
			_taskQueryHandlers = UnityContainerSingelton.Instance.Resolve<TaskQueryHandlers>();
		}

		public virtual CommandDto GetCommand()
		{
			return GetCommand(1);
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

		protected CommandDto GetCommand(int maxVersion)
		{
			using (var scope = new TransactionScope())
			{
				var task = _taskQueryHandlers.Handle(new GetNewTaskQuery(maxVersion));
				if (task == null)
				{
					return null;
				}

				_taskCommandHandlers.Handle(new SetStateInProcessCommand(task.Id));

				scope.Complete();

				return _mapper.Map<CommandDto>(task);
			}

			
		}
	}
}
