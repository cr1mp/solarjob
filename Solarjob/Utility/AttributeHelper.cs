using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}
