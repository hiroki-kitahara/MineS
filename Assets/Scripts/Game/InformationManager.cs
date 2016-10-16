﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.UI;
using System.Collections;
using System.Text;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InformationManager : SingletonMonoBehaviour<InformationManager>
	{
		[SerializeField]
		private Text text;

		[SerializeField]
		private Transform parent;

		[SerializeField]
		private InformationElement prefabInformation;

		[SerializeField]
		private StringAsset.Finder
			onAttack = null,
			onMiss = null, onMissByFear = null,
			onDefeatByPlayer = null,
			onVisibleEnemy = null,
			onLevelUpPlayer = null,
			onContinuousAttack = null,
			onAcquiredItem = null,
			onRecovery = null,
			onInitiativeDamage = null,
			onUseRecoveryHitPointItem = null,
			onUseRecoveryArmorItem = null,
			onUseAddAbnormalStatusItem = null,
			onUseRemoveAbnormalStatusItem = null,
			onUseDamageItem = null,
			onAlsoAddAbnormalStatus = null,
			invalidateAddAbnormalStatus = null;

		private Queue<string> messageQueue = new Queue<string>();

		private Coroutine currentMessageCoroutine = null;

		private const string AttackerColor = "attackerColor";

		private const string ReceiverColor = "receiverColor";

		private const string TargetColor = "targetColor";

		private const string AbnormalStatusColor = "abnormalStatusColor";

		void Start()
		{
			DungeonManager.Instance.AddNextFloorEvent(this.OnNextFloor);
		}

		public static void OnAttack(IAttack attacker, IAttack receiver, int damage)
		{
			if(OptionManager.Instance.Data.isFewMessage)
			{
				return;
			}
			var instance = InformationManager.Instance;
			var message = instance.onAttack.Format(attacker.Name, receiver.Name, damage)
				.Replace(AttackerColor, attacker.ColorCode)
				.Replace(ReceiverColor, receiver.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnMiss(IAttack attacker)
		{
			var instance = InformationManager.Instance;
			var message = instance.onMiss.Format(attacker.Name).Replace(AttackerColor, attacker.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnMissByFear(IAttack attacker)
		{
			var instance = InformationManager.Instance;
			var message = instance.onMissByFear.Format(attacker.Name).Replace(AttackerColor, attacker.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnDefeat(IAttack target)
		{
			var instance = InformationManager.Instance;
			var message = instance.onDefeatByPlayer.Format(target.Name, target.Experience, target.Money).Replace(TargetColor, target.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnVisibleEnemy(IAttack enemy)
		{
			var instance = InformationManager.Instance;
			var message = instance.onVisibleEnemy.Format(enemy.Name).Replace(TargetColor, enemy.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnLevelUpPlayer(IAttack attacker, int level)
		{
			var instance = InformationManager.Instance;
			var message = instance.onLevelUpPlayer.Format(attacker.Name, level).Replace(TargetColor, attacker.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnContinuousAttack(IAttack attacker, IAttack receiver, int damage)
		{
			var instance = InformationManager.Instance;
			var message = instance.onContinuousAttack.Format(receiver.Name, damage)
				.Replace(AttackerColor, attacker.ColorCode)
				.Replace(ReceiverColor, receiver.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnAcquiredItem(string itemName)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onAcquiredItem.Format(itemName));
		}

		public static void OnRecovery(int value)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(instance.onRecovery.Format(value));
		}

		public static void OnInitiativeDamage(IAttack attacker, IAttack receiver, int damage)
		{
			var instance = InformationManager.Instance;
			var message = instance.onInitiativeDamage.Format(receiver.Name, damage)
				.Replace(AttackerColor, attacker.ColorCode)
				.Replace(ReceiverColor, receiver.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnUseRecoveryHitPointItem(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.onUseRecoveryHitPointItem.Format(user.Name, value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnUseRecoveryArmorItem(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.onUseRecoveryArmorItem.Format(user.Name, value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnUseAddAbnormalStatusItem(IAttack user, GameDefine.AbnormalStatusType abnormalStatusType, GameDefine.AddAbnormalStatusResultType addResult)
		{
			var descriptionKey = GameDefine.GetAbnormalStatusDescriptionKey(abnormalStatusType);
			var descriptionData = DescriptionManager.Instance.Data.Get(descriptionKey);
			var instance = InformationManager.Instance;
			var onUseMessage = instance.onUseAddAbnormalStatusItem.Format(user.Name, descriptionData.Title)
				.Replace(TargetColor, user.ColorCode)
				.Replace(AbnormalStatusColor, GameDefine.GetAbnormalStatusColor(abnormalStatusType));
			instance._AddMessage(onUseMessage);

			if(addResult == GameDefine.AddAbnormalStatusResultType.Invalided)
			{
				instance._AddMessage(instance.invalidateAddAbnormalStatus.Get);
			}
		}

		public static void OnUseRemoveAbnormalStatusItem(IAttack user, GameDefine.AbnormalStatusType abnormalStatusType)
		{
			var descriptionKey = GameDefine.GetAbnormalStatusDescriptionKey(abnormalStatusType);
			var descriptionData = DescriptionManager.Instance.Data.Get(descriptionKey);
			var instance = InformationManager.Instance;
			var message = instance.onUseRemoveAbnormalStatusItem.Format(user.Name, descriptionData.Title)
				.Replace(TargetColor, user.ColorCode)
				.Replace(AbnormalStatusColor, GameDefine.GetAbnormalStatusColor(abnormalStatusType));
			instance._AddMessage(message);
		}

		public static void OnUseDamageItem(IAttack user, int value)
		{
			var instance = InformationManager.Instance;
			var message = instance.onUseDamageItem.Format(user.Name, value)
				.Replace(TargetColor, user.ColorCode);
			instance._AddMessage(message);
		}

		public static void OnAlsoAddAbnormalStatus(GameDefine.AbnormalStatusType abnormalStatusType)
		{
			var descriptionKey = GameDefine.GetAbnormalStatusDescriptionKey(abnormalStatusType);
			var descriptionData = DescriptionManager.Instance.Data.Get(descriptionKey);
			var instance = InformationManager.Instance;
			var message = instance.onAlsoAddAbnormalStatus.Format(descriptionData.Title)
				.Replace(AbnormalStatusColor, GameDefine.GetAbnormalStatusColor(abnormalStatusType));
			instance._AddMessage(message);
		}

		public static void AddMessage(string message)
		{
			var instance = InformationManager.Instance;
			instance._AddMessage(message);
		}

		private void _AddMessage(string message)
		{
			if(this.currentMessageCoroutine == null)
			{
				this.currentMessageCoroutine = StartCoroutine(this.AddMessageCoroutine(message));
			}
			else
			{
				this.messageQueue.Enqueue(message);
			}
		}

		private IEnumerator AddMessageCoroutine(string message)
		{
			var informationElement = Instantiate(this.prefabInformation, this.parent, false) as InformationElement;
			informationElement.Initialize(message);
			yield return new WaitForSeconds(1.0f);

			if(this.messageQueue.Count > 0)
			{
				this.currentMessageCoroutine = StartCoroutine(this.AddMessageCoroutine(this.messageQueue.Dequeue()));
			}
			else
			{
				this.currentMessageCoroutine = null;
			}
		}

		private void OnNextFloor()
		{
			if(this.currentMessageCoroutine != null)
			{
				StopCoroutine(this.currentMessageCoroutine);
			}
			this.messageQueue.Clear();
			this.currentMessageCoroutine = null;
			InformationElement.RemoveAll();
		}
	}
}