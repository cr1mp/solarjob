using Domain.Attributes;

namespace Domain.Models.Params
{
	[TaskType("SendMessage"),TaskStartClientVersion(1)]
	public class SendMessage
	{
		public string Theme { get; set; }
		public string Message { get; set; }
		public string Address { get; set; }
	}
}