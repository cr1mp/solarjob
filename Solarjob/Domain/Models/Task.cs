using System;
using DAL.Abstraction;
using DAL.Abstraction.Entities;
using Domain.Enums;

namespace Domain.Models
{
	/// <summary>
	/// Задача на выполнение.
	/// </summary>
	public class Job:Entity, IEntity<Guid>
	{
		/// <summary>
		/// Состояние задачи.
		/// </summary>
		public TaskState State { get; set; }

		/// <summary>
		/// Версия с которой эта задача доступна клиентам.
		/// </summary>
		public int Version { get; set; }

		/// <summary>
		/// Имя задачи.
		/// Например "Отправить письмо Васе".
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Params { get; set; }

		/// <summary>
		/// Время начала выполнения задачи.
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// Тип задачи.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Кто выполняет задачу.
		/// </summary>
		public string Executor { get; set; }

		/// <summary>
		/// MIME тип параметров.
		/// </summary>
		public MimeType MimeType { get; set; }
	}
}
