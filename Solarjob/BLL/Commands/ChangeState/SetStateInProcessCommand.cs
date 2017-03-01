using System;

namespace BLL.Commands.ChangeState
{
	public class SetStateInProcessCommand
	{
		public SetStateInProcessCommand(Guid taskId)
		{
			TaskId = taskId;
		}

		public Guid TaskId { get; set; }
	}
}