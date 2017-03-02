using System;

namespace Utility.Deserializer
{
	public class JsonDeserializer:IDeserializer
	{
		public object Deserialize(Type t, string s)
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject(s,t);
		}

		public T Deserialize<T>(string s)
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
		}
	}
}