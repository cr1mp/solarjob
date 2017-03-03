using System.ServiceModel;
using WcfServer.Results;
using WcfServer.V2.Dtos;

namespace WcfServer.V2
{
	[ServiceContract]
	public interface ICommandService : V1.ICommandService
	{
		[OperationContract(Name = "AddNewTask")]
		Result AddTask(NewCommandDto command);
	}
}
