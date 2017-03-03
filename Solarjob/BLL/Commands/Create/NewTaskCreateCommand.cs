using System;

namespace BLL.Commands.Create
{
	public class NewTaskCreateCommand: StartTimeCommand
	{
		public NewTaskCreateCommand(DateTime startTime,string taskName) 
			: base(startTime, taskName)
		{
		}
	}
}
