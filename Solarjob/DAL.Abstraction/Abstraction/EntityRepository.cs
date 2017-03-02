namespace DAL.Abstraction
{
	public abstract class EntityRepository<TBusinessEntity, TKey> : EntityReadOnlyRepository<TBusinessEntity, TKey>, IRepository<TBusinessEntity>
		where TBusinessEntity : IEntity<TKey>
	{
		public virtual void Delete(TKey id)
		{
			Delete(GetById(id));
		}

		public abstract void Add(TBusinessEntity entity);
		public abstract void Delete(TBusinessEntity entity);
	}
}
