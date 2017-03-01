namespace BLL.Infrastructure.QueryHandlers
{
	public interface IQueryHandler<in TQuery, out TResult>
	{
		TResult Handle(TQuery query);
	}
}