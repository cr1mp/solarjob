using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.EventHandlers;
using BLL.Events;
using BLL.Infrastructure.EventHandlers;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;

namespace BLL
{
	public class BLLModule
	{
		private readonly IUnityContainer _container;
		private readonly IEventAggregator _eventAggregator;

		public BLLModule(IUnityContainer container,
			IEventAggregator eventAggregator)
		{
			_container = container;
			_eventAggregator = eventAggregator;

			Configure();
		}

		public void Configure()
		{
			_eventAggregator.GetEvent<SetStateFailEvent>().Subscribe(_container.Resolve<SetStateFailEventHandler>(), ThreadOption.BackgroundThread);

		}
	}
}
