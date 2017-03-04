using System;

namespace WsClient.PL
{
	internal interface IApplication:IDisposable
	{
		void Run();
	}
}