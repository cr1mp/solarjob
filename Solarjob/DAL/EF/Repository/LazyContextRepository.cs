using System;
using DAL.Abstraction;
using DAL.EF.ContextProvider;

namespace DAL.EF.Repository
{
	public class LazyContextRepository<TBusinessEntity> : EntityLazyContextRepository<TBusinessEntity,Guid>
		where TBusinessEntity : class, IEntity<Guid>
	{
		public LazyContextRepository(IDbContextProvider contextProvider) 
			: base(contextProvider)
		{
		}
	}

	public class EntityLazyContextRepository<TBusinessEntity,TKey> : EFRepository<TBusinessEntity, TKey>
		where TBusinessEntity : class, IEntity<TKey>
	{
		public EntityLazyContextRepository(IDbContextProvider contextProvider)
			: base(() => contextProvider.CurrentDbContext)
		{
		}
	}
}
