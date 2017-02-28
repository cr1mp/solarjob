using System;
using Domain.Enums;

namespace Domain.Models
{
	public class Task:Entity
	{
		public CommandState State { get; set; }

		public int Version { get; set; }

		public string Name { get; set; }

		public string Params { get; set; }

		public DateTime StartTime { get; set; }
	}
}
