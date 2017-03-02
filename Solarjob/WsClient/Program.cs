using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Serilog;
using WsClient.BLL;

namespace WsClient
{
	static class Program
	{
		private static ILogger _log;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(params string[] param)
		{
			var container = ConfigureApp();

			if (param.Any(x => x == "-Console"))
			{
				var cancelToken = new CancellationTokenSource();

				Task.Factory.StartNew(()=>Run(cancelToken.Token,container.Resolve<TaskComponent>()),cancelToken.Token);

				Console.WriteLine();
				cancelToken.Cancel();


			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[]
				{
					container.Resolve<TaskerWsClient>()
				};
				ServiceBase.Run(ServicesToRun);
			}
		}


		private static void Run(CancellationToken cancellationToken,TaskComponent taskComponent)
		{
			taskComponent.Start();
			while (true)
			{

				cancellationToken.ThrowIfCancellationRequested();
			}
			taskComponent.Stop();
		}

		private static IUnityContainer ConfigureApp()
		{
			var container = new UnityContainer();
			container.RegisterType<ILogger>(new InjectionFactory((ctr, type, name) =>
			{
				ILogger log = new LoggerConfiguration()
				 .WriteTo.LiterateConsole()
				.CreateLogger();

				return log;
			}));

			_log = container.Resolve<ILogger>();

			_log.Debug("Начало конфигурирования контейнера. ID потока: {ThreadId}. Tags {@Tags}.", Thread.CurrentThread.ManagedThreadId, new[] { "TelegramNotify", "Start" });
			ConfigContainer(container);
			_log.Debug("Конец конфигурирования контейнера. ID потока: {ThreadId}. Tags {@Tags}.", Thread.CurrentThread.ManagedThreadId, new[] { "TelegramNotify", "End" });

			return container;
		}

		private static void ConfigContainer(IUnityContainer container)
		{


			container.RegisterInstance<IUnityContainer>(container);
			container.RegisterType<TaskerWsClient>();

			container.Resolve<TelegtamAPIModule>().Configure(container);
			container.Resolve<TelegramNotifyWcfServiceModule>().Configure(container);

			DependencyFactory.Container = container;
		}
	}
}
