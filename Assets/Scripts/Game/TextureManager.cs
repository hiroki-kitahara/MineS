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
	public class TextureManager : SingletonMonoBehaviour<TextureManager>
	{
		[System.Serializable]
		public class AbnormalStatus
		{
			[SerializeField]
			private SerializeFieldGetter.Sprite regeneration;
			[SerializeField]
			private SerializeFieldGetter.Sprite sharpness;
			[SerializeField]
			private SerializeFieldGetter.Sprite curing;
			[SerializeField]
			private SerializeFieldGetter.Sprite xray;
			[SerializeField]
			private SerializeFieldGetter.Sprite trapMaster;
			[SerializeField]
			private SerializeFieldGetter.Sprite happiness;
			[SerializeField]
			private SerializeFieldGetter.Sprite poison;
			[SerializeField]
			private SerializeFieldGetter.Sprite blur;
			[SerializeField]
			private SerializeFieldGetter.Sprite gout;
			[SerializeField]
			private SerializeFieldGetter.Sprite dull;
			[SerializeField]
			private SerializeFieldGetter.Sprite fear;
			[SerializeField]
			private SerializeFieldGetter.Sprite seal;
			[SerializeField]
			private SerializeFieldGetter.Sprite confusion;
			[SerializeField]
			private SerializeFieldGetter.Sprite assumption;

			private AbnormalStatus()
			{
			}

			public Sprite GetIcon(GameDefine.AbnormalStatusType type)
			{
				switch(type)
				{
				case GameDefine.AbnormalStatusType.Regeneration:
					return this.regeneration.Element;
				case GameDefine.AbnormalStatusType.Sharpness:
					return this.sharpness.Element;
				case GameDefine.AbnormalStatusType.Curing:
					return this.curing.Element;
				case GameDefine.AbnormalStatusType.Xray:
					return this.xray.Element;
				case GameDefine.AbnormalStatusType.TrapMaster:
					return this.trapMaster.Element;
				case GameDefine.AbnormalStatusType.Happiness:
					return this.happiness.Element;
				case GameDefine.AbnormalStatusType.Poison:
					return this.poison.Element;
				case GameDefine.AbnormalStatusType.Blur:
					return this.blur.Element;
				case GameDefine.AbnormalStatusType.Gout:
					return this.gout.Element;
				case GameDefine.AbnormalStatusType.Dull:
					return this.dull.Element;
				case GameDefine.AbnormalStatusType.Fear:
					return this.fear.Element;
				case GameDefine.AbnormalStatusType.Seal:
					return this.seal.Element;
				case GameDefine.AbnormalStatusType.Confusion:
					return this.confusion.Element;
				case GameDefine.AbnormalStatusType.Assumption:
					return this.assumption.Element;
				default:
					Debug.AssertFormat(false, "不正な値です. {0}", type);
					return null;
				}
			}
		}

		[System.Serializable]
		public class Trap
		{
			public SerializeFieldGetter.Sprite poison;

			public SerializeFieldGetter.Sprite gout;

			public SerializeFieldGetter.Sprite blur;

			public SerializeFieldGetter.Sprite dull;

			public SerializeFieldGetter.Sprite mine;

			public Sprite Get(GameDefine.AbnormalStatusType type)
			{
				switch(type)
				{
				case GameDefine.AbnormalStatusType.Poison:
					return poison.Element;
				case GameDefine.AbnormalStatusType.Blur:
					return blur.Element;
				case GameDefine.AbnormalStatusType.Dull:
					return dull.Element;
				case GameDefine.AbnormalStatusType.Gout:
					return gout.Element;
				default:
					Debug.AssertFormat(false, "不正な値です. type = {0}", type);
					return null;
				}

			}
		}

		[System.Serializable]
		public class Item
		{
			[SerializeField]
			private SerializeFieldGetter.Sprite usableItem;

			[SerializeField]
			private SerializeFieldGetter.Sprite weapon;

			[SerializeField]
			private SerializeFieldGetter.Sprite shield;

			[SerializeField]
			private SerializeFieldGetter.Sprite accessory;

			public Sprite Get(GameDefine.ItemType type)
			{
				switch(type)
				{
				case GameDefine.ItemType.UsableItem:
					return this.usableItem.Element;
				case GameDefine.ItemType.Weapon:
					return this.weapon.Element;
				case GameDefine.ItemType.Shield:
					return this.shield.Element;
				case GameDefine.ItemType.Accessory:
					return this.accessory.Element;
				default:
					Debug.AssertFormat(false, "不正な値です. type = {0}", type);
					return null;
				}
			}
		}

		public AbnormalStatus abnormalStatus;

		public Trap trap;

		public Item defaultEquipment;

		public SerializeFieldGetter.Sprite blackSmith;

		public SerializeFieldGetter.Sprite shop;

		public SerializeFieldGetter.Sprite wareHouse;

		public SerializeFieldGetter.Sprite publicity;

		public SerializeFieldGetter.Sprite recoveryItem;

		public SerializeFieldGetter.Sprite stairImage;

		public SerializeFieldGetter.Sprite closeStairImage;

		public SerializeFieldGetter.Sprite anvilImage;

		public SerializeFieldGetter.Sprite moneyImage;

		public SerializeFieldGetter.Sprite stoneStatueImage;
	}
}