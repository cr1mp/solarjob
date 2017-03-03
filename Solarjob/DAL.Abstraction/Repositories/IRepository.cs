namespace DAL.Abstraction.Repositories
{
	public interface IRepository<TBusinessEntity> : IReadOnlyRepository<TBusinessEntity>
	{

		void Add(TBusinessEntity entity);

		void Delete(TBusinessEntity entity);
	}
}
