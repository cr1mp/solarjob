using System;
using System.Linq;
using AutoMapper;
using BLL.Commands;
using BLL.Commands.Create;
using BLL.Components;
using BLL.Infrastructure.CommandHandlers;
using BLL.Mapper;
using DAL.Abstraction;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Params;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utility.Deserializer;
using Utility.Serializer;

namespace BLL.Test
{
	[TestClass]
	public class CommandTest
	{
		[TestMethod]
		public void CreateSendMessageTask()
		{
			var start = DateTime.Now;

			IRepository<Task> taskRepository = new InMemoryRepository<Task>();
			var config = new MapperConfiguration(cfg =>cfg.AddProfile(new BllAutoMapperProfile()));
			ICommandHandler<SendMessageTaskCreateCommand> commandHandler = new CommandComponent(taskRepository,
																								new JsonSerializer(),
																								config.CreateMapper());

			var command = new SendMessageTaskCreateCommand(start,"address","message", "theme");
			commandHandler.Handle(command);

			var tast = taskRepository.First();

			Assert.IsNotNull(tast.Params);
			var sendMessane = new JsonDeserializer().Deserialize<SendMessage>(tast.Params);

			Assert.AreEqual("address", sendMessane.Address);
			Assert.AreEqual("message", sendMessane.Message);
			Assert.AreEqual("theme", sendMessane.Theme);

			Assert.AreEqual(start, tast.StartTime);
			Assert.AreEqual("SendMessage", tast.Name);
			Assert.AreEqual(CommandState.New, tast.State);
			Assert.AreEqual(1, tast.Version);

		}

		[TestMethod]
		public void CreateFileTask()
		{
			var start = DateTime.Now;

			IRepository<Task> taskRepository = new InMemoryRepository<Task>();
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new BllAutoMapperProfile()));
			ICommandHandler<CreateFileTaskCreateCommand> commandHandler = new CommandComponent(taskRepository,
																								new JsonSerializer(),
																								config.CreateMapper());

			var command = new CreateFileTaskCreateCommand(start, "name");
			commandHandler.Handle(command);

			var createFile = taskRepository.First();

			Assert.IsNotNull(createFile.Params);
			var sendMessane = new JsonDeserializer().Deserialize<CreateFile>(createFile.Params);

			Assert.AreEqual("name", sendMessane.Name);

			Assert.AreEqual(start, createFile.StartTime);
			Assert.AreEqual("CreateFile", createFile.Name);
			Assert.AreEqual(CommandState.New, createFile.State);
			Assert.AreEqual(1, createFile.Version);

		}

		[TestMethod]
		public void CreateNewTask()
		{
			var start = DateTime.Now;

			IRepository<Task> taskRepository = new InMemoryRepository<Task>();
			var config = new MapperConfiguration(cfg => cfg.AddProfile(new BllAutoMapperProfile()));
			ICommandHandler<NewTaskCreateCommand> commandHandler = new CommandComponent(taskRepository,
																								new JsonSerializer(),
																								config.CreateMapper());

			var command = new NewTaskCreateCommand(start);
			commandHandler.Handle(command);

			var createFile = taskRepository.First();

			Assert.IsNotNull(createFile.Params);
			var newTask = new JsonDeserializer().Deserialize<New>(createFile.Params);

			Assert.IsNotNull(newTask);

			Assert.AreEqual(start, createFile.StartTime);
			Assert.AreEqual("New", createFile.Name);
			Assert.AreEqual(CommandState.New, createFile.State);
			Assert.AreEqual(2, createFile.Version);

		}



	}
}