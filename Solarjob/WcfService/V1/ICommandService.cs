using System;
using System.ServiceModel;
using WcfServer.Results;
using WcfServer.V1.Dtos;

namespace WcfServer.V1
{
	[ServiceContract]
	public interface ICommandService
	{
		[OperationContract]
		Result<CommandDto> GetCommand(string clientName);

		[OperationContract]
		Result Done(Guid commandId);

		[OperationContract]
		Result Fail(Guid commandId);

		[OperationContract(Name = "AddSendMessageTask")]
		Result AddTask(SendMessageCommandDto command);

		[OperationContract(Name = "AddCreateFileTask")]
		Result AddTask(CreateFileCommandDto command);

		[OperationContract]
		Result Ping();
	}
}
