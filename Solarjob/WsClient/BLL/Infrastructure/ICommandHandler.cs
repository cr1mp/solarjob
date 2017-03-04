namespace WsClient.BLL.Infrastructure
{
	/// <summary>
	/// Обработчик команды.
	/// </summary>
	/// <typeparam name="TCommand">Тип команды.</typeparam>
	public interface ICommandHandler<in TCommand>
	{
		/// <summary>
		/// Обработать команду.
		/// </summary>
		/// <param name="command">Команда.</param>
		void Handle(TCommand command);
	}
}