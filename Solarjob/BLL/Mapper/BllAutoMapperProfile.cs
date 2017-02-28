using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Commands.Create;
using Domain.Models.Params;

namespace BLL.Mapper
{
	public class BllAutoMapperProfile : Profile
	{
		public BllAutoMapperProfile()
		{
			CreateMap<SendMessageTaskCreateCommand, SendMessage>();
			CreateMap<CreateFileTaskCreateCommand, CreateFile>();
			CreateMap<NewTaskCreateCommand, New> ();
		}
	}
}
