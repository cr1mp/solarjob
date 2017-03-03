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

namespace WsClient.PL
{
	class Bootstrapper
	{
		private static ILogger _log;

		public void RunApplication(params string[] param)
		{
			var container = ConfigureApp();

			if (param.Any(x => x == "-Console"))
			{
				var cancelToken = new CancellationTokenSource();

				Task.Factory.StartNew(() => Run(cancelToken.Token, container.Resolve<TaskComponent>()), cancelToken.Token);
				Task.Factory.StartNew(() => SlideShow(cancelToken.Token, container.Resolve<TaskComponent>()), cancelToken.Token);
				Console.ReadLine();
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

		private static void SlideShow(CancellationToken cancelTokenToken, TaskComponent resolve)
		{
			throw new NotImplementedException();
		}


		private static void Run(CancellationToken cancellationToken, TaskComponent taskComponent)
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

			//container.Resolve<TelegtamAPIModule>().Configure(container);
			//container.Resolve<TelegramNotifyWcfServiceModule>().Configure(container);

			//DependencyFactory.Container = container;
		}
	}
}
