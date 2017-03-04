using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Deserializer
{
	public class XmlDeserializer : IDeserializer
	{
		public T Deserialize<T>(string s)
		{
			throw new NotImplementedException();
		}

		public object Deserialize(Type t, string s)
		{
			throw new NotImplementedException();
		}
	}
}
