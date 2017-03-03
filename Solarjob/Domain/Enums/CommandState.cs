namespace Domain.Enums
{
	/// <summary>
	/// Статус задачи.
	/// </summary>
	public enum TaskState
	{
		/// <summary>
		/// Новая задача.
		/// </summary>
		New,
		/// <summary>
		/// Задача в процессе.
		/// </summary>
		InProcess,
		/// <summary>
		/// Задача успешно выполнена.
		/// </summary>
		Done,
		/// <summary>
		/// При выполнении задачи возникла ошибка.
		/// </summary>
		Fail,
	}
}