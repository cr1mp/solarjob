using System;
using System.ServiceModel;
using WcfServer.V1.Dtos;

namespace WcfServer.V1
{
	[ServiceContract]
	public interface ICommandService
	{
		[OperationContract]
		CommandDto GetCommand();

		[OperationContract]
		void Done(Guid commandId);

		[OperationContract]
		void Fail(Guid commandId);

		[OperationContract(Name = "AddSendMessageTask")]
		void AddTask(SendMessageCommandDto command);

		[OperationContract(Name = "AddCreateFileTask")]
		void AddTask(CreateFileCommandDto command);
	}
}
