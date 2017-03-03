using System;
using System.Linq.Expressions;
using Domain.Enums;
using Domain.Models;

namespace BLL.Queries
{
	public class GetNewTaskQuery
	{
		public GetNewTaskQuery(int clientVersion)
		{
			Expression = task =>(task.State==TaskState.New) && 
								(task.Version <= clientVersion) && 
								(task.StartTime<=DateTime.Now);
		}

		internal Expression<Func<Job, bool>> Expression { get; private set; }
	}
}