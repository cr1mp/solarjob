using Domain.Attributes;

namespace Domain.Models.Params
{
	[TaskType(type: "CreateFile"),TaskStartClientVersion(1)]
	public class CreateFile
	{
		public int Delay { get; set; } = 10000;

		public string Name { get; set; }
	}
}