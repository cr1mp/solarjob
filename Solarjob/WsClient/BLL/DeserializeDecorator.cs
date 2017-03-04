using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Deserializer;

namespace WsClient.BLL
{
	public class DeserializerFactory
	{
		public IDeserializer CreateXmlDeserializer()
		{
			return new XmlDeserializer();
		}

		public IDeserializer CreateJsonDeserializer()
		{
			return new JsonDeserializer();
		}
	}
}
