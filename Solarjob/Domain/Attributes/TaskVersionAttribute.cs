using System;

namespace Domain.Attributes
{
	public class TaskVersionAttribute : Attribute
	{
		public TaskVersionAttribute( int startVersion)
		{
			
			StartVersion = startVersion;
		}

		public int StartVersion { get; set; } 
	}
}