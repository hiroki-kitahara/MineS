﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateShopAction : CellClickActionBase
	{
		public CreateShopAction()
		{
		}

		public override void Invoke(CellData data)
		{
			this.cellController.SetImage(this.Image);
			data.BindCellClickAction(new VisitShopAction(false));
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
				return TextureManager.Instance.shop.Element;
			}
		}

		public override void Serialize(int y, int x)
		{
		}

		public override void Deserialize(int y, int x)
		{
		}
	}
}