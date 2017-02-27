using System;
using DAL.Abstraction;

namespace Domain.Models
{
	public class Entity:IEntity<Guid>
	{
		protected Entity()
		{
			
		}

		public Guid Id { get; set; }

	}
}