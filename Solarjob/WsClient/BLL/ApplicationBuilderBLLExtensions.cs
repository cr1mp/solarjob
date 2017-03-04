using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using WsClient.BLL.Infrastructure;
using WsClient.PL.Abstraction;

namespace WsClient.BLL
{
	public static class ApplicationBuilderBLLExtensions
	{

		public static IApplicationBuilder ConfigureBLL(this IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.ConfigireContainer(container =>
			{
				container.RegisterAllTypesForOpenGeneric(typeof(ICommandHandler<>), AppDomain.CurrentDomain.GetAssemblies());
				container.RegisterType<ICommandProcessor, UnityCommandProcessor>();
			});

			return applicationBuilder;
		}

		public static void RegisterAllTypesForOpenGeneric(this IUnityContainer container, Type genericType, Assembly[] assemblies)
		{
			var handlerRegistrations =
				from assembly in assemblies
				from implementation in assembly.GetExportedTypes()
				where !implementation.IsAbstract
				where !implementation.ContainsGenericParameters
				let services =
				from iface in implementation.GetInterfaces()
				where iface.IsGenericType
				where iface.GetGenericTypeDefinition() == genericType
				select iface
				from service in services
				select new { service, implementation };

			foreach (var registration in handlerRegistrations)
			{
				container.RegisterType(registration.service,
					registration.implementation);
			}
		}

	}
}
