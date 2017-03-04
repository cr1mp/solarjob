using Microsoft.Practices.Unity;
using WsClient.PL.Abstraction;

namespace WsClient.PL.App
{
	internal class Application:IApplication
	{
		private readonly IUnityContainer _container;
		private IClient Client;

		public Application(IUnityContainer container)
		{
			_container = container;
		}

		public void Run()
		{
			Client.Start();
		}

		public void Dispose()
		{
			Client.Dispose();
		}

		public void Initialize()
		{
			if (Client == null)
			{
				Client = _container.Resolve<IClient>();
			}
		}
	}
}