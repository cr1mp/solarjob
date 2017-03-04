using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using WsClient.PL.Abstraction;

namespace WsClient.PL.WindowsServiceClient
{
	public static class ApplicationBuilderWindowsServiceClientExtensions
	{
		public static IApplicationBuilder UseWindowsCervice(this IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.ConfigireContainer(container => container.RegisterType<IClient, WindowsServiceClient>(new ContainerControlledLifetimeManager()));

			return applicationBuilder;
		}
	}
}
