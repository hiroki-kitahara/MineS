﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class SelectShopBuyItemAction : CellClickActionBase
	{
		private Item item;

		public SelectShopBuyItemAction(Item item)
		{
			this.item = item;
		}

		public override void Invoke(CellData data)
		{
			Debug.AssertFormat(this.item != null, "アイテムがありません.");
			ShopManager.Instance.Buy(this.item);
		}

		public override void SetCellController(CellController cellController)
		{
			base.SetCellController(cellController);
			if(this.item != null)
			{
				this.cellController.SetImage(this.item.InstanceData.Image);
			}
		}

		public override void SetCellData(CellData data)
		{
			base.SetCellData(data);
			data.BindDeployDescription(new DeployDescriptionOnItem(this.item));
		}

		public override GameDefine.EventType EventType
		{
			get
			{
				return GameDefine.EventType.Shop;
			}
		}

		public override Sprite Image
		{
			get
			{
				return this.item.InstanceData.Image;
			}
		}
	}
}