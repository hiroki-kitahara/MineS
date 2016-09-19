﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable][CreateAssetMenu()]
	public class WeaponMasterData : ScriptableObject
	{
		[System.Serializable]
		public class Element : ItemMasterDataBase
		{
			[SerializeField]
			private int power;

			[SerializeField]
			private List<int> spells;

			public override GameDefine.ItemType ItemType
			{
				get
				{
					return GameDefine.ItemType.Weapon;
				}
			}
		}

		[SerializeField]
		private List<Element> database;
	}
}