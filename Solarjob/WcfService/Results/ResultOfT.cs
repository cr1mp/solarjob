using System.Runtime.Serialization;

namespace WcfServer.Results
{
	/// <summary>
	/// Результат выполнения с объектом ответа.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[DataContract]
	public class Result<T>:Result
	{
		internal Result(T resultObject)
		{
			ResultObject = resultObject;
		}

		[DataMember]
		public T ResultObject { get; set; }
	}
}
