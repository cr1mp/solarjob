using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Attributes;

namespace Utility
{
	public static class AttributeHelper
	{
		public static int GetClientStartVersion(this object o)
		{
			var t = o.GetType();

			var versionAttribute = (TaskStartClientVersionAttribute)Attribute.GetCustomAttribute(t, typeof(TaskStartClientVersionAttribute));

			if (versionAttribute == null)
			{
				throw new Exception("The attribute was not found.");
			}
			return versionAttribute.StartClientVersion;
		}

		public static string GetTaskType(this object o)
		{
			var t = o.GetType();

			var versionAttribute = (TaskTypeAttribute)Attribute.GetCustomAttribute(t, typeof(TaskTypeAttribute));

			if (versionAttribute == null)
			{
				throw new Exception("The attribute was not found.");
			}
			return versionAttribute.Type;
		}

		public static Type GetTaskType(string s)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			return assemblies.SelectMany(x => x.GetExportedTypes())
				      .Where(x=>x.GetCustomAttributes().Any(a=>a is TaskTypeAttribute))
					  .First(x=>(x.GetCustomAttributes() as TaskTypeAttribute).Type==s);
		}
	}
}
