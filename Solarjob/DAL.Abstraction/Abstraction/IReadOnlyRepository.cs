using System.Linq;

namespace DAL.Abstraction
{
    public interface IReadOnlyRepository<out TBusinessEntity> :IQueryable<TBusinessEntity>
    {
    }
}
