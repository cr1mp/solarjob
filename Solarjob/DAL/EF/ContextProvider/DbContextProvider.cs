using System;
using System.Data.Entity;

namespace DAL.EF.ContextProvider
{
	public class DbContextProvider: IDbContextProvider
	{
		[ThreadStatic]
		private static DbContext _dbContext;

		public DbContext CurrentDbContext {
			get
			{
				if (_dbContext != null)
					return _dbContext;
				throw new InvalidOperationException(
					"Сессия не открыта. Логика доступа к базе данных не может быть использована." +
					"Пожалуйста, откройте сеанс явно через UnitOfWorkFactory.");
			}
			set
			{
				if(value!=null && _dbContext!=null)
					throw new InvalidOperationException(
						"Текущая сессия не закрыта. " +
						"Пожалуйста, закройте текущий сеанс явно через UnitOfWork.Commit() или UnitOfWork.Dispose().");
				_dbContext = value;
				
			} }
	}
}
