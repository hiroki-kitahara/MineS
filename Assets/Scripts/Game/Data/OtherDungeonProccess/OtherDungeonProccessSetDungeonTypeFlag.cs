﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class OtherDungeonProccessSetDungeonTypeFlag : OtherDungeonProccessBase
	{
		[SerializeField]
		GameDefine.DungeonType type;

		public override void Invoke()
		{
			MineS.SaveData.Progress.ClearDungeon(this.type);
		}
	}
}