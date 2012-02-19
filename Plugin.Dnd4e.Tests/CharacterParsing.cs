using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using NUnit.Framework;
using WotcOnlineDataRepository;

namespace Plugin.Dnd4e.Tests
{
	[TestFixture]
	public class CharacterParsing
	{
		[Test]
		public void ShouldParseEarlyOfflineFormat()
		{
			var runData = _EarlyOffline();
			Assert.That(new CharacterFile(runData.FileName).ToCharacter(), Is.EqualTo(runData.Result));
		}

		[Test]
		public void ShouldParseFinalOfflineFormat()
		{
			var runData = _LateOffline();
			Assert.That(new CharacterFile(runData.FileName).ToCharacter(), Is.EqualTo(runData.Result));
		}

		[Test]
		public void ShouldParseInitialOnlineFormat()
		{
			var runData = _EarlyOnline();
			Assert.That(new CharacterFile(runData.FileName).ToCharacter(), Is.EqualTo(runData.Result));
		}

		public class CharacterExpectations
		{
			[NotNull] public FileInfo FileName;
			[NotNull] public CharacterDnd4E Result;
		}

		public IEnumerable<CharacterExpectations> SampleData()
		{
			yield return _EarlyOffline();
			yield return _LateOffline();
			yield return _EarlyOnline();
		}

		[NotNull]
		private static CharacterExpectations _EarlyOffline()
		{
			return new CharacterExpectations
			{
				FileName = new FileInfo(@"SampleData\Varis.dnd4e"),
				Result =
					new CharacterDnd4E
					{
						Name = "Varis",
						Gender = "Male",
						Race = "Elf",
						CharClass = "Ranger",
						Powers =
							{
								new Power
								{
									Name = "Melee Basic Attack",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard()
								},
								new Power
								{
									Name = "Ranged Basic Attack",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard()
								},
								new Power
								{
									Name = "Elven Accuracy",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Free(),
									PowerId = 1450
								},
								new Power
								{
									Name = "Hunter's Quarry",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Minor(),
									PowerId = 5598
								},
								new Power
								{
									Name = "Nimble Strike",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard(),
									PowerId = 919
								},
								new Power
								{
									Name = "Twin Strike",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard(),
									PowerId = 87
								},
								new Power
								{
									Name = "Two-Fanged Strike",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Standard(),
									PowerId = 2209
								},
								new Power
								{
									Name = "Split the Tree",
									Refresh = Power.Usage.Daily,
									Action = ActionType.Standard(),
									PowerId = 2207
								},
								new Power
								{
									Name = "Disruptive Strike",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Interrupt(),
									PowerId = 1416
								},
								new Power
								{
									Name = "Spitting-Cobra Stance",
									Refresh = Power.Usage.Daily,
									Action = ActionType.Minor(),
									PowerId = 4394
								},
								new Power
								{
									Name = "Serpentine Dodge",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Move(),
									PowerId = 4400
								},
								new Power
								{
									Name = "Biting Volley",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Standard(),
									PowerId = 4402
								},
								new Power
								{
									Name = "Fast Hands",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Free(),
									PowerId = 9344
								},
								new Power
								{
									Name = "Attacks on the Run",
									Refresh = Power.Usage.Daily,
									Action = ActionType.Standard(),
									PowerId = 384
								},
								new Power
								{
									Name = "Expeditious Stride",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Minor(),
									PowerId = 926
								},
							},
					},
			};
		}

		private static CharacterExpectations _EarlyOnline()
		{
			return new CharacterExpectations
			{
				FileName = new FileInfo(@"SampleData\Shivra.dnd4e"),
				Result = new CharacterDnd4E
				{
					Name = "Shivra",
					Gender = "female",
					Race = "Drow",
					CharClass = "Sorcerer",
					Powers =
						{
							new Power
							{
								Name = "Melee Basic Attack",
								Refresh = Power.Usage.AtWill,
								Action = ActionType.Standard()
							},
							new Power
							{
								Name = "Ranged Basic Attack",
								Refresh = Power.Usage.AtWill,
								Action = ActionType.Standard()
							},
							new Power
							{
								Name = "Cloud of Darkness",
								Refresh = Power.Usage.Encounter,
								Action = ActionType.Minor(),
								PowerId = 2473
							},
							new Power
							{
								Name = "Arcing Fire",
								Refresh = Power.Usage.AtWill,
								Action = ActionType.Standard(),
								PowerId = 7405
							},
							new Power
							{
								Name = "Blazing Starfall",
								Refresh = Power.Usage.AtWill,
								Action = ActionType.Standard(),
								PowerId = 5840
							},
							new Power
							{
								Name = "Whirlwind",
								Refresh = Power.Usage.Encounter,
								Action = ActionType.Standard(),
								PowerId = 3009
							},
							new Power
							{
								Name = "Howling Tempest",
								Refresh = Power.Usage.Daily,
								Action = ActionType.Standard(),
								PowerId = 3034
							},
							new Power
							{
								Name = "Deep Shroud",
								Refresh = Power.Usage.Daily,
								Action = ActionType.Minor(),
								PowerId = 3207
							},
							new Power
							{
								Name = "Thundering Gust",
								Refresh = Power.Usage.Encounter,
								Action = ActionType.Standard(),
								PowerId = 3012
							},
							new Power
							{
								Name = "Thunder Leap",
								Refresh = Power.Usage.Daily,
								Action = ActionType.Standard(),
								PowerId = 5274
							},
							new Power
							{
								Name = "Sudden Scales",
								Refresh = Power.Usage.Encounter,
								Action = ActionType.Interrupt(),
								PowerId = 5277
							},
							new Power
							{
								Name = "Spark Form",
								Refresh = Power.Usage.Encounter,
								Action = ActionType.Standard(),
								PowerId = 3016
							},
							new Power
							{
								Name = "Howling Hurricane",
								Refresh = Power.Usage.Daily,
								Action = ActionType.Standard(),
								PowerId = 5856
							},
							new Power
							{
								Name = "Narrow Escape",
								Refresh = Power.Usage.Encounter,
								Action = ActionType.Reaction(),
								PowerId = 5283
							},
							new Power
							{
								Name = "Accursed Flames",
								Refresh = Power.Usage.Encounter,
								Action = ActionType.Minor(),
								PowerId = 4791
							},
						},
				},
			};
		}

		private static CharacterExpectations _LateOffline()
		{
			return new CharacterExpectations
			{
				FileName = new FileInfo(@"SampleData\Pieter.dnd4e"),
				Result =
					new CharacterDnd4E
					{
						Name = "Pieter",
						Gender = "Male",
						Race = "Human",
						CharClass = "Wizard",
						Powers =
							{
								new Power
								{
									Name = "Melee Basic Attack",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard()
								},
								new Power
								{
									Name = "Ranged Basic Attack",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard()
								},
								new Power
								{
									Name = "Hand of Radiance",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Standard(),
									PowerId = 7151
								},
								new Power
								{
									Name = "Chilling Cloud",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard(),
									PowerId = 7406
								},
								new Power
								{
									Name = "Orb of Imposition",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Free(),
									PowerId = 5594
								},
								new Power
								{
									Name = "Ghost Sound",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard(),
									PowerId = 1217
								},
								new Power
								{
									Name = "Light",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Minor(),
									PowerId = 1225
								},
								new Power
								{
									Name = "Mage Hand",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Minor(),
									PowerId = 1227
								},
								new Power
								{
									Name = "Prestidigitation",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard(),
									PowerId = 1930
								},
								new Power
								{
									Name = "Cloud of Daggers",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard(),
									PowerId = 1164
								},
								new Power
								{
									Name = "Thunderwave",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Standard(),
									PowerId = 1169
								},
								new Power
								{
									Name = "Grasping Shadows",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Standard(),
									PowerId = 3215
								},
								new Power
								{
									Name = "Flaming Sphere",
									Refresh = Power.Usage.Daily,
									Action = ActionType.Standard(),
									PowerId = 1435
								},
								new Power
								{
									Name = "Sleep",
									Refresh = Power.Usage.Daily,
									Action = ActionType.Standard(),
									PowerId = 451
								},
								new Power
								{
									Name = "Grease",
									Refresh = Power.Usage.Daily,
									Action = ActionType.Standard(),
									PowerId = 2351
								},
								new Power
								{
									Name = "Grease Attack",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Free(),
									PowerId = 7340
								},
								new Power
								{
									Name = "Shield",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Interrupt(),
									PowerId = 1235
								},
								new Power
								{
									Name = "Arcane Insight",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Free(),
									PowerId = 4236
								},
								new Power
								{
									Name = "Hypnotic Pattern",
									Refresh = Power.Usage.Encounter,
									Action = ActionType.Standard(),
									PowerId = 4020
								},
								new Power
								{
									Name = "Hypnotic Pattern Attack",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Opportunity(),
									PowerId = 7343
								},
								new Power
								{
									Name = "Grasp of the Grave",
									Refresh = Power.Usage.Daily,
									Action = ActionType.Standard(),
									PowerId = 6958
								},
								new Power
								{
									Name = "Scattering Shock",
									Refresh = Power.Usage.Daily,
									Action = ActionType.Standard(),
									PowerId = 11034
								},
								new Power
								{
									Name = "Visions of Avarice",
									Refresh = Power.Usage.Daily,
									Action = ActionType.Standard(),
									PowerId = 4074
								},
								new Power
								{
									Name = "Visions of Avarice Attack",
									Refresh = Power.Usage.AtWill,
									Action = ActionType.Minor(),
									PowerId = 7344
								},
							},
					},
			};
		}
	}
}
