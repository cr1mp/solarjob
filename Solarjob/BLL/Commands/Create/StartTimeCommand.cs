using System;

namespace BLL.Commands.Create
{
	public class StartTimeCommand
	{
		protected StartTimeCommand(DateTime startTime,string taskName)
		{
			StartTime = startTime;
			Name = taskName;
		}

		public DateTime StartTime { get; private set; }
		public string Name { get; private set; }
	}
}
