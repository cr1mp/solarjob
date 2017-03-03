using System;
using System.Linq;
using AutoMapper;
using BLL.Commands;
using BLL.Commands.ChangeState;
using BLL.Commands.Create;
using BLL.Components;
using BLL.Dtos;
using BLL.Infrastructure.CommandHandlers;
using BLL.Infrastructure.QueryHandlers;
using BLL.Mapper;
using BLL.Queries;
using DAL.Abstraction;
using Domain.Enums;
using Domain.Models.Params;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Utility.Deserializer;
using Utility.Serializer;
using Job = Domain.Models.Job;

namespace BLL.Test
{
	[TestClass]
	public class CommandTest
	{
		private ISerializer _serializer;
		EntityRepository<Job, Guid> _taskRepository;
		private TaskCommandHandlers _taskCommandHandlers;
		private TaskQueryHandlers _taskQueryHandlers;


		[TestInitialize()]
		public void Initialize()
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new BllAutoMapperProfile()));
			IMapper mapper = config.CreateMapper();
			_serializer = new JsonSerializer();
			IUnitOfWorkFactory factory = Mock.Create<IUnitOfWorkFactory>();
			_taskRepository = new InMemoryRepository<Job>();
			IEventAggregator eventAggregator = Mock.Create<IEventAggregator>();

			_taskCommandHandlers = new TaskCommandHandlers(_taskRepository,
															_serializer,
															mapper,
															factory,
															eventAggregator);

			_taskQueryHandlers = new TaskQueryHandlers(_taskRepository,mapper,factory);
		}

		[TestMethod]
		public void CreateSendMessageTask()
		{
			var start = DateTime.Now;

			_taskRepository = new InMemoryRepository<Job>();
			
			ICommandHandler<SendMessageTaskCreateCommand> commandHandler = _taskCommandHandlers;

			var command = new SendMessageTaskCreateCommand(start, "name", "address","message", "theme");
			commandHandler.Handle(command);

			var tast = _taskRepository.First();

			Assert.IsNotNull(tast.Params);
			var sendMessage = new JsonDeserializer().Deserialize<SendMessage>(tast.Params);

			Assert.AreEqual("address", sendMessage.Address);
			Assert.AreEqual("message", sendMessage.Message);
			Assert.AreEqual("theme", sendMessage.Theme);

			Assert.AreEqual(start, tast.StartTime);
			Assert.AreEqual("SendMessage", tast.Name);
			Assert.AreEqual(TaskState.New, tast.State);
			Assert.AreEqual(1, tast.Version);

		}

		[TestMethod]
		public void CreateFileTask()
		{
			var start = DateTime.Now;

			_taskRepository = new InMemoryRepository<Job>();
			
			ICommandHandler<CreateFileTaskCreateCommand> commandHandler = _taskCommandHandlers;

			var command = new CreateFileTaskCreateCommand(start, "name", "name");
			commandHandler.Handle(command);

			var task = _taskRepository.First();

			Assert.IsNotNull(task.Params);
			var sendMessane = new JsonDeserializer().Deserialize<CreateFile>(task.Params);

			Assert.AreEqual("name", sendMessane.Name);

			Assert.AreEqual(start, task.StartTime);
			Assert.AreEqual("CreateFile", task.Name);
			Assert.AreEqual(TaskState.New, task.State);
			Assert.AreEqual(1, task.Version);

		}

		[TestMethod]
		public void CreateNewTask()
		{
			var start = DateTime.Now;

			_taskRepository = new InMemoryRepository<Job>();

			ICommandHandler<NewTaskCreateCommand> commandHandler = _taskCommandHandlers;

			var command = new NewTaskCreateCommand(start,"name");
			commandHandler.Handle(command);

			var task = _taskRepository.First();

			Assert.IsNotNull(task.Params);
			var newTask = new JsonDeserializer().Deserialize<NewTask>(task.Params);

			Assert.IsNotNull(newTask);

			Assert.AreEqual(start, task.StartTime);
			Assert.AreEqual("New", task.Name);
			Assert.AreEqual(TaskState.New, task.State);
			Assert.AreEqual(2, task.Version);

		}

		[TestMethod]
		public void ChangeStateTask()
		{
			Guid taskId=Guid.Parse("{E79904A7-F442-454E-B8EF-01B1C1AACDD4}");
			_taskRepository = new InMemoryRepository<Job>();

			_taskRepository.Add(new Job
			{
				Id = taskId,
				State = TaskState.New
			});
			TaskCommandHandlers taskCommandComponent = _taskCommandHandlers;


			ICommandHandler<SetStateInProcessCommand> setStateInProcessCommandHandler1 = taskCommandComponent;
			var setStateInProcessCommand = new SetStateInProcessCommand(taskId, "executor");
			setStateInProcessCommandHandler1.Handle(setStateInProcessCommand);

			var task = _taskRepository.First();

			Assert.AreEqual(TaskState.InProcess, task.State);


			ICommandHandler<SetStateDoneCommand> setStateDoneCommandHandler = taskCommandComponent;
			var setStateDoneCommand = new SetStateDoneCommand(taskId);
			setStateDoneCommandHandler.Handle(setStateDoneCommand);

			task = _taskRepository.First();

			Assert.AreEqual(TaskState.Done, task.State);

			task.State = TaskState.InProcess;


			ICommandHandler<SetStateFailCommand> setStateNewCommandHandler = taskCommandComponent;
			var setStateNewCommand = new SetStateFailCommand(taskId);
			setStateNewCommandHandler.Handle(setStateNewCommand);

			task = _taskRepository.First();

			Assert.AreEqual(TaskState.New, task.State);
		}

		[TestMethod]
		public void GetTask()
		{
			var task1V1 = new Job
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Parse("1990 10 16"),
				Version = 1
			};
			var task2V1 = new Job
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Parse("1990 10 17"),
				Version = 1
			};
			

			var task1V2 = new Job
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Parse("1990 10 17"),
				Version = 2
			};
			var task2V2 = new Job
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Parse("1990 10 16"),
				Version = 2
			};

			var task1V3 = new Job
			{
				Id = Guid.NewGuid(),
				StartTime = DateTime.Parse("2018 10 16"),
				Version = 3
			};

			_taskRepository = new InMemoryRepository<Job>();
			_taskRepository.Add(task1V1);
			_taskRepository.Add(task2V1);

			_taskRepository.Add(task1V2);
			_taskRepository.Add(task2V2);

			_taskRepository.Add(task1V3);

			IQueryHandler<GetNewTaskQuery, TaskDto> taskCommandComponent = _taskQueryHandlers;

			var taskDto = taskCommandComponent.Handle(new GetNewTaskQuery(1));
			var task = _taskRepository.GetById(taskDto.Id);
			Assert.AreEqual(task1V1, task);
			task.State=TaskState.InProcess;

			taskDto = taskCommandComponent.Handle(new GetNewTaskQuery(2));
			task = _taskRepository.GetById(taskDto.Id);
			Assert.AreEqual(task2V2, task);
			task.State = TaskState.Done;

			taskDto = taskCommandComponent.Handle(new GetNewTaskQuery(1));
			task = _taskRepository.GetById(taskDto.Id);
			Assert.AreEqual(task2V1, task);
			task.State = TaskState.Done;

			taskDto = taskCommandComponent.Handle(new GetNewTaskQuery(2));
			task = _taskRepository.GetById(taskDto.Id);
			Assert.AreEqual(task1V2, task);
			task.State = TaskState.Done;

			taskDto = taskCommandComponent.Handle(new GetNewTaskQuery(3));
			Assert.IsNull(taskDto);

		}
	}
}