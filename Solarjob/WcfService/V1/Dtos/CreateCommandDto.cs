﻿using System;
using System.Runtime.Serialization;

namespace WcfServer.V1.Dtos
{
	[DataContract]
	public class SendMessageCommandDto
	{
		[DataMember]
		public DateTime StartTime { get; set; }

		[DataMember]
		public string Address { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public string Theme { get; set; }

		[DataMember]
		public string TaskName { get; set; }
	}
}