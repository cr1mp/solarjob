using System;

namespace WcfService.V1
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
	public class CommandService : ICommandService
	{
		public virtual void AddCommand(SendMessageCommandDto createCommandDto)
		{
			throw new NotImplementedException();
		}

		public virtual void Done(Guid commandId)
		{
			throw new NotImplementedException();
		}

		public virtual void Fail(Guid commandId)
		{
			throw new NotImplementedException();
		}

		public virtual CommandDto GetCommand()
		{
			throw new NotImplementedException();
		}
	}
}
