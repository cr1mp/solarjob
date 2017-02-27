using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Attributes
{
	public class TaskNameAttribute:Attribute
	{
		public TaskNameAttribute(string name,params int[] versions)
		{
			Name = name;
			Version = versions;
		}

		public int[] Version { get; set; }

		public string Name { get; set; }
	}
}
