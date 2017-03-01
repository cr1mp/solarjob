using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Infrastructure.QueryHandlers;
using BLL.Queries;
using DAL.Abstraction;
using Domain.Models;

namespace BLL.Components
{
	public class TaskQueryHandlers:IQueryHandler<GetNewTaskQuery,Task>
	{
		private readonly IRepository<Task> _taskRepository;

		public TaskQueryHandlers(IRepository<Task> taskRepository )
		{
			_taskRepository = taskRepository;
		}

		public Task Handle(GetNewTaskQuery query)
		{
			return _taskRepository.OrderBy(x=>x.StartTime)
								  .FirstOrDefault(query.Expression);
		}
	}
}
