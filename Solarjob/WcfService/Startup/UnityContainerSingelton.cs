using System;
using AutoMapper;
using BLL.Container;
using BLL.Mapper;
using Microsoft.Practices.Unity;

namespace WcfServer.Startup
{
	public sealed class UnityContainerSingelton 
    {
		private static readonly Lazy<IUnityContainer> _instance=
			new Lazy<IUnityContainer>(ConfigureContainer);

	    UnityContainerSingelton()
	    {
	    }

	    public static IUnityContainer Instance => _instance.Value;

	    private static IUnityContainer ConfigureContainer()
        {
			var container = new UnityContainer();

			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new BllAutoMapperProfile());
			});
			container.BLLInitialize()
                .RegisterType<V1.ICommandService, V1.CommandService>()
                .RegisterType<V2.ICommandService, V2.CommandService>()
				
				.RegisterInstance<IMapper>(config.CreateMapper(), new HierarchicalLifetimeManager())
				;

	        return container;
        }
    }    
}