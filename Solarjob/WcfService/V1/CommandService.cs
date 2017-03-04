using System;
using AutoMapper;
using BLL.Commands.ChangeState;
using BLL.Commands.Create;
using BLL.Components;
using BLL.Queries;
using WcfServer.Startup;
using WcfServer.V1.Dtos;
using Microsoft.Practices.Unity;
using WcfServer.Results;

namespace WcfServer.V1
{
	public class CommandService : ICommandService
	{
		protected readonly TaskCommandHandlers _taskCommandHandlers;
		protected readonly TaskQueryHandlers _taskQueryHandlers;
		protected readonly IMapper _mapper;
		// Маркер блокировки.
		private static readonly object ThreadLock = new object();

		public CommandService()
		{
			_mapper = UnityContainerSingelton.Instance.Resolve<IMapper>();

			_taskCommandHandlers = UnityContainerSingelton.Instance.Resolve<TaskCommandHandlers>();
			_taskQueryHandlers = UnityContainerSingelton.Instance.Resolve<TaskQueryHandlers>();
		}

		public virtual Result<CommandDto> GetCommand(string clientName)
		{
			return new Result<CommandDto>(GetCommand(1, clientName));
		}

		public Result Done(Guid commandId)
		{
			_taskCommandHandlers.Handle(new SetStateDoneCommand(commandId));
			return new Result();
		}

		public Result Fail(Guid commandId)
		{
			_taskCommandHandlers.Handle(new SetStateFailCommand(commandId));
			return new Result();
		}

		public Result AddTask(SendMessageCommandDto command)
		{
			_taskCommandHandlers.Handle(new SendMessageTaskCreateCommand(command.StartTime,
																		command.TaskName,
																		command.Address,
																		command.Message,
																		command.Theme));
			return new Result();
		}

		public Result AddTask(CreateFileCommandDto command)
		{
			_taskCommandHandlers.Handle(new CreateFileTaskCreateCommand(command.StartTime,
																		command.TaskName,
																		command.FileName));
			return new Result();
		}

		public Result Ping()
		{
			return new Result();
		}

		protected CommandDto GetCommand(int maxVersion, string executor)
		{

			BLL.Dtos.TaskDto task;

			lock (ThreadLock)
			{
				task = _taskQueryHandlers.Handle(new GetNewTaskQuery(maxVersion));
				if (task == null)
				{
					return null;
				}

				_taskCommandHandlers.Handle(new SetStateInProcessCommand(task.Id, executor));
			}

			return _mapper.Map<CommandDto>(task);

		}
	}
}
