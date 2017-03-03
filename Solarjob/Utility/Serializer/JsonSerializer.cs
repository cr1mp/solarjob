using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Utility.Serializer
{
	public class JsonSerializer:ISerializer
	{
		public MimeType GetMimeType() => MimeType.ApplicationJson;

		public string Serialize(object o) => Newtonsoft.Json.JsonConvert.SerializeObject(o);
	}
}
