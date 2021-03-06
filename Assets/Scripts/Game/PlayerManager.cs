﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;
using UnityEngine.Events;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class PlayerManager : SingletonMonoBehaviour<PlayerManager>, ITurnProgress
	{
		public class InventoryEvent : UnityEvent<Inventory>
		{
		}

		[SerializeField]
		private List<CharacterDataObserver> characterDataObservers;

		[SerializeField]
		private List<InventoryObserver> inventoryObservers;

		[SerializeField]
		private List<ItemObserver> selectItemObservers;

		[SerializeField]
		private GameObject inventoryUI;

		[SerializeField]
		private ExperienceData experienceData;

		[SerializeField]
		private CharacterMasterData playerInitialStatus;

		[SerializeField]
		private CharacterMasterData growthData;

		[SerializeField]
		private CellController cellController;

		private InventoryEvent onOpenInventoryUI = new InventoryEvent();

		private InventoryEvent onCloseInventoryUI = new InventoryEvent();

		private Inventory currentOpenInventory = null;

		public PlayerData Data{ private set; get; }

		public ExperienceData ExperienceData{ get { return this.experienceData; } }

		private const string PlayerDataSerializeKeyName = "PlayerData";

		protected override void Awake()
		{
			base.Awake();
			this.LoadData();
		}

		void Start()
		{
			this.NotifyCharacterDataObservers();
			TurnManager.Instance.AddEndTurnEvent(this.OnTurnProgress);
			TurnManager.Instance.AddLateEndTurnEvent(this.OnLateTurnProgress);
			Item.AddOnUseItemEvent(this.OnUseItem);
			DungeonManager.Instance.AddNextFloorEvent(this.OnNextFloor);
			DungeonManager.Instance.AddChangeDungeonEvent(this.OnChangeDungeon);
		}

		public void RecoveryHitPoint(int value, bool isLimit)
		{
			this.Data.RecoveryHitPoint(value, isLimit);
			this.NotifyCharacterDataObservers();
		}

		public void RecoveryArmor(int value, bool playSE)
		{
			this.Data.RecoveryArmor(value, playSE);
			this.NotifyCharacterDataObservers();
		}

		public void AddExperience(int value)
		{
			value = Calculator.GetFinalExperience(value, this.Data);
			this.Data.AddExperience(value);
			this.NotifyCharacterDataObservers();
			while(this.Data.CanLevelUp)
			{
				this.Data.LevelUp(this.growthData);
				this.NotifyCharacterDataObservers();
			}
		}

		public void RemoveInventoryItem(Item item)
		{
			this.Data.Inventory.RemoveItem(item);
			if(MineS.SaveData.Option.AutoSort)
			{
				this.SortItem();
			}
			this.Serialize();
		}

		public void RemoveInventoryItemOrEquipment(Item item)
		{
			this.Data.Inventory.RemoveItemOrEquipment(item);
			if(MineS.SaveData.Option.AutoSort)
			{
				this.SortItem();
			}
			this.Serialize();
		}

		public void ChangeItem(Item before, Item after)
		{
			this.Data.Inventory.ChangeItem(before, after);
			if(MineS.SaveData.Option.AutoSort)
			{
				this.SortItem();
			}
			this.Serialize();
		}

		public void SortItem()
		{
			this.Data.Inventory.Sort();
			this.Serialize();
			this.UpdateInventoryUI();
		}

		public void RemoveEquipment(Item equipment)
		{
			this.Data.Inventory.RemoveEquipment(equipment);
			this.Data.Inventory.AddItem(equipment);
			if(MineS.SaveData.Option.AutoSort)
			{
				this.SortItem();
			}
			this.Serialize();
		}

		public void AddMoney(int value, bool playSE)
		{
			if(playSE)
			{
				SEManager.Instance.PlaySE(SEManager.Instance.acquireMoney);
			}
			this.Data.AddMoney(Calculator.GetFinalMoney(value, this.Data));
			this.NotifyCharacterDataObservers();
			this.Serialize();
		}

		public void NotifyCharacterDataObservers()
		{
			if(ResultManager.Instance.IsResult)
			{
				return;
			}

			for(int i = 0; i < this.characterDataObservers.Count; i++)
			{
				this.characterDataObservers[i].ModifiedData(this.Data);
			}
		}

		public void OpenInventoryUI(GameDefine.InventoryModeType type, Inventory inventory)
		{
			CellManager.Instance.ChangeCellClickMode(GameDefine.CellClickMode.Step);
			this.currentOpenInventory = inventory;
			this.currentOpenInventory.SetMode(type);
			this.inventoryUI.SetActive(true);
			this.UpdateInventoryUI(inventory);
			this.onOpenInventoryUI.Invoke(inventory);
		}

		public void CloseInventoryUI()
		{
			this.inventoryUI.SetActive(false);

			if(this.currentOpenInventory != null)
			{
				this.currentOpenInventory.SetExchangeItem(null, null);
				this.onCloseInventoryUI.Invoke(this.currentOpenInventory);
			}
		}

		public void SelectItem(Item item)
		{
			this.Data.Inventory.SetSelectItem(item);
			this.selectItemObservers.ForEach(i => i.ModifiedData(item));
		}

		public void CloseConfirmSelectItemUI()
		{
			this.Data.Inventory.SetSelectItem(null);
		}

		public void UpdateInventoryUI(Inventory inventory)
		{
			this.currentOpenInventory = inventory;
			this.inventoryObservers.ForEach(i => i.ModifiedData(inventory));
		}

		public void UpdateInventoryUI()
		{
			this.UpdateInventoryUI(this.currentOpenInventory);
		}

		public GameDefine.AddAbnormalStatusResultType AddAbnormalStatus(GameDefine.AbnormalStatusType type, int remainingTurn, int waitTurn)
		{
			var result = this.Data.AddAbnormalStatus(AbnormalStatusFactory.Create(type, this.Data, remainingTurn, waitTurn));
			this.NotifyCharacterDataObservers();

			return result;
		}

		public void RemoveAbnormalStatus(GameDefine.AbnormalStatusType type)
		{
			this.Data.RemoveAbnormalStatus(type);
			this.NotifyCharacterDataObservers();
		}

		public GameDefine.AcquireItemResultType AddItemOnClickCell(Item item, CellData cellData)
		{
			if(this.Data.Inventory.CanAddItem(item))
			{
				this.AddItem(item);
				return GameDefine.AcquireItemResultType.Acquired;
			}
			else
			{
				this.Data.Inventory.SetExchangeItem(item, cellData);
				this.OpenInventoryUI(GameDefine.InventoryModeType.Exchange, this.Data.Inventory);
				DescriptionManager.Instance.DeployEmergency("ExchangeItem", item.InstanceData.ItemName);
				return GameDefine.AcquireItemResultType.Full;
			}
		}

		public bool AddItem(Item item)
		{
			var result = this.Data.Inventory.AddItem(item);
			if(result)
			{
				InformationManager.OnAcquiredItem(item.InstanceData.ItemName);
				if(MineS.SaveData.Option.AutoSort)
				{
					this.SortItem();
				}
			}

			this.Serialize();

			return result;
		}

		public void AddOpenInventoryUIEvent(UnityAction<Inventory> call)
		{
			this.onOpenInventoryUI.AddListener(call);
		}

		public void AddCloseInventoryUIEvent(UnityAction<Inventory> call)
		{
			this.onCloseInventoryUI.AddListener(call);
		}

		public void Giveup()
		{
			this.Data.Giveup();
		}

		public void LoadData()
		{
			if(HK.Framework.SaveData.ContainsKey(PlayerDataSerializeKeyName))
			{
				this.Data = PlayerData.Deserialize(PlayerDataSerializeKeyName, this.cellController);
			}
			else
			{
				this.Data = new PlayerData(this.playerInitialStatus, this.growthData, this.cellController);
			}
			this.NotifyCharacterDataObservers();
		}

		public void Serialize()
		{
			this.Data.Serialize(PlayerDataSerializeKeyName);
		}

		public void DebugAddAbnormalStatus(int type)
		{
			this.Data.AddAbnormalStatus(AbnormalStatusFactory.Create((GameDefine.AbnormalStatusType)type, this.Data, DebugManager.Instance.AbnormalStatusRemainingTurn, 0));
			this.NotifyCharacterDataObservers();
		}

		public void DebugRecoveryHitPointFull()
		{
			this.Data.RecoveryHitPoint(999, true);
			this.NotifyCharacterDataObservers();
		}

		public void DebugRecoveryHitPoint()
		{
			this.Data.RecoveryHitPoint(999, false);
			this.NotifyCharacterDataObservers();
		}

		public void DebugRecoveryHitPointDying()
		{
			this.Data.RecoveryHitPoint(-this.Data.HitPoint + 1, false);
			this.NotifyCharacterDataObservers();
		}

		public void DebugRecoveryArmor()
		{
			this.Data.RecoveryArmor(999, true);
			this.NotifyCharacterDataObservers();
		}

		public void DebugZeroArmor()
		{
			this.Data.RecoveryArmor(-this.Data.Armor, true);
			this.NotifyCharacterDataObservers();
		}

		public void DebugAddMoney(int value)
		{
			this.AddMoney(value, true);
			this.NotifyCharacterDataObservers();
		}

		public void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.Data.OnTurnProgress(type, turnCount);
		}

		public void RemoveSaveData()
		{
			this.Data.Inventory.RemoveAll();
			this.Data.AddMoney(-this.Data.Money);
		}

		private void OnLateTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.NotifyCharacterDataObservers();
		}

		private void OnUseItem(Item item)
		{
			this.NotifyCharacterDataObservers();
		}

		private void OnNextFloor()
		{
			this.Data.RecoveryArmor(this.Data.ArmorMax / 2, false);
			this.NotifyCharacterDataObservers();
		}

		private void OnChangeDungeon()
		{
			this.Data.OnChangeDungeon();
			this.NotifyCharacterDataObservers();
		}
	}
}