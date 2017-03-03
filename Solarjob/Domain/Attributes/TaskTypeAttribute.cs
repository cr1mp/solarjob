using System;

namespace Domain.Attributes
{
	/// <summary>
	/// Созраняем тип задачи.
	/// Клиенты по типу задачи выбирают обработчика.
	/// </summary>
	public class TaskTypeAttribute:Attribute
	{
		public TaskTypeAttribute(string type)
		{
			Type = type;
		}

		public string Type { get; set; }
	}
}
