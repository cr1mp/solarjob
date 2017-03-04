using System;

namespace WsClient.PL.Abstraction
{
	public interface IApplication:IDisposable
	{
		void Run();
	}
}