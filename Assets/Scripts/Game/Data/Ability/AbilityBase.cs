﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AbilityBase
	{
		public GameDefine.AbilityType Type{ protected set; get; }

		public CharacterData Holder{ protected set; get; }

		public string DescriptionKey{ protected set; get; }

		public AbilityBase(GameDefine.AbilityType type, CharacterData holder, string descriptionKey)
		{
			this.Type = type;
			this.Holder = holder;
			this.DescriptionKey = descriptionKey;
		}

		public void SetHolder(CharacterData holder)
		{
			this.Holder = holder;
		}

		public virtual void OnTurnProgress()
		{
			
		}
	}
}