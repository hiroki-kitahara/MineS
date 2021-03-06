﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DebugAcquireItem : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		private Text text;

		private ItemMasterDataBase item;

		public void Initialize(ItemMasterDataBase item)
		{
			this.item = item;
			this.text.text = this.item.ItemName;
		}

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			var playerManager = PlayerManager.Instance;
			if(playerManager.Data.Inventory.IsFreeSpace)
			{
				playerManager.AddItem(new Item(this.item));
			}
			else
			{
				Debug.LogWarning("空きがないよ.");
			}
		}

#endregion
	}
}