﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CharacterData
	{
		public int HitPointMax{ private set; get; }

		public int HitPoint{ private set; get; }

		public int MagicPointMax{ private set; get; }

		public int MagicPoint{ private set; get; }

		public int Strength{ private set; get; }

		public int Armor{ private set; get; }

		public void Initialize(int hitPoint, int magicPoint, int strength, int armor)
		{
			this.HitPointMax = hitPoint;
			this.HitPoint = hitPoint;
			this.MagicPointMax = magicPoint;
			this.MagicPoint = magicPoint;
			this.Strength = strength;
			this.Armor = armor;
		}

		public void Recovery(int value)
		{
			this.HitPoint += value;
		}

		public void TakeDamage(int value, bool onlyHitPoint)
		{
			if(!onlyHitPoint)
			{
				this.Armor -= value;
				value = -this.Armor;
				value = value < 0 ? 0 : value;
				this.Armor = this.Armor < 0 ? 0 : this.Armor;
			}

			this.HitPoint -= value;
			this.HitPoint = this.HitPoint < 0 ? 0 : this.HitPoint;
		}

		public bool IsDead
		{
			get
			{
				return this.HitPoint <= 0;
			}
		}
	}
}