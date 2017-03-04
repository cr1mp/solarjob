using System.ServiceProcess;
using WsClient.BLL;

namespace WsClient.PL.WindowsServiceClient
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
