﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbnormalStatusDull : AbnormalStatusBase
	{
		public AbnormalStatusDull()
			: base()
		{
		}

		public AbnormalStatusDull(IAttack holder, int remainingTurn, int waitTurn)
			: base(holder, remainingTurn, waitTurn)
		{
		}

		public override GameDefine.AbnormalStatusType Type
		{
			get
			{
				return GameDefine.AbnormalStatusType.Dull;
			}
		}

		public override GameDefine.AbnormalStatusType OppositeType
		{
			get
			{
				return GameDefine.AbnormalStatusType.Sharpness;
			}
		}

		public override GameDefine.AbilityType InvalidateAbilityType
		{
			get
			{
				return GameDefine.AbilityType.Plating;
			}
		}
	}
}