using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Practices.Unity;
using Serilog;
using WsClient.PL.Abstraction;
using WsClient.PL.App;

namespace WsClient.PL.AppBuilder
{
	internal class ApplicationBuilder:IApplicationBuilder
	{
		private readonly IApplicationEnviroment _applicationEnviroment;
		private readonly List<Action<IUnityContainer>> _configureContainerDelegates;
		private readonly List<Action<LoggerConfiguration>> _configureLoggerDelegates;
		private List<Action<IMapperConfigurationExpression>> _configureMapperDelegates;
		private bool _applicationBuilt;

		public ApplicationBuilder()
		{
			_applicationEnviroment = new ApplicationEnviroment();
			_configureContainerDelegates = new List<Action<IUnityContainer>>();
			_configureLoggerDelegates = new List<Action<LoggerConfiguration>>();
			_configureMapperDelegates = new List<Action<IMapperConfigurationExpression>>();
		}

		public IApplicationBuilder ConfigireContainer(Action<IUnityContainer> configireContainer)
		{
			_configureContainerDelegates.Add(configireContainer);
			return this;
		}

		public IApplicationBuilder ConfigureLogging(Action<LoggerConfiguration> configureLogging)
		{
			_configureLoggerDelegates.Add(configureLogging);
			return this;
		}

		public IApplicationBuilder ConfigureMappers(Action<IMapperConfigurationExpression> configureMappers)
		{
			_configureMapperDelegates.Add(configureMappers);
			return this;
		}

		public IApplication Build()
		{
			if (_applicationBuilt)
			{
				throw new InvalidOperationException();
			}
			_applicationBuilt = true;

			IUnityContainer container = BuildContainer();

			var app = new Application(container);

			app.Initialize();

			return app;
		}

		private IUnityContainer BuildContainer()
		{
			var container = new UnityContainer();

			var loggerConfiguration = new LoggerConfiguration();

			foreach (var configureLoggerDelegate in _configureLoggerDelegates)
			{
				configureLoggerDelegate(loggerConfiguration);
			}

			var logger = loggerConfiguration.CreateLogger();

			container.RegisterInstance<ILogger>(logger);

			var mapper =new MapperConfiguration(cfg =>
			{
				_configureMapperDelegates.ForEach(x=>x(cfg));
			}).CreateMapper();

			container.RegisterInstance<IMapper>(mapper);

			foreach (var configureContainerDelegate in _configureContainerDelegates)
			{
				configureContainerDelegate(container);
			}

			return container;
		}
	}
}
