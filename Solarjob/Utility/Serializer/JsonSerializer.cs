using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Serializer
{
	public class JsonSerializer:ISerializer
	{
		public string Serialize(object o)
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(o);
		}
	}
}
