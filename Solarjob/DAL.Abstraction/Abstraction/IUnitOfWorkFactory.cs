namespace DAL.Abstraction
{
	/// <summary>
	/// Интерфейс фабрики UnitOfWork.
	/// </summary>
	public interface IUnitOfWorkFactory
	{
		/// <summary>
		/// Создает UnitOfWork с поддержкой состояний, если у UnitOfWork не будет вызван метод <see cref="IUnitOfWork.Commit" />, то автоматически будет выполнен rollback.
		/// </summary>
		/// <returns>UnitOfWork</returns>
		IUnitOfWork Create();
	}
}
