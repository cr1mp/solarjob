using System;
using System.Linq;
using AutoMapper;
using BLL.Commands;
using BLL.Commands.ChangeState;
using BLL.Commands.Create;
using BLL.Components;
using BLL.Infrastructure.CommandHandlers;
using BLL.Infrastructure.QueryHandlers;
using BLL.Mapper;
using BLL.Queries;
using DAL.Abstraction;
using Domain.Enums;
using Domain.Models.Params;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility.Deserializer;
using Utility.Serializer;
using Task = Domain.Models.Task;

namespace BLL.Test
{
	[TestClass]
	public class CommandTest
	{
		[TestMethod]
		public void CreateSendMessageTask()
		{
			var start = DateTime.Now;

			EntityRepository<Task,Guid> taskRepository = new InMemoryRepository<Task>();
			var config = new MapperConfiguration(cfg =>cfg.AddProfile(new BllAutoMapperProfile()));
			ICommandHandler<SendMessageTaskCreateCommand> commandHandler = new TaskCommandHandlers(taskRepository,
																								new JsonSerializer(),
																								config.CreateMapper());

			var command = new SendMessageTaskCreateCommand(start,"address","message", "theme");
			commandHandler.Handle(command);

			var tast = taskRepository.First();

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

			EntityRepository<Task, Guid> taskRepository = new InMemoryRepository<Task>();
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new BllAutoMapperProfile()));
			ICommandHandler<CreateFileTaskCreateCommand> commandHandler = new TaskCommandHandlers(taskRepository,
																								new JsonSerializer(),
																								config.CreateMapper());

			var command = new CreateFileTaskCreateCommand(start, "name");
			commandHandler.Handle(command);

			var task = taskRepository.First();

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

			EntityRepository<Task, Guid> taskRepository = new InMemoryRepository<Task>();
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new BllAutoMapperProfile()));
			ICommandHandler<NewTaskCreateCommand> commandHandler = new TaskCommandHandlers(taskRepository,
																								new JsonSerializer(),
																								config.CreateMapper());

			var command = new NewTaskCreateCommand(start);
			commandHandler.Handle(command);

			var task = taskRepository.First();

			Assert.IsNotNull(task.Params);
			var newTask = new JsonDeserializer().Deserialize<New>(task.Params);

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
			EntityRepository<Task, Guid> taskRepository = new InMemoryRepository<Task>();
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new BllAutoMapperProfile()));
			taskRepository.Add(new Task
			{
				Id = taskId,
				State = TaskState.New
			});
			TaskCommandHandlers taskCommandComponent = new TaskCommandHandlers(taskRepository,
																								new JsonSerializer(),
																								config.CreateMapper());


			ICommandHandler<SetStateInProcessCommand> setStateInProcessCommandHandler1 = taskCommandComponent;
			var setStateInProcessCommand = new SetStateInProcessCommand(taskId);
			setStateInProcessCommandHandler1.Handle(setStateInProcessCommand);

			var task = taskRepository.First();

			Assert.AreEqual(TaskState.InProcess, task.State);


			ICommandHandler<SetStateDoneCommand> setStateDoneCommandHandler = taskCommandComponent;
			var setStateDoneCommand = new SetStateDoneCommand(taskId);
			setStateDoneCommandHandler.Handle(setStateDoneCommand);

			task = taskRepository.First();

			Assert.AreEqual(TaskState.Done, task.State);

			task.State = TaskState.InProcess;


			ICommandHandler<SetStateNewCommand> setStateNewCommandHandler = taskCommandComponent;
			var setStateNewCommand = new SetStateNewCommand(taskId);
			setStateNewCommandHandler.Handle(setStateNewCommand);

			task = taskRepository.First();

			Assert.AreEqual(TaskState.New, task.State);
		}

		[TestMethod]
		public void GetTask()
		{
			var task1V1 = new Task
			{
				StartTime = DateTime.Parse("1990 10 16"),
				Version = 1
			};
			var task2V1 = new Task
			{
				StartTime = DateTime.Parse("1990 10 17"),
				Version = 1
			};
			

			var task1V2 = new Task
			{
				StartTime = DateTime.Parse("1990 10 17"),
				Version = 2
			};
			var task2V2 = new Task
			{
				StartTime = DateTime.Parse("1990 10 16"),
				Version = 2
			};

			var task1V3 = new Task
			{
				StartTime = DateTime.Parse("2018 10 16"),
				Version = 3
			};

			EntityRepository<Task, Guid> taskRepository = new InMemoryRepository<Task>();
			taskRepository.Add(task1V1);
			taskRepository.Add(task2V1);
			
			taskRepository.Add(task1V2);
			taskRepository.Add(task2V2);

			taskRepository.Add(task1V3);

			IQueryHandler<GetNewTaskQuery, Task> taskCommandComponent = new TaskQueryHandlers(taskRepository);

			var task = taskCommandComponent.Handle(new GetNewTaskQuery(1));
			Assert.AreEqual(task1V1, task);
			task.State=TaskState.InProcess;

			task = taskCommandComponent.Handle(new GetNewTaskQuery(2));
			Assert.AreEqual(task2V2, task);
			task.State = TaskState.Done;

			task = taskCommandComponent.Handle(new GetNewTaskQuery(1));
			Assert.AreEqual(task2V1, task);
			task.State = TaskState.Done;

			task = taskCommandComponent.Handle(new GetNewTaskQuery(2));
			Assert.AreEqual(task1V2, task);
			task.State = TaskState.Done;

			task = taskCommandComponent.Handle(new GetNewTaskQuery(3));
			Assert.IsNull(task);

		}
	}
}