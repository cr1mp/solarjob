using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Abstraction;

namespace BLL.Test
{
	class InMemoryRepository <TEntity>: EntityRepository<TEntity, Guid> where TEntity : IEntity<Guid>
	{
		List<TEntity> memory;

		public InMemoryRepository()
		{
			memory = new List<TEntity>();
		}

		protected override IQueryable<TEntity> All => memory.AsQueryable();
		public override void Add(TEntity entity)
		{
			memory.Add(entity);
		}

		public override void Delete(TEntity entity)
		{
			memory.Remove(entity);
		}
	}
}
