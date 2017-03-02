using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WsClient.BLL;

namespace WsClient
{
	public partial class TaskerWsClient : ServiceBase
	{
		private readonly TaskComponent _taskComponent;

		public TaskerWsClient(TaskComponent taskComponent)
		{
			
			InitializeComponent();
			_taskComponent = taskComponent;
		}

		protected override void OnStart(string[] args)
		{
			_taskComponent.Start();
		}

		protected override void OnStop()
		{
			_taskComponent.Stop();
		}
	}
}
