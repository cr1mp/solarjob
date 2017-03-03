using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.Commands.ChangeState;
using BLL.Commands.Create;
using BLL.Events;
using BLL.Infrastructure.CommandHandlers;
using DAL.Abstraction;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Params;
using Microsoft.Practices.Prism.PubSubEvents;
using Utility;
using Utility.Serializer;

namespace BLL.Components
{
	/// <summary>
	/// Команды которые меняют состояние системы.
	/// </summary>
	public partial class TaskCommandHandlers : ICommandHandler<SendMessageTaskCreateCommand>,
												ICommandHandler<CreateFileTaskCreateCommand>,
												ICommandHandler<NewTaskCreateCommand>,
												ICommandHandler<SetStateDoneCommand>,
												ICommandHandler<SetStateInProcessCommand>,
												ICommandHandler<SetStateFailCommand>

	{
		private readonly EntityRepository<Job, Guid> _taskRepository;
		private readonly ISerializer _serializer;
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkFactory _factory;
		private readonly IEventAggregator _eventAggregator;

		public TaskCommandHandlers(EntityRepository<Job, Guid> taskRepository,
			ISerializer serializer,
			IMapper mapper,
			IUnitOfWorkFactory factory,
			IEventAggregator eventAggregator)
		{
			_taskRepository = taskRepository;
			_serializer = serializer;
			_mapper = mapper;
			_factory = factory;
			_eventAggregator = eventAggregator;
		}

		/// <summary>
		/// Создать задачу по отправке сообщения.
		/// </summary>
		/// <param name="command"></param>
		public void Handle(SendMessageTaskCreateCommand command)
		{
			var sendMessageTask = _mapper.Map<SendMessage>(command);
			AddNewTask(sendMessageTask, command);
		}

		/// <summary>
		/// Создать задачу по созданию файла.
		/// </summary>
		/// <param name="command"></param>
		public void Handle(CreateFileTaskCreateCommand command)
		{
			var sendMessageTask = _mapper.Map<CreateFile>(command);
			sendMessageTask.Delay = command.Delay <= 0 ? 10000 : command.Delay;
			AddNewTask(sendMessageTask, command);
		}

		/// <summary>
		/// Создать новую задачу.
		/// </summary>
		/// <param name="command"></param>
		public void Handle(NewTaskCreateCommand command)
		{
			var sendMessageTask = _mapper.Map<NewTask>(command);
			AddNewTask(sendMessageTask, command);
		}

		private void AddNewTask(object param, StartTimeCommand startTimeCommand)
		{
			CommitWrapper(() =>
			{
				var task = new Job
				{
					State = TaskState.New,
					StartTime = startTimeCommand.StartTime,
					Name = startTimeCommand.Name,
					Version = param.GetClientStartVersion(),
					Params = _serializer.Serialize(param),
					MimeType = _serializer.GetMimeType(),
					Type = param.GetTaskType(),
				};


				_taskRepository.Add(task);
			});
		}

		/// <summary>
		/// Поменять статус задачи -> Выполнена.
		/// </summary>
		/// <param name="command"></param>
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

		/// <summary>
		/// Поменять статус задачи -> В процессе.
		/// </summary>
		/// <param name="command"></param>
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

				task.Executor = command.Executor;
			});
		}

		/// <summary>
		/// Поменять статус задачи -> Выполнена с ошибкой.
		/// </summary>
		/// <param name="command"></param>
		public void Handle(SetStateFailCommand command)
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

				task.State = TaskState.Fail;

			});

			_eventAggregator.GetEvent<SetStateFailEvent>().Publish(command);
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
