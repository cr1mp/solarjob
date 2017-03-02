using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.Deserializer;
using WsClient.WcfServer.V1;


namespace WsClient.BLL
{
	public class TaskComponent
	{
		private readonly ICommandService _server;
		private readonly IDeserializer _deserializer;
		private bool _isRun;

		public TaskComponent(ICommandService server, IDeserializer deserializer)
		{
			_server = server;
			_deserializer = deserializer;
		}

		public void Start()
		{
			while (_isRun)
			{
				var command = _server.GetCommand();
				if (command == null)
				{
					continue;
				}

				try
				{
					Type t = AttributeHelper.GetTaskType(command.Name);

					var task = _deserializer.Deserialize(t, command.Params);

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
			_isRun = false;
		}

		void Handle()
		{
			
		}
	}
}
