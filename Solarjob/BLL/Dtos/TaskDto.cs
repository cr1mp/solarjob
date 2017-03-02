using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
	public class TaskDto
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Params { get; set; }
	}
}
