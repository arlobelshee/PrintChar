using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using NUnit.Framework;

namespace WotcOnlineDataRepository.Tests
{
	[TestFixture]
	public class ParseTargetType
	{
		[Test]
		public void MeleeWeapon()
		{
			Assert.That(TargetType.For("meleE WeapOn"), Is.EqualTo(TargetType.MeleeWeapon));
		}

		[Test]
		public void RangedWeapon()
		{
			Assert.That(TargetType.For("rAngeD WeaPon"), Is.EqualTo(TargetType.RangedWeapon));
		}

		[Test]
		public void RangedFixed()
		{
			Assert.That(TargetType.For("rAngeD 32"), Is.EqualTo(TargetType.Ranged(32)));
		}

		[Test]
		public void MeleeFixed()
		{
			Assert.That(TargetType.For("MeLee 3"), Is.EqualTo(TargetType.Melee(3)));
		}

		[Test]
		public void AnyWeapon()
		{
			Assert.That(TargetType.For("anY WeapoN"), Is.EqualTo(TargetType.AnyWeapon));
		}

		[Test]
		public void CloseBurst()
		{
			Assert.That(TargetType.For("cLose burSt 9"), Is.EqualTo(TargetType.CloseBurst(9)));
		}

		[Test]
		public void CloseBlast()
		{
			Assert.That(TargetType.For("cLose BLast 2"), Is.EqualTo(TargetType.CloseBlast(2)));
		}

		[Test]
		public void Touch()
		{
			Assert.That(TargetType.For("ToUch"), Is.EqualTo(TargetType.Touch));
		}

		[Test]
		public void MeleeTouchIsTouch()
		{
			Assert.That(TargetType.For("mEleE ToUch"), Is.EqualTo(TargetType.Touch));
		}

		[Test]
		public void Personal()
		{
			Assert.That(TargetType.For("pERSonal"), Is.EqualTo(TargetType.Personal));
		}

		[Test]
		public void Area()
		{
			Assert.That(TargetType.For("aRea bURst 9 wiTHiN 14"), Is.EqualTo(TargetType.Area(9, 14)));
		}

		[Test]
		public void UnparsableTargetTypeThrowsException()
		{
			Assert.Throws(typeof (NotImplementedException), () => TargetType.For("MeLee 3n"));
			Assert.Throws(typeof (NotImplementedException), () => TargetType.For("close blast 3n"));
			Assert.Throws(typeof (NotImplementedException), () => TargetType.For("close frog 3"));
		}
	}
}
