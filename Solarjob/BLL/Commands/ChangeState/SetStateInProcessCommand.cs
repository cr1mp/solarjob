using System;

namespace BLL.Commands.ChangeState
{
	public class SetStateInProcessCommand
	{
		public SetStateInProcessCommand(Guid taskId, string executor)
		{
			TaskId = taskId;
			Executor = executor;
		}

		public Guid TaskId { get; set; }
		public string Executor { get; set; }
	}
}