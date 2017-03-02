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
		public static int GetStartVersion(this object o)
		{
			var t = o.GetType();

			var versionAttribute = (TaskVersionAttribute)Attribute.GetCustomAttribute(t, typeof(TaskVersionAttribute));

			if (versionAttribute == null)
			{
				throw new Exception("The attribute was not found.");
			}
			return versionAttribute.StartVersion;
		}

		public static string GetTaskName(this object o)
		{
			var t = o.GetType();

			var versionAttribute = (TaskNameAttribute)Attribute.GetCustomAttribute(t, typeof(TaskNameAttribute));

			if (versionAttribute == null)
			{
				throw new Exception("The attribute was not found.");
			}
			return versionAttribute.Name;
		}

		public static Type GetTaskType(string s)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			return assemblies.SelectMany(x => x.GetExportedTypes())
				      .Where(x=>x.GetCustomAttributes().Any(a=>a is TaskNameAttribute))
					  .First(x=>(x.GetCustomAttributes() as TaskNameAttribute).Name==s);
		}
	}
}
