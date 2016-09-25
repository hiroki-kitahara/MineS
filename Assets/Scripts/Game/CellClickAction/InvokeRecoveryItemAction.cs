﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeRecoveryItemAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			data.Controller.SetImage(null);
			data.BindCellClickAction(null);
			PlayerManager.Instance.RecoveryHitPoint(GameDefine.RecoveryItemRecovery, false);
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.RecoveryItem;
			}
		}

		public override Sprite Image
		{
			get
			{
				return DungeonManager.Instance.CurrentData.RecoveryItemImage;
			}
		}
	}
}