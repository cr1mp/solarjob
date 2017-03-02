using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Deserializer
{
	public interface IDeserializer
	{
		T Deserialize<T>(string s);

		object Deserialize(Type t, string s);
	}
}
