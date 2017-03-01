using System.ServiceModel;
using WcfServer.V2.Dtos;

namespace WcfServer.V2
{
	[ServiceContract]
	public interface ICommandService : V1.ICommandService
	{
		[OperationContract(Name = "AddNewTask")]
		void AddTask(NewCommandDto command);
	}
}
