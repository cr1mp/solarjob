using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.Dtos;
using BLL.Infrastructure.QueryHandlers;
using BLL.Queries;
using DAL.Abstraction;
using Domain.Models;

namespace BLL.Components
{
	public class TaskQueryHandlers : IQueryHandler<GetNewTaskQuery, TaskDto>
	{
		private readonly IRepository<Job> _taskRepository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkFactory _factory;

		public TaskQueryHandlers(IRepository<Job> taskRepository, IMapper mapper, IUnitOfWorkFactory factory)
		{
			_taskRepository = taskRepository;
			_mapper = mapper;
			_factory = factory;
		}

		public TaskDto Handle(GetNewTaskQuery query)
		{
			using (_factory.Create())
			{
				return _mapper.Map<TaskDto>(_taskRepository.OrderBy(x => x.StartTime)
					.FirstOrDefault(query.Expression));
			}
		}
	}
}
