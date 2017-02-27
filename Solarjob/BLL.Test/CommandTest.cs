using System.Linq;
using BLL.Commands;
using BLL.Components;
using BLL.Infrastructure.CommandHandlers;
using DAL.Abstraction;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLL.Test
{
	[TestClass]
	public class CommandTest
	{
		[TestMethod]
		public void CreateTask()
		{


			IRepository<Command> taskRepository = new InMemoryRepository<Command>();
			ICommandHandler<SendMessageTaskCreateCommand> createCommandHandler = new CommandComponent(taskRepository);

			var createCommand =new SendMessageTaskCreateCommand();
			createCommandHandler.Handle(createCommand);

			var command = taskRepository.First();

		}
	}
}