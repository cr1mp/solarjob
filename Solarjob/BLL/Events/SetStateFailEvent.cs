using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Commands.ChangeState;
using Microsoft.Practices.Prism.PubSubEvents;

namespace BLL.Events
{
	internal class SetStateFailEvent : PubSubEvent<SetStateFailCommand>
	{
	}
}
