﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbilityRegeneration : AbilityBase
	{
		public AbilityRegeneration(CharacterData holder)
			: base(GameDefine.AbilityType.Regeneration, holder, "Regeneration")
		{
		}

		public override void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			if(this.Holder == null)
			{
				return;
			}

			this.Holder.RecoveryHitPoint(Calculator.GetRegenerationValue(this.Holder.HitPointMax), true);
		}
	}
}