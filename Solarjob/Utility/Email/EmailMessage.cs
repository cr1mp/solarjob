namespace Utility.Email
{
	public class EmailMessage
	{
		

		public EmailMessage(string receiver,string header,string body)
		{
			Receiver = receiver;
			Header = header;
			Body = body;
		}

		public string Receiver { get; set; }
		public string Header { get; set; }
		public string Body { get; set; }
	}
}