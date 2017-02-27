using System;

namespace Domain.Models
{
	public class Task
	{
		protected Task()
		{
			
		}

		public string Name { get; set; }
		public DateTime StartTime { get; set; }
	}
}