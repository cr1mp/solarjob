using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.EventHandlers
{
	/// <summary>
	/// Интерфейс класса, способного подписываться на события Prism.
	/// </summary>
	public interface IEventHandler<in TEventArgs>
	{
		void Handle(TEventArgs parameter);
	}
}
