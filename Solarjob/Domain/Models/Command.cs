using System;
using Domain.Enums;

namespace Domain.Models
{
	public class Command:Entity
	{
		public CommandState State { get; set; }

		public int[] Versions { get; set; }

		public string Name { get; set; }

		public object Params { get; set; }

		public DateTime StartTime { get; set; }
	}
}
