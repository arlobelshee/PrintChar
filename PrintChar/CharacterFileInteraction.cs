﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Win32;
using PluginApi.Model;

namespace PrintChar
{
	public class CharacterFileInteraction
	{
		private readonly IEnumerable<GameSystem> _gameSystems;
		private readonly bool _requireFileToExist;
		private readonly Func<GameSystem, string, Character> _loader;

		public CharacterFileInteraction(IEnumerable<GameSystem> gameSystems, bool requireFileToExist, [NotNull] Func<GameSystem, string, Character> loader)
		{
			_gameSystems = gameSystems;
			_requireFileToExist = requireFileToExist;
			_loader = loader;
		}

		[CanBeNull]
		public Character LoadCharacter([CanBeNull] Character character)
		{
			return LoadCharacter(character, _Open(CreateDialog(character)));
		}

		[CanBeNull]
		public Character LoadCharacter([CanBeNull] Character character, [CanBeNull] string fileName)
		{
			if (String.IsNullOrEmpty(fileName) || (character != null && character.File.FullName == fileName))
				return character;

			string extensionWithoutPeriod = Path.GetExtension(fileName).Substring(1);
			return _loader(_gameSystems.First(g => g.FileExtension == extensionWithoutPeriod), fileName);
		}

		[NotNull]
		public OpenFileDialog CreateDialog([CanBeNull] Character character)
		{
			return new OpenFileDialog
			{
				Filter = String.Join("|", _gameSystems
					.Select(_FormatGameSystemFileInfo)),
				DefaultExt = (character == null ? _gameSystems.First() : character.GameSystem).FileExtension,
				CheckFileExists = _requireFileToExist,
				Multiselect = false,
				InitialDirectory = character == null ? null : character.File.DirectoryName
			};
		}

		[NotNull]
		public IEnumerable<GameSystem> GameSystems
		{
			get { return _gameSystems; }
		}

		public bool RequireFileToExist
		{
			get { return _requireFileToExist; }
		}

		[NotNull]
		public Func<GameSystem, string, Character> Loader
		{
			get { return _loader; }
		}

		[CanBeNull]
		private static string _Open([NotNull] FileDialog dialog)
		{
			return dialog.ShowDialog() == true ? dialog.FileName : null;
		}

		[NotNull]
		private static string _FormatGameSystemFileInfo([NotNull] GameSystem g)
		{
			return String.Format("{0} file ({1})|{1}", g.Name, g.FilePattern);
		}
	}
}
