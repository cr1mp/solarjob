using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Serilog;

namespace WsClient.PL
{
	internal class ApplicationBuilder:IApplicationBuilder
	{
		private readonly IApplicationEnviroment _applicationEnviroment;
		private List<Action<IUnityContainer>> _configureContainerDelegates;

		public ApplicationBuilder()
		{
			_applicationEnviroment = new ApplicationEnviroment();
			_configureContainerDelegates = new List<Action<IUnityContainer>>();
		}

		public IApplicationBuilder ConfigureMappers()
		{
			throw new NotImplementedException();
		}

		public IApplicationBuilder ConfigireContainer(Action<IUnityContainer> configireContainer)
		{
			throw new NotImplementedException();
		}

		public IApplicationBuilder ConfigureLogging(Action<ILogger> configureLogging)
		{
			throw new NotImplementedException();
		}

		public IApplication Build()
		{
			return new Application();
		}
	}
}
