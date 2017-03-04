using System;
using System.Threading;
using System.Threading.Tasks;
using WsClient.BLL;
using WsClient.PL.Abstraction;

namespace WsClient.PL.ConsoleClient
{
	public class ConsoleClient: IClient
	{
		private readonly TaskComponent _taskComponent;
		private readonly Slider _slider;

		public ConsoleClient(TaskComponent taskComponent,Slider slider)
		{
			_taskComponent = taskComponent;
			_slider = slider;
		}

		public void Start()
		{
			var cancelToken = new CancellationTokenSource();

			Task.Factory.StartNew(() => _taskComponent.Start(), cancelToken.Token);
			Task.Factory.StartNew(() => _slider.SlideShow(), cancelToken.Token);
			Console.ReadLine();
			cancelToken.Cancel();
		}

		public void Dispose()
		{
			_taskComponent.Stop();
			_slider.Stop();
		}
	}
}