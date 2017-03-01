using Microsoft.Practices.Unity;
using Unity.Wcf;

namespace WcfServer
{
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
			
             container
                .RegisterType<V1.ICommandService, V1.CommandService>()
                .RegisterType<V2.ICommandService, V2.CommandService>()
                //.RegisterType<DataContext>(new HierarchicalLifetimeManager())
				;
        }
    }    
}