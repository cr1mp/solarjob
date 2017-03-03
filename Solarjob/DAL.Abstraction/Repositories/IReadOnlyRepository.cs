using System.Linq;

namespace DAL.Abstraction.Repositories
{
    public interface IReadOnlyRepository<out TBusinessEntity> :IQueryable<TBusinessEntity>
    {
    }
}
