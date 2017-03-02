using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Domain.Models;

namespace DAL.EF.Context
{
	internal class TaskerContext:DbContext
	{

		public TaskerContext()
		{
			
		}

		public DbSet<Task> Tasks { get; set; }

		/*protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Task>().HasKey(t=>t.Id);
		}*/

		public void FixEfProviderServicesProblem()
		{
			var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}
	}
}
