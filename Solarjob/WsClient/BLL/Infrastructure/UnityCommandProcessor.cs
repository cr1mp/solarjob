using System;
using Microsoft.Practices.Unity;

namespace WsClient.BLL.Infrastructure
{
	public class UnityCommandProcessor : CommandProcessor, ICommandProcessor
	{
		private readonly IUnityContainer _unityContainer;

		public UnityCommandProcessor(IUnityContainer unityContainer)
		{
			_unityContainer = unityContainer;
		}

		protected override dynamic GetCommandHandler(Type handlerType)
		{
			return _unityContainer.Resolve(handlerType);
		}
	}
}