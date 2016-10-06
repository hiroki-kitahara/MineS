﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class PlayerManager : SingletonMonoBehaviour<PlayerManager>, ITurnProgress
	{
		[SerializeField]
		private List<CharacterDataObserver> characterDataObservers;

		[SerializeField]
		private List<InventoryObserver> inventoryObservers;

		[SerializeField]
		private List<ItemObserver> selectItemObservers;

		[SerializeField]
		private GameObject inventoryUI;

		[SerializeField]
		private GameObject confirmSelectItemUI;

		[SerializeField]
		private ExperienceData experienceData;

		[SerializeField]
		private CharacterMasterData playerInitialStatus;

		[SerializeField]
		private CharacterMasterData growthData;

		public PlayerData Data{ private set; get; }

		public ExperienceData ExperienceData{ get { return this.experienceData; } }

		protected override void Awake()
		{
			base.Awake();
			this.Data = new PlayerData(this.playerInitialStatus, this.growthData);
		}

		void Start()
		{
			this.NotifyCharacterDataObservers();
			TurnManager.Instance.AddEndTurnEvent(this.OnTurnProgress);
			TurnManager.Instance.AddLateEndTurnEvent(this.OnLateTurnProgress);
		}

		public void RecoveryHitPoint(int value, bool isLimit)
		{
			this.Data.RecoveryHitPoint(value, isLimit);
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
		}

		public void ChangeItem(Item before, Item after)
		{
			this.Data.Inventory.ChangeItem(before, after);
		}

		public void AddMoney(int value)
		{
			this.Data.AddMoney(Calculator.GetFinalMoney(value, this.Data));
			this.NotifyCharacterDataObservers();
		}

		public void NotifyCharacterDataObservers()
		{
			for(int i = 0; i < this.characterDataObservers.Count; i++)
			{
				this.characterDataObservers[i].ModifiedData(this.Data);
			}
		}

		public void OpenInventoryUI()
		{
			this.inventoryUI.SetActive(true);
			this.UpdateInventoryUI();
		}

		public void CloseInventoryUI()
		{
			this.Data.Inventory.SetExchangeItem(null, null);
			this.inventoryUI.SetActive(false);
		}

		public void SelectItem(Item item)
		{
			this.Data.Inventory.SetSelectItem(item);
			this.confirmSelectItemUI.SetActive(true);
			this.selectItemObservers.ForEach(i => i.ModifiedData(item));
		}

		public void CloseConfirmSelectItemUI()
		{
			this.Data.Inventory.SetSelectItem(null);
			this.confirmSelectItemUI.SetActive(false);
		}

		public void UpdateInventoryUI()
		{
			this.inventoryObservers.ForEach(i => i.ModifiedData(this.Data.Inventory));
		}

		public void AddAbnormalStatus(GameDefine.AbnormalStatusType type, int remainingTurn, int waitTurn)
		{
			this.Data.AddAbnormalStatus(AbnormalStatusFactory.Create(type, this.Data, remainingTurn, waitTurn));
			this.NotifyCharacterDataObservers();
		}

		public GameDefine.AcquireItemResultType AddItem(Item item, CellData cellData)
		{
			if(this.Data.Inventory.IsFreeSpace)
			{
				this.Data.Inventory.AddItem(item);
				InformationManager.OnAcquiredItem(item.InstanceData.ItemName);
				return GameDefine.AcquireItemResultType.Acquired;
			}
			else
			{
				this.Data.Inventory.SetExchangeItem(item, cellData);
				PlayerManager.Instance.OpenInventoryUI();
				DescriptionManager.Instance.DeployEmergency("ExchangeItem");
				return GameDefine.AcquireItemResultType.Full;
			}
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
			this.Data.RecoveryArmor(999);
			this.NotifyCharacterDataObservers();
		}

		public void DebugZeroArmor()
		{
			this.Data.RecoveryArmor(-this.Data.Armor);
			this.NotifyCharacterDataObservers();
		}

		public void OnTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.Data.OnTurnProgress(type, turnCount);
		}

		private void OnLateTurnProgress(GameDefine.TurnProgressType type, int turnCount)
		{
			this.NotifyCharacterDataObservers();
		}
	}
}