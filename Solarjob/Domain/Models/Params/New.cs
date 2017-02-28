using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Attributes;

namespace Domain.Models.Params
{
	[TaskName("New"),TaskVersion(2)]
	public class New
	{
		public string Param { get; set; }
	}
}
