using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfServer.Results
{
	/// <summary>
	/// Результат выполнения.
	/// </summary>
	[DataContract]
	public class Result
	{
		[DataMember]
		public bool IsSuccess => ErrorCode == 0 ;
		[DataMember]
		public uint ErrorCode { get; set; } = 0;
		[DataMember]
		public virtual string ErrorMessage => ErrorDictionary.ErrorsDescriction[ErrorCode];
	}

	/// <summary>
	/// todo Для примера. Переделать.
	/// </summary>
	internal static class ErrorDictionary
	{
		public static readonly Dictionary<uint, string> ErrorsDescriction = new Dictionary<uint, string>()
		{
			{ 1,"Описание первой ошибки"},
			{ 2,"Описание первой ошибки"},

		};
	}
}
