using System;

namespace BLL.Commands.Create
{
	public class StartTimeCommand
	{
		protected StartTimeCommand(DateTime startTime)
		{
			StartTime = startTime;
		}

		public DateTime StartTime { get; private set; }
	}
}
