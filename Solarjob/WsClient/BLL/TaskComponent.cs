using System;
using Utility;
using WsClient.BLL.Infrastructure;
using WsClient.WcfServer.V1;


namespace WsClient.BLL
{
	public class TaskComponent
	{
		private readonly ICommandService _server;
		private readonly DeserializerFactory _deserializerFactory;
		private readonly ICommandProcessor _commandProcessor;
		private bool _isStopped;

		public TaskComponent(ICommandService server, DeserializerFactory deserializerFactory,
			ICommandProcessor commandProcessor)
		{
			_server = server;
			_deserializerFactory = deserializerFactory;
			_commandProcessor = commandProcessor;
		}

		public void Start()
		{
			while (!_isStopped)
			{
				try
				{
					if (!_server.Ping().IsSuccess)
					{
						continue;
					}
				}
				catch (Exception ex)
				{
					
					continue;
				}
				

				var result = _server.GetCommand(Environment.MachineName);

				if (result == null)
				{
					continue;
				}

				if (!result.IsSuccess)
				{
					continue;
				}

				var command = result.ResultObject;

				if (command == null)
				{
					continue;
				}

				try
				{
					Type t = AttributeHelper.GetTaskType(command.Type);

					var task = _deserializerFactory.CreateJsonDeserializer().Deserialize(t, command.Params);

					_commandProcessor.Process(task);

					_server.Done(command.Id);
				}
				catch (Exception ex)
				{
					_server.Fail(command.Id);
				}
			}
		}

		public void Stop()
		{
			_isStopped = true;
		}
	}
}
