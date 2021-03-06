using System;
using System.Runtime.Serialization;

namespace WcfServer.V1.Dtos
{
	[DataContract]
	public class CommandDto
	{

		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Type { get; set; }

		[DataMember]
		public string Params { get; set; }
	}
}