using System;

namespace WsClient.BLL.Infrastructure
{
	public abstract class CommandProcessor : ICommandProcessor
	{
		public void Process<TCommand>(TCommand command)
		{
			var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
			dynamic handler = GetCommandHandler(handlerType);
			if (handler == null)
			{
				throw new NullReferenceException($"Обработчик {typeof(TCommand).Name} не зареган в контейнере.");
			}
			handler.Handle((dynamic)command);
		}
		protected abstract dynamic GetCommandHandler(Type handlerType);
	}
}