using JetBrains.Annotations;
using WotcOnlineDataRepository.WotcServices.Content;

namespace WotcOnlineDataRepository
{
	public class ContentId
	{
		public enum ContentType
		{
			Character = 0,
		}

		private readonly int _contentId;
		private readonly ContentType _typeId;

		internal ContentId([NotNull] ContentIdentifier identifier)
		{
			_contentId = identifier.ContentID.Value;
			_typeId = identifier.TypeID.To<ContentType>();
		}

		public override string ToString()
		{
			return string.Format("{1} {0}", _contentId, _typeId);
		}
	}
}