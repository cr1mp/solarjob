using System;
using System.Data.Entity;
using System.Linq;
using DAL.Abstraction;

namespace DAL.EF.Repository
{
	public class EFRepository<TBusinessEntity,TKey> : EntityRepository<TBusinessEntity, TKey>
		where TBusinessEntity : class,IEntity<TKey>
	{

		private readonly DbContext _context;
		private readonly Func<DbContext> _getContext;


		public EFRepository(Func<DbContext> getContext)
		{
			_getContext = getContext;
		}

		public EFRepository(DbContext context)
		{
			if (context == null)
				throw new ArgumentException("context");

			_context = context;
		}

		protected virtual DbContext Context => _context ?? _getContext();

		protected virtual DbSet<TBusinessEntity> Table => Context.Set<TBusinessEntity>();


		protected override IQueryable<TBusinessEntity> All => Table;


		public override void Add(TBusinessEntity entity)
		{
			Table.Add(entity);
		}

		public override void Delete(TBusinessEntity entity)
		{
			Table.Remove(entity);
		}
	}
}
