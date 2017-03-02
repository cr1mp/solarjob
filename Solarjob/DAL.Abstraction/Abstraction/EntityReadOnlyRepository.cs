using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Abstraction
{
    public abstract class EntityReadOnlyRepository<TBusinessEntity, TKey> : IReadOnlyRepository<TBusinessEntity>
		where TBusinessEntity : IEntity<TKey>
	{
		/// <summary>
		/// Получить сущьность по ид или NullReferenceException.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual TBusinessEntity GetById(TKey id)
		{
			var result = GetByIdOrNull(id);
			if (result==null)
			{
				throw new NullReferenceException();
			}
			return result;
		}

		/// <summary>
		/// Получить сущьность по ид или NULL.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual TBusinessEntity GetByIdOrNull(TKey id)
		{
			return All.FirstOrDefault(x => x.Id.Equals(id));
		}

		protected abstract IQueryable<TBusinessEntity> All { get; }

		#region IReadOnlyRepository

		public Type ElementType => All.ElementType;

		public Expression Expression => All.Expression;

		public IQueryProvider Provider => All.Provider;
		public IEnumerator<TBusinessEntity> GetEnumerator() => All.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => All.GetEnumerator();

		#endregion
	}
}
