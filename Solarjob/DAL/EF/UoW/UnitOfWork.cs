using DAL.Abstraction;
using DAL.Abstraction.UoW;
using DAL.EF.ContextProvider;

namespace DAL.EF.UoW
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDbContextProvider _contextProvider;

		public UnitOfWork(IDbContextProvider contextProvider )
		{
			_contextProvider = contextProvider;
		}

		public virtual void Commit()
		{
			_contextProvider.CurrentDbContext.SaveChanges();
		}

		public void Dispose()
		{
			_contextProvider.CurrentDbContext.Dispose();
			_contextProvider.CurrentDbContext = null;
		}
	}
}
