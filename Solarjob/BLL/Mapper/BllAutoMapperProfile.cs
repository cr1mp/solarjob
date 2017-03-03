using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BLL.Commands.Create;
using BLL.Dtos;
using Domain.Models;
using Domain.Models.Params;

namespace BLL.Mapper
{
	public class BllAutoMapperProfile : Profile
	{
		public BllAutoMapperProfile()
		{
			CreateMap<SendMessageTaskCreateCommand, SendMessage>();
			CreateMap<CreateFileTaskCreateCommand, CreateFile>();
			CreateMap<NewTaskCreateCommand, NewTask> ();
			CreateMap<Job, TaskDto> ();
		}
	}
}
