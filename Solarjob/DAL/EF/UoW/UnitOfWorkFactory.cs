using DAL.Abstraction;
using DAL.EF.Context;
using DAL.EF.ContextProvider;

namespace DAL.EF.UoW
{
	public class UnitOfWorkFactory : IUnitOfWorkFactory
	{
		private readonly IDbContextProvider _dbContextProvider;

		public UnitOfWorkFactory(IDbContextProvider dbContextProvider)
		{
			_dbContextProvider = dbContextProvider;
		}

		public IUnitOfWork Create()
		{
			_dbContextProvider.CurrentDbContext = new TaskerContext();
			return new UnitOfWork(_dbContextProvider);
		}
	}
}
