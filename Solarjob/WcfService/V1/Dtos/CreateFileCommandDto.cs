using System;
using System.Runtime.Serialization;

namespace WcfServer.V1.Dtos
{
	[DataContract]
	public class CreateFileCommandDto
	{
		[DataMember]
		public DateTime StartTime { get; set; }

		[DataMember]
		public string FileName { get; set; }

		[DataMember]
		public string TaskName { get; set; }
	}
}