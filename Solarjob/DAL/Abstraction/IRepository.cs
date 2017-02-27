namespace DAL.Abstraction
{
	public interface IRepository<TBusinessEntity> : IReadOnlyRepository<TBusinessEntity>
	{

		void Add(TBusinessEntity entity);

		void Delete(TBusinessEntity entity);
	}
}
