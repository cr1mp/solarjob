using System;
using System.ServiceProcess;
using WsClient.PL.Abstraction;

namespace WsClient.PL.WindowsServiceClient
{
	public class WindowsServiceClient : IClient
	{
		private readonly TaskerWsClient _taskerWsClient;

		public WindowsServiceClient(TaskerWsClient taskerWsClient)
		{
			_taskerWsClient = taskerWsClient;
		}

		public void Start()
		{
			ServiceBase.Run(new ServiceBase[]
			{
				_taskerWsClient
			});
		}

		public void Dispose()
		{
			_taskerWsClient.Dispose();
		}
	}
}