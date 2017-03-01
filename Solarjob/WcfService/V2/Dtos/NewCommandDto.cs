using System;
using System.Runtime.Serialization;

namespace WcfServer.V2.Dtos
{
	[DataContract]
	public class NewCommandDto
	{
		[DataMember]
		public DateTime StartTime { get; set; }
	}
}