using System;
using System.ServiceModel;

namespace WcfService.V1
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

		void AddCommand(SendMessageCommandDto createCommandDto);
		void AddCommand(CreateFileCommandDto createCommandDto);
	}
}
