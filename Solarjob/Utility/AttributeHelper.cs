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

			var handlerRegistrations =
				from assembly in assemblies

				from rType in assembly.GetExportedTypes()
				where !rType.IsAbstract
				where !rType.IsGenericType

				let attributes =
				from attr in rType.GetCustomAttributes(typeof(TaskTypeAttribute))
				//where attr is TaskTypeAttribute
				//where (attr as TaskTypeAttribute).Type == s
				select attr
				from attribute in attributes
				select new { attribute, rType };

			foreach (var handlerRegistration in handlerRegistrations)
			{
				if ((handlerRegistration.attribute as TaskTypeAttribute).Type == s)
				{
					return handlerRegistration.rType;
				}
			}

			throw new InvalidOperationException();
		}
	}
}
