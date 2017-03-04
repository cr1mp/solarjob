namespace WsClient.BLL.Infrastructure
{
	public interface ICommandProcessor
	{
		void Process<TCommand>(TCommand command);
	}
}