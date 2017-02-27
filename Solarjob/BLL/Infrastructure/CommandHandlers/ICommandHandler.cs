using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.CommandHandlers
{
	public interface ICommandHandler<in TCommand>
	{
		void Handle(TCommand command);
	}
}
