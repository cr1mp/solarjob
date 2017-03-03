using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.PubSubEvents;

namespace BLL.Infrastructure.EventHandlers
{
	public static class EventAggregatorExtensions
	{
		/// <summary>
		/// Метод подписки на событие с использованием адаптера.
		/// </summary>
		public static SubscriptionToken Subscribe<TPayload>(this PubSubEvent<TPayload> targetEvent, IEventHandler<TPayload> handler)
		{
			return targetEvent.Subscribe(handler.Handle, true); //true для того, чтобы handler не умирал
		}

		/// <summary>
		/// Метод подписки на событие с использованием адаптера.
		/// </summary>
		public static SubscriptionToken Subscribe<TPayload>(this PubSubEvent<TPayload> targetEvent, IEventHandler<TPayload> handler, ThreadOption threadOption)
		{
			return targetEvent.Subscribe(handler.Handle, threadOption, true); //true для того, чтобы handler не умирал
		}
	}
}
