using System;

namespace BLL.Commands.ChangeState
{
	public class SetStateNewCommand
	{
		public SetStateNewCommand(Guid taskId)
		{
			TaskId = taskId;
		}

		public Guid TaskId { get; set; }
	}
}
