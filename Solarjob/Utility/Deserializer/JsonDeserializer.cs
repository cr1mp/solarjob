namespace Utility.Deserializer
{
	public class JsonDeserializer:IDeserializer
	{
		public T Deserialize<T>(string s)
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
		}
	}
}