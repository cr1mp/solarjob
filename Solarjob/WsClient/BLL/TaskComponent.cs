using System;
using Utility;
using WsClient.WcfServer.V1;


namespace WsClient.BLL
{
	public class TaskComponent
	{
		private readonly ICommandService _server;
		private readonly DeserializerFactory _deserializerFactory;
		private bool _isStopped;

		public TaskComponent(ICommandService server, DeserializerFactory deserializerFactory)
		{
			_server = server;
			_deserializerFactory = deserializerFactory;
		}

		public void Start()
		{
			while (!_isStopped)
			{
				if (!_server.Ping().IsSuccess)
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
					Type t = AttributeHelper.GetTaskType(command.Name);

					var task = _deserializerFactory.CreateJsonDeserializer().Deserialize(t, command.Params);

					Handle();

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

		void Handle()
		{
			
		}
	}
}
