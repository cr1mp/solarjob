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
			new Bootstrapper().RunApplication(param);
		}
	}
}