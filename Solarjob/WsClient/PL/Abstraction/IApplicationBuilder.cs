using System;
using AutoMapper;
using Microsoft.Practices.Unity;
using Serilog;

namespace WsClient.PL.Abstraction
{
	public interface IApplicationBuilder
	{
		IApplicationBuilder ConfigireContainer(Action<IUnityContainer> configireContainer);

		IApplicationBuilder ConfigureLogging(Action<LoggerConfiguration> configureLogging);

		IApplicationBuilder ConfigureMappers(Action<IMapperConfigurationExpression> configureMappers);

		IApplication Build();
	}
}