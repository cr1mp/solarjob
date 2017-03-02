using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dtos;
using WcfServer.V1.Dtos;

namespace WcfServer.Startup
{
	internal class PLAutoMapperProfile: Profile
	{
		public PLAutoMapperProfile()
		{
			CreateMap<TaskDto,CommandDto > ();
		}
	}
}
