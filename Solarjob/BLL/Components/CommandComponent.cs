using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.Commands;
using BLL.Commands.Create;
using BLL.Infrastructure.CommandHandlers;
using DAL.Abstraction;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Params;
using Utility;

namespace BLL.Components
{
	public class CommandComponent:ICommandHandler<SendMessageTaskCreateCommand>,
								  ICommandHandler<CreateFileTaskCreateCommand>,
								  ICommandHandler<NewTaskCreateCommand>,
								  ICommandHandler<DoneCommand>

	{
		private readonly EntityRepository<Task, Guid> _taskRepository;
		private readonly ISerializer _serializer;
		private readonly IMapper _mapper;

		public CommandComponent(EntityRepository<Task,Guid> taskRepository,
								ISerializer serializer,
								IMapper mapper)
		{
			_taskRepository = taskRepository;
			_serializer = serializer;
			_mapper = mapper;
		}

		public void Handle(SendMessageTaskCreateCommand command)
		{
			var sendMessageTask =_mapper.Map<SendMessage>(command);
			AddNewTask(sendMessageTask,command.StartTime);
		}

		public void Handle(CreateFileTaskCreateCommand command)
		{
			var sendMessageTask = _mapper.Map<CreateFile>(command);
			AddNewTask(sendMessageTask, command.StartTime);
		}

		public void Handle(NewTaskCreateCommand command)
		{
			var sendMessageTask = _mapper.Map<New>(command);
			AddNewTask(sendMessageTask, command.StartTime);
		}

		private void AddNewTask(object param, DateTime start)
		{
			var task = new Task();
			task.State = CommandState.New;
			task.StartTime = start;
			task.Name = param.GetTaskName();
			task.Version = param.GetStartVersion();
			task.Params = _serializer.Serialize(param);

			_taskRepository.Add(task);
		}

		public void Handle(DoneCommand command)
		{
			var task = _taskRepository.GetByIdOrNull(command.TaskId);

			if (task == null)
			{
				throw new NullReferenceException();
			}

			if (task.State != CommandState.InProcess)
			{
				throw new InvalidOperationException();
			}

			task.State = CommandState.Done;
		}
	}
}
