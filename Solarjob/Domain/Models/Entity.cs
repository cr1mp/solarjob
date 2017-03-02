using System;
using DAL.Abstraction;

namespace Domain.Models
{
	public class Entity:IEntity<Guid>
	{
		protected Entity()
		{
			Id=Guid.NewGuid();
		}

		public Guid Id { get; set; }

	}
}