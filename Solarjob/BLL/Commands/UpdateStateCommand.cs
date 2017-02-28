using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Commands
{
	public class DoneCommand
	{
		public DoneCommand(Guid taskId)
		{
			TaskId = taskId;
		}
		public Guid TaskId { get; set; }
	}
}
