using System;

namespace BLL.Commands.ChangeState
{
	public class SetStateFailCommand
	{
		public SetStateFailCommand(Guid taskId)
		{
			TaskId = taskId;
		}

		public Guid TaskId { get; set; }
	}
}
