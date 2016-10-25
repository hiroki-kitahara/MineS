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
	[System.Serializable]
	public class EquipmentInstanceData : ItemInstanceDataBase
	{
		[SerializeField]
		private int basePower;

		[SerializeField]
		private int brandingLimit;

		/// <summary>
		/// 抽出可能か.
		/// </summary>
		[SerializeField]
		private bool canExtraction;

		[SerializeField]
		private GameDefine.ItemType itemType;

		[SerializeField, EnumLabel("Ability", typeof(GameDefine.AbilityType))]
		public List<GameDefine.AbilityType> abilities;

		[SerializeField]
		private int level;

		public int Level{ get { return this.level; } }

		public int Power
		{
			get
			{
				return this.basePower + this.Level;
			}
		}

		public List<AbilityBase> Abilities{ private set; get; }

		public override string ItemName
		{
			get
			{
				if(this.Level <= 0)
				{
					return base.ItemName;
				}

				return ItemManager.Instance.equipmentRevisedLevelName.Element.Format(base.ItemName, this.Level);
			}
		}

		public override GameDefine.ItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		public bool CanExtraction
		{
			get
			{
				return this.canExtraction;
			}
		}

		public EquipmentInstanceData(ItemDataBase masterData)
		{
			base.InternalCreateFromMasterData(this, masterData);
			var equipmentData = masterData as EquipmentData;
			this.basePower = equipmentData.BasePower;
			this.brandingLimit = equipmentData.BrandingLimit;
			this.canExtraction = equipmentData.CanExtraction;
			this.itemType = equipmentData.ItemType;
			this.abilities = new List<GameDefine.AbilityType>(equipmentData.abilities);
			this.level = 0;
			this.Abilities = AbilityFactory.Create(this.abilities, null);
		}

		public EquipmentInstanceData()
		{
			
		}

		public void InitializeAbilities()
		{
			this.Abilities = AbilityFactory.Create(this.abilities, null);
		}

		public void SetAbilitiesHolder(CharacterData holder)
		{
			this.Abilities.ForEach(a => a.SetHolder(holder));
		}

		public void Synthesis(Item target)
		{
			var targetEquipmentData = target.InstanceData as EquipmentInstanceData;
			this.Abilities.AddRange(targetEquipmentData.Abilities);
			if(this.Abilities.Count > this.brandingLimit)
			{
				this.Abilities.RemoveRange(this.brandingLimit, this.Abilities.Count - this.brandingLimit);
			}
		}

		public bool CanRemoveAbility(int index)
		{
			return index >= (this.MasterData as EquipmentData).abilities.Count;
		}

		public void RemoveAbility(int index)
		{
			this.Abilities.RemoveAt(index);
		}

		public void LevelUp()
		{
			this.level++;
		}

		public bool CanLevelUp
		{
			get
			{
				return this.Level < GameDefine.EquipmentLevelMax;
			}
		}

		public bool CanSynthesis
		{
			get
			{
				return this.Abilities.Count < this.brandingLimit;
			}
		}

		public bool ExistBranding
		{
			get
			{
				return this.Abilities.Count > 0;
			}
		}

		public int NeedLevelUpMoney
		{
			get
			{
				return Mathf.FloorToInt((this.purchasePrice / 10) * (this.Level + 1));
			}
		}

	}
}