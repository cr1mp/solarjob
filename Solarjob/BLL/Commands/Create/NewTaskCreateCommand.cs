using System;

namespace BLL.Commands.Create
{
	public class NewTaskCreateCommand: StartTimeCommand
	{
		public NewTaskCreateCommand(DateTime startTime) 
			: base(startTime)
		{
		}
	}
}
