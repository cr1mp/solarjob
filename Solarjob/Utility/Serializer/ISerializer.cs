using Domain.Enums;

namespace Utility.Serializer
{
	public interface ISerializer
	{
		string Serialize(object o);
		MimeType GetMimeType();
	}
}