using System;

namespace Domain.Attributes
{
	/// <summary>
	/// Для поддержки обратной совместимости клиентов храним версию с котрой началась поддержка этого типа задач.
	/// </summary>
	public class TaskStartClientVersionAttribute : Attribute
	{
		public TaskStartClientVersionAttribute( int startClientVersion)
		{
			
			StartClientVersion = startClientVersion;
		}

		public int StartClientVersion { get; set; } 
	}
}