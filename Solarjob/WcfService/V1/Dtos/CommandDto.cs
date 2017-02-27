using System;
using System.Runtime.Serialization;

namespace WcfService.V1
{
	[DataContract]
	public class CommandDto
	{

		[DataMember]
		public Guid Id { get; set; }

		[DataMember]
		public string StringValue { get; set; }
	}
}