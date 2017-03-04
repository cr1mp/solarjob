using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WsClient.PL.Abstraction;

namespace WsClient.PL.ConsoleClient
{
	public static class ApplicationBuilderConsoleClientExtensions
	{
		public static IApplicationBuilder UseConsole(this IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.ConfigireContainer(container => container.RegisterType<IClient,ConsoleClient>(new ContainerControlledLifetimeManager()));

			return applicationBuilder;
		}
	}
}
