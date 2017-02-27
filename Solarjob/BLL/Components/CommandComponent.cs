using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Commands;
using BLL.Infrastructure.CommandHandlers;
using DAL.Abstraction;
using Domain.Models;

namespace BLL.Components
{
	public class CommandComponent:ICommandHandler<SendMessageTaskCreateCommand>,
								  ICommandHandler<CreateFileTaskCreateCommand>,
								  ICommandHandler<NewTaskCreateCommand>

	{
		public CommandComponent(IRepository<Command> taskRepository)
		{
			
		}

		public void Handle(SendMessageTaskCreateCommand command)
		{
			throw new NotImplementedException();
		}

		public void Handle(CreateFileTaskCreateCommand command)
		{
			throw new NotImplementedException();
		}

		public void Handle(NewTaskCreateCommand command)
		{
			throw new NotImplementedException();
		}
	}
}
