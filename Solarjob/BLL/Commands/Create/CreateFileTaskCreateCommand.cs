using System;

namespace BLL.Commands.Create
{
	public class CreateFileTaskCreateCommand: StartTimeCommand
	{
		public CreateFileTaskCreateCommand(DateTime startTime,string name)
			: base(startTime)
		{
			Name = name;
		}

		public int Delay { get; set; } 

		public string Name { get; set; }

		
	}
}
