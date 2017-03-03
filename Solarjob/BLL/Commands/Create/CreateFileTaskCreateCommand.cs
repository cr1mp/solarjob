using System;

namespace BLL.Commands.Create
{
	public class CreateFileTaskCreateCommand: StartTimeCommand
	{
		public CreateFileTaskCreateCommand(DateTime startTime, string taskName, string fileName)
			: base(startTime,taskName)
		{
			FileName = fileName;
		}

		public int Delay { get; set; } 

		public string FileName { get; set; }

		
	}
}
