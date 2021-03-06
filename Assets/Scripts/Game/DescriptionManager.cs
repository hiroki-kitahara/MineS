﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DescriptionManager : SingletonMonoBehaviour<DescriptionManager>
	{
		[SerializeField]
		private List<DescriptionDataObserver> basicObservers;

		[SerializeField]
		private List<CharacterDataObserver> characterDataObservers;

		[SerializeField]
		private List<ItemObserver> itemObservers;

		[SerializeField]
		private DescriptionData data;

		[SerializeField]
		private GameObject emergencyRoot;

		[SerializeField]
		private Text emergencyText;

		public void Deploy(CharacterData data)
		{
			this.characterDataObservers.ForEach(o =>
			{
				o.gameObject.SetActive(true);
				o.ModifiedData(data);
			});
		}

		public void Deploy(string key)
		{
			this.Deploy(this.data.Get(key));
		}

		public void Deploy(DescriptionData.Element element)
		{
			this.basicObservers.ForEach(o =>
			{
				o.gameObject.SetActive(true);
				o.ModifiedData(element);
			});
		}

		public void Deploy(Item item)
		{
			if(item.InstanceData.ItemType == GameDefine.ItemType.UsableItem)
			{
				this.Deploy((item.InstanceData as UsableItemInstanceData).DescriptionElement);
			}
			else if(GameDefine.IsEquipment(item.InstanceData.ItemType))
			{
				this.itemObservers.ForEach(i =>
				{
					i.gameObject.SetActive(true);
					i.ModifiedData(item);
				});
			}
			else if(item.InstanceData.ItemType == GameDefine.ItemType.Throwing)
			{
				this.Deploy((item.InstanceData as ThrowingInstanceData).DescriptionElement);
			}
			else if(item.InstanceData.ItemType == GameDefine.ItemType.MagicStone)
			{
				this.Deploy((item.InstanceData as MagicStoneInstanceData).DescriptionElement);
			}
			else
			{
				Debug.AssertFormat(false, "未対応のアイテムです. itemType = {0}", item.InstanceData.ItemType);
			}
		}

		public void DeployEmergency(string key, params object[] option)
		{
			this.emergencyRoot.SetActive(true);
			this.emergencyText.text = string.Format(this.data.Get(key).Message, option);
		}

		public DescriptionData Data{ get { return this.data; } }
	}
}