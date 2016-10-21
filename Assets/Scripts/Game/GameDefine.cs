﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using System;
using System.Linq;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class GameDefine
	{
		public enum EventType:int
		{
			/// <summary>
			/// 無し.
			/// </summary>
			None,

			/// <summary>
			/// 階段.
			/// </summary>
			Stair,

			/// <summary>
			/// 回復アイテム.
			/// </summary>
			RecoveryItem,

			/// <summary>
			/// 敵.
			/// </summary>
			Enemy,

			/// <summary>
			/// アイテム.
			/// </summary>
			Item,

			/// <summary>
			/// 罠.
			/// </summary>
			Trap,

			/// <summary>
			/// 鍛冶屋.
			/// </summary>
			BlackSmith,

			/// <summary>
			/// 店.
			/// </summary>
			Shop,
		}

		/// <summary>
		/// アクション実行タイプ.
		/// </summary>
		public enum ActionableType:int
		{
			/// <summary>
			/// アクション可能.
			/// </summary>
			OK,
			/// <summary>
			/// まだ踏めない.
			/// </summary>
			NotStep,
			/// <summary>
			/// ロックされている.
			/// </summary>
			Lock,
		}

		public enum AcquireItemResultType:int
		{
			/// <summary>
			/// 取得した.
			/// </summary>
			Acquired,

			/// <summary>
			/// インベントリに空きが無かった.
			/// </summary>
			Full,
		}

		public enum AdjacentType:int
		{
			Left,
			LeftTop,
			Top,
			RightTop,
			Right,
			RightBottom,
			Bottom,
			LeftBottom,
		}

		public enum RareType:int
		{
			Normal,
			Rare,
			SuperRare,
			Legend,
		}

		public enum ItemType:int
		{
			UsableItem,
			Weapon,
			Shield,
			Accessory,
		}

		public enum UsableItemType:int
		{
			/// <summary>
			/// ヒットポイント回復.
			/// </summary>
			RecoveryHitPointLimit,

			/// <summary>
			/// アーマー回復.
			/// </summary>
			RecoveryArmor,

			/// <summary>
			/// 状態異常を付与する.
			/// </summary>
			AddAbnormalStatus,

			/// <summary>
			/// 状態異常を削除する.
			/// </summary>
			RemoveAbnormalStatus,

			/// <summary>
			/// 対象にダメージを与える.
			/// </summary>
			Damage,

			/// <summary>
			/// 全てのセルを表示する.
			/// </summary>
			NailDown,

			/// <summary>
			/// 罠を全て解除する.
			/// </summary>
			CallingOff,

			/// <summary>
			/// 対象のヒットポイントを吸収する.
			/// </summary>
			Drain,

			/// <summary>
			/// 対象のアーマーを0にする.
			/// </summary>
			Brake,

			/// <summary>
			/// 最大攻撃力が増加する.
			/// </summary>
			UndineDrop,

			/// <summary>
			/// 最大ヒットポイントが増加する.
			/// </summary>
			UndineTear,

			/// <summary>
			/// レベルが上がる.
			/// </summary>
			UndineBlood,

			/// <summary>
			/// 敵に投げるとアイテムになる.
			/// </summary>
			Alchemy,

			/// <summary>
			/// 使うと敵をおびき寄せる. 敵に投げると分裂する.
			/// </summary>
			Actinidia,

			/// <summary>
			/// 1階層戻る.
			/// </summary>
			TurnBack,

			/// <summary>
			/// 1階層進む.
			/// </summary>
			Proceed,

			/// <summary>
			/// 最大攻撃力が減少する.
			/// </summary>
			OndineDrop,

			/// <summary>
			/// 最大ヒットポイントが減少する.
			/// </summary>
			OndineTear,

			/// <summary>
			/// レベルが下がる.
			/// </summary>
			OndineBlood,
		}

		public enum AbnormalStatusType:int
		{
			/// <summary>
			/// 無し.
			/// </summary>
			None = 0,

			/// <summary>
			/// 再生：ヒットポイントが徐々に回復する.
			/// </summary>
			Regeneration = 1,

			/// <summary>
			/// 鋭利：与えるダメージを2倍にする.
			/// </summary>
			Sharpness = 2,

			/// <summary>
			/// 硬化：受けるダメージを半分にする.
			/// </summary>
			Curing = 3,

			/// <summary>
			/// 透視：未識別のセルを透視する.
			/// </summary>
			Xray = 4,

			/// <summary>
			/// 罠師：罠の影響を受けない.
			/// </summary>
			TrapMaster = 5,

			/// <summary>
			/// 幸福：取得経験値が2倍になる.
			/// </summary>
			Happiness = 6,

			/// <summary>
			/// 毒：ターン終了時にダメージを受ける.
			/// </summary>
			Poison = 7,

			/// <summary>
			/// 呆け：取得経験値が半分になる.
			/// </summary>
			Blur = 8,

			/// <summary>
			/// 負傷：受けるダメージが2倍になる.
			/// </summary>
			Gout = 9,

			/// <summary>
			/// 鈍ら：与えるダメージが半分になる.
			/// </summary>
			Dull = 10,

			/// <summary>
			/// 恐怖：攻撃できなくなる.
			/// </summary>
			Fear = 11,

			/// <summary>
			/// 封印：特殊能力が発動しなくなる.
			/// </summary>
			Seal = 12,

			/// <summary>
			/// 混乱：ターンを消費する行動がランダムに行われてしまう.
			/// </summary>
			Confusion = 13,
		}

		public enum AbilityType:int
		{
			/// <summary>
			/// 無し.
			/// </summary>
			[EnumLabel("無し")]
			None,

			/// <summary>
			/// 貫通：アーマーを無視して攻撃する.
			/// </summary>
			[EnumLabel("貫通：アーマーを無視して攻撃する")]
			Penetoration,

			/// <summary>
			/// 慈悲：ターン終了時に他の敵のHPを回復する.
			/// </summary>
			[EnumLabel("慈悲：ターン終了時に他の敵のHPを回復する")]
			Mercy,

			/// <summary>
			/// 自己再生：ターン終了時にHPを回復する.
			/// </summary>
			[EnumLabel("自己再生：ターン終了時にHPを回復する")]
			Regeneration,

			/// <summary>
			/// 跳返：受けたダメージの半分を相手に返す.
			/// </summary>
			[EnumLabel("跳返：受けたダメージの半分を相手に返す")]
			Splash,

			/// <summary>
			/// 吸収：与えたダメージの半分を吸収する.
			/// </summary>
			[EnumLabel("吸収：与えたダメージの半分を吸収する")]
			Absorption,

			/// <summary>
			/// 遠距離攻撃：ターン終了時に攻撃する.
			/// </summary>
			[EnumLabel("遠距離攻撃：ターン終了時に攻撃する")]
			LongRangeAttack,

			/// <summary>
			/// 三子教訓：他の敵の数だけ攻撃力が上昇する.
			/// </summary>
			[EnumLabel("三子教訓：他の敵の数だけ攻撃力が上昇する")]
			Reinforcement,

			/// <summary>
			/// 連撃：攻撃した際に他に敵がいる場合は半分のダメージを与える.
			/// </summary>
			[EnumLabel("連撃：攻撃した際に他に敵がいる場合は半分のダメージを与える")]
			ContinuousAttack,

			/// <summary>
			/// 習熟：取得する経験値が上昇する.
			/// </summary>
			[EnumLabel("習熟：取得する経験値が上昇する")]
			Proficiency,

			/// <summary>
			/// 巻上：取得するお金が上昇する.
			/// </summary>
			[EnumLabel("巻上：取得するお金が上昇する")]
			Hoist,

			/// <summary>
			/// 改修：敵を倒した際にアーマーが回復する.
			/// </summary>
			[EnumLabel("改修：敵を倒した際にアーマーが回復する")]
			Repair,

			/// <summary>
			/// 幸運：敵を倒した際のアイテムドロップ率が上昇する.
			/// </summary>
			[EnumLabel("幸運：敵を倒した際のアイテムドロップ率が上昇する")]
			Fortune,

			/// <summary>
			/// 匠：攻撃力が上昇する.
			/// </summary>
			[EnumLabel("匠：攻撃力が上昇する")]
			Artisan,

			/// <summary>
			/// 精巧：アーマーが上昇する.
			/// </summary>
			[EnumLabel("精巧：アーマーが上昇する")]
			Exquisite,

			/// <summary>
			/// 回避：回避率が上昇する.
			/// </summary>
			[EnumLabel("回避：回避率が上昇する")]
			Evasion,

			/// <summary>
			/// 命中：命中率が上昇する.
			/// </summary>
			[EnumLabel("命中：命中率が上昇する")]
			HitProbability,

			/// <summary>
			/// 五右衛門：与えたダメージから一定数のお金を取得する.
			/// </summary>
			[EnumLabel("五右衛門：与えたダメージから一定数のお金を取得する")]
			Goemon,

			/// <summary>
			/// 窃盗：攻撃した際に道具袋に空きがある場合に一定確率でアイテムを取得する.
			/// </summary>
			[EnumLabel("窃盗：攻撃した際に道具袋に空きがある場合に一定確率でアイテムを取得する")]
			Theft,

			/// <summary>
			/// 必中：攻撃が必ず当たる.
			/// </summary>
			[EnumLabel("必中：攻撃が必ず当たる")]
			InbariablyHit,

			/// <summary>
			/// 捨身：アーマーを消費して攻撃力を上昇する.
			/// </summary>
			[EnumLabel("捨身：アーマーを消費して攻撃力を上昇する")]
			RiskOfLife,

			/// <summary>
			/// 増進：バフの継続ターンを増やす.
			/// </summary>
			[EnumLabel("増進：バフの継続ターンを増やす")]
			Enhancement,

			/// <summary>
			/// 減退：バフの継続ターンが減少する.
			/// </summary>
			[EnumLabel("減退：バフの継続ターンが減少する")]
			Weak,

			/// <summary>
			/// 免疫：デバフの継続ターンを減らす.
			/// </summary>
			[EnumLabel("免疫：デバフの継続ターンを減らす")]
			Immunity,

			/// <summary>
			/// 伝染：デバフの継続ターンを増やす.
			/// </summary>
			[EnumLabel("伝染：デバフの継続ターンを増やす")]
			Infection,

			/// <summary>
			/// 罠避：罠に引っかからなくなる.
			/// </summary>
			[EnumLabel("罠避：罠に引っかからなくなる")]
			AvoidTrap,

			/// <summary>
			/// 薬師：回復系アイテムの効果が上昇する.
			/// </summary>
			[EnumLabel("薬師：回復系アイテムの効果が上昇する")]
			HealingBuddha,

			/// <summary>
			/// 毒塗：ダメージを与えた際に一定確率でデバフの毒を付与する.
			/// </summary>
			[EnumLabel("毒塗：ダメージを与えた際に一定確率でデバフの毒を付与する")]
			PoisonPainted,

			/// <summary>
			/// 放心：ダメージを与えた際に一定確率でデバフの呆けを付与する.
			/// </summary>
			[EnumLabel("放心：ダメージを与えた際に一定確率でデバフの呆けを付与する")]
			Absentmindedness,

			/// <summary>
			/// 秘孔突き：ダメージを与えた際に一定確率でデバフの負傷を付与する.
			/// </summary>
			[EnumLabel("秘孔突き：ダメージを与えた際に一定確率でデバフの負傷を付与する")]
			VitalsPoke,

			/// <summary>
			/// 刃毀：ダメージを与えた際に一定確率でデバフの鈍らを付与する.
			/// </summary>
			[EnumLabel("刃毀：ダメージを与えた際に一定確率でデバフの鈍らを付与する")]
			BladeBroken,

			/// <summary>
			/// 錯乱：ダメージを与えた際に一定確率でデバフの混乱を付与する.
			/// </summary>
			[EnumLabel("錯乱：ダメージを与えた際に一定確率でデバフの混乱を付与する")]
			Derangement,

			/// <summary>
			/// 脅迫：ダメージを与えた際に一定確率でデバフの恐怖を付与する.
			/// </summary>
			[EnumLabel("脅迫：ダメージを与えた際に一定確率でデバフの恐怖を付与する")]
			Intimidation,

			/// <summary>
			/// 呪縛：ダメージを与えた際に一定確率でデバフの封印を付与する.
			/// </summary>
			[EnumLabel("呪縛：ダメージを与えた際に一定確率でデバフの封印を付与する")]
			Curse,

			/// <summary>
			/// 雄叫び：識別状態になった際に他の敵も識別されてしまう（敵のみ）.
			/// </summary>
			[EnumLabel("雄叫び：識別状態になった際に他の敵も識別されてしまう（敵のみ）")]
			WarCry,

			/// <summary>
			/// 回復：ダメージを与えた際にヒットポイントが回復する.
			/// </summary>
			[EnumLabel("回復：ダメージを与えた際にヒットポイントが回復する")]
			Recovery,

			/// <summary>
			/// 防護：受けるダメージを軽減する.
			/// </summary>
			[EnumLabel("防護：受けるダメージを軽減する")]
			Protection,

			/// <summary>
			/// 先手：識別した際にダメージを与える.
			/// </summary>
			[EnumLabel("先手：識別した際にダメージを与える")]
			Initiative,

			/// <summary>
			/// 千里眼：常にバフの透視の効果を得る.
			/// </summary>
			[EnumLabel("千里眼：常にバフの透視の効果を得る")]
			Clairvoyance,

			/// <summary>
			/// 分裂：ダメージを受けた際に同じ敵を何処かに生成する（敵のみ）.
			/// </summary>
			[EnumLabel("分裂：ダメージを受けた際に同じ敵を何処かに生成する（敵のみ）")]
			Division,

			/// <summary>
			/// 召喚：その階数に出現する敵を生成する（敵のみ）.
			/// </summary>
			[EnumLabel("召喚：その階数に出現する敵を生成する（敵のみ）")]
			Summon,

			/// <summary>
			/// 転生：死亡した際にその階数に出現する敵を生成する（敵のみ）.
			/// </summary>
			[EnumLabel("転生：死亡した際にその階数に出現する敵を生成する（敵のみ）")]
			Reincarnation,

			/// <summary>
			/// 挑発：他の敵がダメージを受ける時に肩代わりする.
			/// </summary>
			[EnumLabel("挑発：他の敵がダメージを受ける時に肩代わりする")]
			Provocation,

			/// <summary>
			/// 血清：デバフの毒を受け付けない.
			/// </summary>
			[EnumLabel("血清：デバフの毒を受け付けない")]
			Serum,

			/// <summary>
			/// 集中：デバフの呆けを受け付けない.
			/// </summary>
			[EnumLabel("集中：デバフの呆けを受け付けない")]
			Concentration,

			/// <summary>
			/// 快癒：デバフの負傷を受け付けない.
			/// </summary>
			[EnumLabel("快癒：デバフの負傷を受け付けない")]
			CompleteRecovery,

			/// <summary>
			/// 鍍金：デバフの鈍らを受け付けない.
			/// </summary>
			[EnumLabel("鍍金：デバフの鈍らを受け付けない")]
			Plating,

			/// <summary>
			/// 勇気：デバフの恐怖を受け付けない.
			/// </summary>
			[EnumLabel("勇気：デバフの恐怖を受け付けない")]
			Bravery,

			/// <summary>
			/// 魔除：デバフの封印を受け付けない.
			/// </summary>
			[EnumLabel("魔除：デバフの封印を受け付けない")]
			Talisman,

			/// <summary>
			/// 気付：デバフの混乱を受け付けない.
			/// </summary>
			[EnumLabel("気付：デバフの混乱を受け付けない")]
			Care,
		}

		/// <summary>
		/// ターン経過タイプ.
		/// </summary>
		public enum TurnProgressType:int
		{
			CellClick,

			EnemyAttack,
		}

		/// <summary>
		/// 罠タイプ.
		/// </summary>
		public enum TrapType:int
		{
			Poison,
			Gout,
			Blur,
			Dull,
			Mine,
		}

		public enum AttackResultType:int
		{
			/// <summary>
			/// 当たった.
			/// </summary>
			Hit,

			/// <summary>
			/// 外した.
			/// </summary>
			Miss,

			/// <summary>
			/// デバフの恐怖によって外した.
			/// </summary>
			MissByFear
		}

		public enum AddAbnormalStatusResultType:int
		{
			/// <summary>
			/// 追加した.
			/// </summary>
			Added,

			/// <summary>
			/// 無効化された.
			/// </summary>
			Invalided,

			/// <summary>
			/// 既にある.
			/// </summary>
			AlreadyHave,
		}

		public enum SelectItemDecideType:int
		{
			/// <summary>
			/// 使う.
			/// </summary>
			Use,

			/// <summary>
			/// 装備する.
			/// </summary>
			Equipment,

			/// <summary>
			/// 外す.
			/// </summary>
			Remove,

			/// <summary>
			/// 投げる.
			/// </summary>
			Throw,

			/// <summary>
			/// 置く.
			/// </summary>
			Put,

			/// <summary>
			/// 説明.
			/// </summary>
			Description,

			/// <summary>
			/// キャンセル.
			/// </summary>
			Cancel,

			/// <summary>
			/// 強化する.
			/// </summary>
			Reinforcement,
		}

		public enum InventoryModeType:int
		{
			/// <summary>
			/// 通常の開いているモード.
			/// </summary>
			Use,

			/// <summary>
			/// 交換モード.
			/// </summary>
			Exchange,

			/// <summary>
			/// 鍛冶屋の強化モード.
			/// </summary>
			BlackSmith_Reinforcement,

			/// <summary>
			/// 鍛冶屋の合成モードのベース装備品選択.
			/// </summary>
			BlackSmith_SynthesisSelectBaseEquipment,

			/// <summary>
			/// 鍛冶屋の合成モードの対象装備品選択.
			/// </summary>
			BlackSmith_SynthesisSelectTargetEquipment,

			/// <summary>
			/// 鍛冶屋の焼印消去モードの対象装備品選択.
			/// </summary>
			BlackSmith_RemoveAbilitySelectBaseEquipment,

			/// <summary>
			/// 鍛冶屋の焼印消去モードの対象アビリティ選択.
			/// </summary>
			BlackSmith_RemoveAbilitySelectAbility,

			/// <summary>
			/// 店の購入モード.
			/// </summary>
			Shop_Buy,

			/// <summary>
			/// 店の売却モード.
			/// </summary>
			Shop_Sell,
		}

		public enum CellClickMode:int
		{
			Step,

			PutItem,

			ThrowItem,
		}

		public enum CharacterType:int
		{
			Player,
			Enemy,
		}

		public const int AdjacentMax = 9;

		public const int MoneyMax = 9999999;

		public const int InventoryItemMax = 24;

		public const int ShopInventoryMax = 5;

		public const int RecoveryItemRecovery = 5;

		public const int ArmorMax = 999;

		public const int AbnormalStatusTrapRemainingTurn = 5;

		public const int EquipmentLevelMax = 99;

		public const int CellRowMax = 8;

		public const int CellCulumnMax = 7;

		public static int Lottery<P>(List<P> elements) where P : IProbability
		{
			int probabilityMax = 0;
			for(int i = 0; i < elements.Count; i++)
			{
				probabilityMax += elements[i].Probability;
			}

			int probability = UnityEngine.Random.Range(0, probabilityMax);
			int currentProbability = 0;
			for(int i = 0; i < elements.Count; i++)
			{
				var table = elements[i];
				if(probability >= currentProbability && probability < (currentProbability + table.Probability))
				{
					return i;
				}
				currentProbability += table.Probability;
			}

			Debug.AssertFormat(false, "抽選処理に失敗しました. 計算を間違えている可能性があります.");
			return -1;
		}

		public static bool IsEquipment(ItemType itemType)
		{
			return itemType == ItemType.Accessory
			|| itemType == ItemType.Shield
			|| itemType == ItemType.Weapon;
		}

		public static bool IsBuff(GameDefine.AbnormalStatusType type)
		{
			Debug.AssertFormat(type != AbnormalStatusType.None, "不正な値です.");

			return type == AbnormalStatusType.Regeneration ||
			type == AbnormalStatusType.Sharpness ||
			type == AbnormalStatusType.Curing ||
			type == AbnormalStatusType.Xray ||
			type == AbnormalStatusType.TrapMaster ||
			type == AbnormalStatusType.Happiness;
		}

		public static string GetAbnormalStatusDescriptionKey(GameDefine.AbnormalStatusType type)
		{
			switch(type)
			{
			case AbnormalStatusType.Regeneration:
				return "AbnormalStatusType.Regeneration";
			case AbnormalStatusType.Sharpness:
				return "AbnormalStatusType.Sharpness";
			case AbnormalStatusType.Blur:
				return "AbnormalStatusType.Blur";
			case AbnormalStatusType.Confusion:
				return "AbnormalStatusType.Confusion";
			case AbnormalStatusType.Curing:
				return "AbnormalStatusType.Curing";
			case AbnormalStatusType.Dull:
				return "AbnormalStatusType.Dull";
			case AbnormalStatusType.Fear:
				return "AbnormalStatusType.Fear";
			case AbnormalStatusType.Gout:
				return "AbnormalStatusType.Gout";
			case AbnormalStatusType.Happiness:
				return "AbnormalStatusType.Happiness";
			case AbnormalStatusType.Poison:
				return "AbnormalStatusType.Poison";
			case AbnormalStatusType.Seal:
				return "AbnormalStatusType.Seal";
			case AbnormalStatusType.TrapMaster:
				return "AbnormalStatusType.TrapMaster";
			case AbnormalStatusType.Xray:
				return "AbnormalStatusType.Xray";
			default:
				Debug.AssertFormat(false, "不正な値です. type = {0}", type);
				return "";
			}
		}

#if UNITY_EDITOR
		public static UsableItemType GetUsableItemType(string name)
		{
			var type = typeof(UsableItemType);
			var index = Array.FindIndex(Enum.GetNames(type), u => u.CompareTo(name) == 0);
			return (UsableItemType)Enum.GetValues(type).GetValue(index);
		}
#endif

		public static string GetAbnormalStatusColor(AbnormalStatusType type)
		{
			return IsBuff(type) ? GoodColorCode : BadColorCode;
		}

		public static GameDefine.AbnormalStatusType ConvertTrapTypeToAbnormalStatusType(GameDefine.TrapType type)
		{
			switch(type)
			{
			case GameDefine.TrapType.Poison:
				return GameDefine.AbnormalStatusType.Poison;
			case GameDefine.TrapType.Blur:
				return GameDefine.AbnormalStatusType.Blur;
			case GameDefine.TrapType.Dull:
				return GameDefine.AbnormalStatusType.Dull;
			case GameDefine.TrapType.Gout:
				return GameDefine.AbnormalStatusType.Gout;
			default:
				Debug.AssertFormat(false, "不正な値です. type = {0}", type);
				return GameDefine.AbnormalStatusType.None;
			}
		}

		public static bool IsAbnormalTrap(GameDefine.TrapType type)
		{
			return type == TrapType.Blur ||
			type == TrapType.Dull ||
			type == TrapType.Gout ||
			type == TrapType.Poison;
		}

		public const string GoodColorCode = "#00FFE9";

		public const string BadColorCode = "#FF3333";
	}
}