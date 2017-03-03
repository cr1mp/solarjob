using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Abstraction;
using DAL.Abstraction.Entities;
using DAL.Abstraction.Repositories;

namespace BLL.Test
{
	class InMemoryRepository <TEntity>: EntityRepository<TEntity, Guid> where TEntity : IEntity<Guid>
	{
		private readonly List<TEntity> _memory;

		public InMemoryRepository()
		{
			_memory = new List<TEntity>();
		}

		protected override IQueryable<TEntity> All => _memory.AsQueryable();
		public override void Add(TEntity entity)
		{
			_memory.Add(entity);
		}

		public override void Delete(TEntity entity)
		{
			_memory.Remove(entity);
		}

		public void Clear()
		{
			_memory.Clear();
		}
	}
}
