using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using JetBrains.Annotations;

namespace PluginApi.Model
{
	public abstract class GameSystem : IEquatable<GameSystem>
	{
		protected GameSystem(string systemLabel, string fileExtension)
		{
			Name = systemLabel;
			FileExtension = string.Format("{0}", fileExtension);
			FilePattern = string.Format("*.{0}", fileExtension);
			Cards = new ObservableCollection<Control>();
		}

		[NotNull]
		public string Name { get; private set; }

		[NotNull]
		public string FileExtension { get; private set; }

		[NotNull]
		public string FilePattern { get; private set; }

		public bool IsReadOnly { get; set; }

		[NotNull]
		public virtual Control EditorDisplay
		{
			get { return new Control(); }
		}

		[NotNull]
		public ObservableCollection<Control> Cards { get; private set; }

		[NotNull]
		public Character LoadCharacter([NotNull] string fileName)
		{
			return Parse(new CachedFile(new FileInfo(fileName)));
		}

		public abstract Character Parse(IDataFile characterData);

		public bool Equals(GameSystem other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other.FileExtension, FileExtension) && GetType() == other.GetType();
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as GameSystem);
		}

		public override int GetHashCode()
		{
			return FileExtension.GetHashCode();
		}

		public static bool operator ==(GameSystem left, GameSystem right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(GameSystem left, GameSystem right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return string.Format("{0} System ({1})", Name, FilePattern);
		}
	}
}
