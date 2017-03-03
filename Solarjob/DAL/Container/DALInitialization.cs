using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstraction;
using DAL.Abstraction.Repositories;
using DAL.Abstraction.UoW;
using DAL.EF.ContextProvider;
using DAL.EF.Repository;
using DAL.EF.UoW;
using Microsoft.Practices.Unity;

namespace DAL.Container
{
	public static class DALInitialization
	{
		public static IUnityContainer DALInitialize(this IUnityContainer container)
		{

			container.RegisterType<IDbContextProvider, DbContextProvider>();
			container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>();

			container.RegisterType(typeof(IRepository<> ),typeof(LazyContextRepository<>));
			container.RegisterType(typeof(IReadOnlyRepository<> ),typeof(LazyContextRepository<>));
			container.RegisterType(typeof(EntityRepository<,> ),typeof(EntityLazyContextRepository<,>));

			return container;
		}
	}
}
