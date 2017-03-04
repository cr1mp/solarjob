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
		private bool _isSuccess;
		private uint _errorCode;
		private string _errorMessage;


		[DataMember]
		//public bool IsSuccess => ErrorCode == 0 ;
		public bool IsSuccess
		{
			get
			{
				return _errorCode == 0;
			}
			set
			{
				_isSuccess = value;
			}
		}

		[DataMember]
		public uint ErrorCode
		{
			get { return _errorCode; }
			set { _errorCode = value; }
		}

		[DataMember]
		//public virtual string ErrorMessage => ;
		public string ErrorMessage
		{
			get { return ErrorDictionary.ErrorsDescriction[_errorCode]; }
			set { _errorMessage = value; }
		}
	}

	/// <summary>
	/// todo Для примера. Переделать.
	/// </summary>
	internal static class ErrorDictionary
	{
		public static readonly Dictionary<uint, string> ErrorsDescriction = new Dictionary<uint, string>()
		{
			{0,string.Empty },
			{ 1,"Описание первой ошибки"},
			{ 2,"Описание первой ошибки"},

		};
	}
}
