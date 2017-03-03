using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Attributes;

namespace Domain.Models.Params
{
	[TaskType("NewTask"),TaskStartClientVersion(2)]
	public class NewTask
	{
		public string Param { get; set; }
	}
}
