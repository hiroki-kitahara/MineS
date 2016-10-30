﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class AcquireItemAction : CellClickActionBase
	{
		private Item item;

		private const string ItemSerialzeKeyFormat = "AcquireItemActionItem_{0}_{1}";

		public AcquireItemAction(Item item, CellController cellController)
		{
			this.item = item;
			cellController.SetImage(this.item.InstanceData.Image);
		}

		public override void Invoke(CellData data)
		{
			var acquiredType = PlayerManager.Instance.AddItemOnClickCell(this.item, data);
			if(acquiredType == GameDefine.AcquireItemResultType.Acquired)
			{
				data.Controller.SetImage(null);
				data.BindCellClickAction(null);
				data.BindDeployDescription(null);
				SEManager.Instance.PlaySE(SEManager.Instance.acquireItem);
			}
		}

		public override void Serialize(int y, int x)
		{
			this.item.Serialize(this.GetItemSerialzeKeyName(y, x));
		}

		public override void Deserialize(int y, int x)
		{
			this.item = Item.Deserialize(this.GetItemSerialzeKeyName(y, x));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Item;
			}
		}

		public override Sprite Image
		{
			get
			{
				return this.item.InstanceData.Image;
			}
		}

		private string GetItemSerialzeKeyName(int y, int x)
		{
			return string.Format(ItemSerialzeKeyFormat, y, x);
		}
	}
}