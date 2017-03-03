using System;
using Domain.Enums;

namespace Utility.Serializer
{
	public class XmlSerializer : ISerializer
	{
		public MimeType GetMimeType() => MimeType.ApplicationXml;

		public string Serialize(object o)
		{
			throw new NotImplementedException();
		}
	}
}