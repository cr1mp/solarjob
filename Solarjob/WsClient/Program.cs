using System;
using AutoMapper;
using Microsoft.Practices.Unity;
using Serilog;
using WsClient.PL;
using WsClient.PL.AppBuilder;
using WsClient.PL.ConsoleClient;
using WsClient.PL.WindowsServiceClient;
using WsClient.WcfServer.V1;

namespace WsClient
{
	class Program
	{

		static void Main()
		{
			var application = new ApplicationBuilder()
#if DEBUG
				.UseConsole()
#else
				.UseWindowsCervice()
#endif
				.ConfigireContainer(ConfigContainer)
				.ConfigureLogging(ConfigureLogging)
				.ConfigureMappers(ConfigureMappers)
				.Build();

			application.Run();
		}

		private static void ConfigureMappers(IMapperConfigurationExpression mapperConfigurationExpression)
		{
			
		}

		private static void ConfigureLogging(LoggerConfiguration loggerConfiguration)
		{
			loggerConfiguration.WriteTo
				.LiterateConsole();
		}

		private static void ConfigContainer(IUnityContainer container)
		{
			container.RegisterType<TaskerWsClient>();
			container.RegisterType<ICommandService,CommandServiceClient>(new InjectionConstructor());

			//container.Resolve<TelegtamAPIModule>().Configure(container);
			//container.Resolve<TelegramNotifyWcfServiceModule>().Configure(container);

			//DependencyFactory.Container = container;
		}
	}
}