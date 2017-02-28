using System;

namespace Domain.Attributes
{
	public class TaskNameAttribute:Attribute
	{
		public TaskNameAttribute(string name)
		{
			Name = name;
		}



		public string Name { get; set; }
	}
}
