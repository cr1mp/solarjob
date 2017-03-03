namespace DAL.Abstraction.Entities
{
    public interface IEntity<TKey>
    {
		TKey Id { get; set; }
    }
}
