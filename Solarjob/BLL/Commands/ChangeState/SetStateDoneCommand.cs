using System;

namespace BLL.Commands.ChangeState
{
	public class SetStateDoneCommand
	{
		public SetStateDoneCommand(Guid taskId)
		{
			TaskId = taskId;
		}
		public Guid TaskId { get; set; }
	}
}
