namespace DAL.Abstraction
{
    public interface IEntity<TKey>
    {
		TKey Id { get; set; }
    }
}
