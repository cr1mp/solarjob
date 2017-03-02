using System.Data.Entity;

namespace DAL.EF.ContextProvider
{
	/// <summary>
	/// Провайдер сессии EntityFramework.
	/// </summary>
	public interface IDbContextProvider
	{
		///<summary>
		/// Текущая сессия.
		///</summary>
		DbContext CurrentDbContext { get; set; }
	}
}
