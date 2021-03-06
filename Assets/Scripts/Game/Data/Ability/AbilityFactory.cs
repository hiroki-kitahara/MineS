﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public static class AbilityFactory
	{
		public static List<AbilityBase> Create(List<GameDefine.AbilityType> types, CharacterData holder)
		{
			var result = new List<AbilityBase>();
			types.ForEach(a => result.Add(Create(a, holder)));

			return result;
		}

		public static AbilityBase Create(GameDefine.AbilityType type, CharacterData holder)
		{
			switch(type)
			{
			case GameDefine.AbilityType.Penetoration:
				return new AbilityBase(type, holder, "Penetoration");
			case GameDefine.AbilityType.Mercy:
				return new AbilityMercy(holder);
			case GameDefine.AbilityType.Regeneration:
				return new AbilityRegeneration(holder);
			case GameDefine.AbilityType.Splash:
				return new AbilityBase(type, holder, "Splash");
			case GameDefine.AbilityType.Absorption:
				return new AbilityBase(type, holder, "Absorption");
			case GameDefine.AbilityType.LongRangeAttack:
				return new AbilityLongRangeAttack(holder);
			case GameDefine.AbilityType.Reinforcement:
				return new AbilityBase(type, holder, "Reinforcement");
			case GameDefine.AbilityType.Proficiency:
				return new AbilityBase(type, holder, "Proficiency");
			case GameDefine.AbilityType.Hoist:
				return new AbilityBase(type, holder, "Hoist");
			case GameDefine.AbilityType.Artisan:
				return new AbilityBase(type, holder, "Artisan");
			case GameDefine.AbilityType.ContinuousAttack:
				return new AbilityBase(type, holder, "ContinuousAttack");
			case GameDefine.AbilityType.Repair:
				return new AbilityBase(type, holder, "Repair");
			case GameDefine.AbilityType.Goemon:
				return new AbilityBase(type, holder, "Goemon");
			case GameDefine.AbilityType.HitProbability:
				return new AbilityBase(type, holder, "HitProbability");
			case GameDefine.AbilityType.Evasion:
				return new AbilityBase(type, holder, "Evasion");
			case GameDefine.AbilityType.InbariablyHit:
				return new AbilityBase(type, holder, "InbariablyHit");
			case GameDefine.AbilityType.Theft:
				return new AbilityBase(type, holder, "Theft");
			case GameDefine.AbilityType.Fortune:
				return new AbilityBase(type, holder, "Fortune");
			case GameDefine.AbilityType.RiskOfLife:
				return new AbilityBase(type, holder, "RiskOfLife");
			case GameDefine.AbilityType.Enhancement:
				return new AbilityBase(type, holder, "Enhancement");
			case GameDefine.AbilityType.Immunity:
				return new AbilityBase(type, holder, "Immunity");
			case GameDefine.AbilityType.AvoidTrap:
				return new AbilityBase(type, holder, "AvoidTrap");
			case GameDefine.AbilityType.Exquisite:
				return new AbilityBase(type, holder, "Exquisite");
			case GameDefine.AbilityType.Weak:
				return new AbilityBase(type, holder, "Weak");
			case GameDefine.AbilityType.Infection:
				return new AbilityBase(type, holder, "Infection");
			case GameDefine.AbilityType.HealingBuddha:
				return new AbilityBase(type, holder, "HealingBuddha");
			case GameDefine.AbilityType.PoisonPainted:
				return new AbilityBase(type, holder, "PoisonPainted");
			case GameDefine.AbilityType.Absentmindedness:
				return new AbilityBase(type, holder, "Absentmindedness");
			case GameDefine.AbilityType.VitalsPoke:
				return new AbilityBase(type, holder, "VitalsPoke");
			case GameDefine.AbilityType.BladeBroken:
				return new AbilityBase(type, holder, "BladeBroken");
			case GameDefine.AbilityType.Derangement:
				return new AbilityBase(type, holder, "Derangement");
			case GameDefine.AbilityType.Intimidation:
				return new AbilityBase(type, holder, "Intimidation");
			case GameDefine.AbilityType.Curse:
				return new AbilityBase(type, holder, "Curse");
			case GameDefine.AbilityType.WarCry:
				return new AbilityWarCry(holder);
			case GameDefine.AbilityType.Recovery:
				return new AbilityBase(type, holder, "Recovery");
			case GameDefine.AbilityType.Protection:
				return new AbilityBase(type, holder, "Protection");
			case GameDefine.AbilityType.Initiative:
				return new AbilityBase(type, holder, "Initiative");
			case GameDefine.AbilityType.Clairvoyance:
				return new AbilityClairvoyance(holder);
			case GameDefine.AbilityType.Division:
				return new AbilityBase(type, holder, "Division");
			case GameDefine.AbilityType.Summon:
				return new AbilityBase(type, holder, "Summon");
			case GameDefine.AbilityType.Reincarnation:
				return new AbilityBase(type, holder, "Reincarnation");
			case GameDefine.AbilityType.Provocation:
				return new AbilityBase(type, holder, "Provocation");
			case GameDefine.AbilityType.Serum:
				return new AbilityBase(type, holder, "Serum");
			case GameDefine.AbilityType.Concentration:
				return new AbilityBase(type, holder, "Concentration");
			case GameDefine.AbilityType.CompleteRecovery:
				return new AbilityBase(type, holder, "CompleteRecovery");
			case GameDefine.AbilityType.Plating:
				return new AbilityBase(type, holder, "Plating");
			case GameDefine.AbilityType.Bravery:
				return new AbilityBase(type, holder, "Bravery");
			case GameDefine.AbilityType.Talisman:
				return new AbilityBase(type, holder, "Talisman");
			case GameDefine.AbilityType.Care:
				return new AbilityBase(type, holder, "Care");
			case GameDefine.AbilityType.GrantRegeneration:
				return new AbilityGrantRegeneration(holder);
			case GameDefine.AbilityType.GrantSharpness:
				return new AbilityGrantSharpness(holder);
			case GameDefine.AbilityType.GrantCuring:
				return new AbilityGrantCuring(holder);
			case GameDefine.AbilityType.Unity:
				return new AbilityBase(type, holder, "Unity");
			default:
				Debug.AssertFormat(false, "不正な値です. type = " + type);
				return null;
			}
		}
	}
}