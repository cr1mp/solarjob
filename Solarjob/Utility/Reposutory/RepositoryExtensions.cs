using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstraction;
using DAL.Abstraction.Repositories;

namespace Utility.Reposutory
{
	public static class RepositoryExtensions
	{
		public static IRepository<T> AddRange<T>(this IRepository<T> repository, IEnumerable<T> collection)
		{
			foreach (var item in collection)
			{
				repository.Add(item);
			}
			return repository;
		}
	}
}
