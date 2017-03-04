using WsClient.PL;

namespace WsClient
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		private static void Main(params string[] param)
		{
			var application = new ApplicationBuilder()
				.ConfigireContainer(container=>)
				//.UseParams(param)
				.Build();


			application.Run();
		}
	}
}