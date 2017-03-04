using System;
using Microsoft.Practices.Unity;
using Serilog;

namespace WsClient.PL
{
	internal interface IApplicationBuilder
	{
		IApplicationBuilder ConfigireContainer(Action<IUnityContainer> configireContainer);

		IApplicationBuilder ConfigureLogging(Action<ILogger> configureLogging);

		IApplicationBuilder ConfigureMappers();

		IApplication Build();
	}
}