﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[System.Serializable]
	public class Item
	{
		public class ItemEvent : UnityEvent<Item>
		{
		}

		public ItemDataBase InstanceData{ private set; get; }

		private static ItemEvent onUseItemEvent = new ItemEvent();

		public Item(ItemDataBase masterData)
		{
			this.InstanceData = masterData.Clone;
		}

		public static void AddOnUseItemEvent(UnityAction<Item> call)
		{
			onUseItemEvent.AddListener(call);
		}

		public void Use(IAttack user)
		{
			if(GameDefine.IsEquipment(this.InstanceData.ItemType))
			{
				var changedEquipment = PlayerManager.Instance.Data.Inventory.ChangeEquipment(this);
				PlayerManager.Instance.ChangeItem(this, changedEquipment);
			}
			else if(this.InstanceData.ItemType == GameDefine.ItemType.UsableItem)
			{
				this.UseUsableItem(user, PlayerManager.Instance.Data.Inventory);
			}
			else
			{
				Debug.LogWarning("未実装のアイテムです ItemType = " + this.InstanceData.ItemType);
			}
			onUseItemEvent.Invoke(this);
		}

		private void UseUsableItem(IAttack user, Inventory inventory)
		{
			var usableItem = this.InstanceData as UsableItemData;
			switch(usableItem.UsableItemType)
			{
			case GameDefine.UsableItemType.RecoveryHitPointLimit:
				{
					var value = Calculator.GetUsableItemRecoveryValue(usableItem.RandomPower, user);
					user.RecoveryHitPoint(value, true);
					inventory.RemoveItem(this);
					InformationManager.OnUseRecoveryHitPointItem(user, value);
				}
			break;
			case GameDefine.UsableItemType.RecoveryArmor:
				{
					var value = Calculator.GetUsableItemRecoveryValue(usableItem.RandomPower, user);
					user.RecoveryArmor(value);
					inventory.RemoveItem(this);
					InformationManager.OnUseRecoveryArmorItem(user, value);
				}
			break;
			case GameDefine.UsableItemType.AddAbnormalStatus:
				{
					var type = (GameDefine.AbnormalStatusType)usableItem.Power0;
					var addAbnormalResultType = user.AddAbnormalStatus(AbnormalStatusFactory.Create(type, user, usableItem.Power1, 0));
					inventory.RemoveItem(this);
					InformationManager.OnUseAddAbnormalStatusItem(user, type, addAbnormalResultType);
				}
			break;
			case GameDefine.UsableItemType.RemoveAbnormalStatus:
				{
					var type = (GameDefine.AbnormalStatusType)usableItem.Power0;
					user.RemoveAbnormalStatus(type);
					inventory.RemoveItem(this);
					InformationManager.OnUseRemoveAbnormalStatusItem(user, type);
				}
			break;
			case GameDefine.UsableItemType.Damage:
				{
					var damage = usableItem.RandomPower;
					user.TakeDamageRaw(null, damage, false);
					InformationManager.OnUseDamageItem(user, damage);
					if(user.CharacterType == GameDefine.CharacterType.Enemy && user.IsDead)
					{
						PlayerManager.Instance.Data.Defeat(user);
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.NailDown:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						CellManager.Instance.OnUseXrayAll();
						InformationManager.OnUseNailDown();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.CallingOff:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						CellManager.Instance.RemoveTrap();
						InformationManager.OnUseCallingOff();
					}
					else
					{
						InformationManager.OnHadNoEffect();
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.Drain:
				{
					if(user.CharacterType == GameDefine.CharacterType.Player)
					{
						InformationManager.OnHadNoEffect();
						InformationManager.WillThrowEnemy();
					}
					else
					{
						var playerData = PlayerManager.Instance.Data;
						var damage = user.HitPoint / 2;
						user.TakeDamageRaw(playerData, damage, true);
						playerData.RecoveryHitPoint(damage, true);
					}
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.Brake:
				{
					user.TakeDamageArmorOnly(user.Armor);
				}
			break;
			case GameDefine.UsableItemType.UndineDrop:
				{
					var value = usableItem.Power0;
					user.AddBaseStrength(usableItem.Power0);
					InformationManager.AddBaseStrength(user, value);
					inventory.RemoveItem(this);
				}
			break;
			case GameDefine.UsableItemType.UndineTear:
				{
					var value = usableItem.Power0;
					user.AddHitPointMax(usableItem.Power0);
					InformationManager.AddHitPointMax(user, value);
					inventory.RemoveItem(this);
				}
			break;
			default:
				Debug.LogWarning("未実装の使用可能アイテムです UsableItemType= " + usableItem.UsableItemType);
			break;
			}
		}

	}
}