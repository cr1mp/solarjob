using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Container;
using Microsoft.Practices.Unity;
using Utility.Serializer;

namespace BLL.Container
{
	public static class BLLInitialization
	{
		public static IUnityContainer BLLInitialize(this IUnityContainer container)
		{
			container.RegisterType<ISerializer,JsonSerializer>();
			container.DALInitialize();

			return container;
		}
	}
}
