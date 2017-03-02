using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.Commands.ChangeState;
using BLL.Commands.Create;
using BLL.Infrastructure.CommandHandlers;
using DAL.Abstraction;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Params;
using Utility;
using Utility.Serializer;

namespace BLL.Components
{
	public partial class TaskCommandHandlers : ICommandHandler<SendMessageTaskCreateCommand>,
		ICommandHandler<CreateFileTaskCreateCommand>,
		ICommandHandler<NewTaskCreateCommand>,
		ICommandHandler<SetStateDoneCommand>,
		ICommandHandler<SetStateInProcessCommand>,
		ICommandHandler<SetStateNewCommand>

	{
		private readonly EntityRepository<Task, Guid> _taskRepository;
		private readonly ISerializer _serializer;
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkFactory _factory;

		public TaskCommandHandlers(EntityRepository<Task, Guid> taskRepository,
			ISerializer serializer,
			IMapper mapper,
			IUnitOfWorkFactory factory)
		{
			_taskRepository = taskRepository;
			_serializer = serializer;
			_mapper = mapper;
			_factory = factory;
		}

		public void Handle(SendMessageTaskCreateCommand command)
		{
			var sendMessageTask = _mapper.Map<SendMessage>(command);
			AddNewTask(sendMessageTask, command.StartTime);
		}

		public void Handle(CreateFileTaskCreateCommand command)
		{
			var sendMessageTask = _mapper.Map<CreateFile>(command);
			sendMessageTask.Delay = command.Delay <= 0 ? 10000 : command.Delay;
			AddNewTask(sendMessageTask, command.StartTime);
		}

		public void Handle(NewTaskCreateCommand command)
		{
			var sendMessageTask = _mapper.Map<New>(command);
			AddNewTask(sendMessageTask, command.StartTime);
		}

		private void AddNewTask(object param, DateTime start)
		{
			CommitWrapper(() =>
			{
				var task = new Task();
				task.State = TaskState.New;
				task.StartTime = start;
				task.Name = param.GetTaskName();
				task.Version = param.GetStartVersion();
				task.Params = _serializer.Serialize(param);

				_taskRepository.Add(task);
			});
		}

		public void Handle(SetStateDoneCommand command)
		{
			CommitWrapper(() =>
			{
				var task = _taskRepository.GetByIdOrNull(command.TaskId);

				if (task == null)
				{
					throw new NullReferenceException();
				}

				if (task.State != TaskState.InProcess)
				{
					throw new InvalidOperationException();
				}

				task.State = TaskState.Done;
			});
		}

		public void Handle(SetStateInProcessCommand command)
		{
			CommitWrapper(() =>
			{
				var task = _taskRepository.GetByIdOrNull(command.TaskId);

				if (task == null)
				{
					throw new NullReferenceException();
				}

				if (task.State != TaskState.New)
				{
					throw new InvalidOperationException();
				}

				task.State = TaskState.InProcess;
			});
		}

		public void Handle(SetStateNewCommand command)
		{
			CommitWrapper(() =>
			{
				var task = _taskRepository.GetByIdOrNull(command.TaskId);

				if (task == null)
				{
					throw new NullReferenceException();
				}

				if (task.State != TaskState.InProcess)
				{
					throw new InvalidOperationException();
				}

				task.State = TaskState.New;
			});
		}

		private void CommitWrapper(Action a)
		{
			using (var uow = _factory.Create())
			{
				a();
				uow.Commit();
			}
		}
	}
}
