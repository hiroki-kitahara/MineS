﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections.Generic;
using HK.Framework;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class CellController : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField]
		private GameObject notStepObject;

		[SerializeField]
		private GameObject identificationObject;

		[SerializeField]
		private GameObject lockObject;

		[SerializeField]
		private CharacterDataObserver characterDataObserver;

		[SerializeField]
		private ItemObserver itemObserver;

		[SerializeField]
		private DescriptionDataObserver descriptionDataObserver;

		[SerializeField]
		private GameObject hitPointObject;

		[SerializeField]
		private GameObject strengthObject;

		[SerializeField]
		private GameObject armorObject;

		[SerializeField]
		private Image image;

		[SerializeField]
		private Text text;

		[SerializeField]
		private DamageEffectCreator damageEffectCreator;

		[SerializeField]
		private Image mapChip;

		[SerializeField]
		private bool canSwipeAction;

		private Tweener imageShakeTweener = null;

        private GameObject buffEffect = null;

        private GameObject debuffEffect = null;

        public CellData Data{ private set; get; }

		public CharacterDataObserver CharacterDataObserver{ get { return this.characterDataObserver; } }

		public DamageEffectCreator DamageEffectCreator{ get { return this.damageEffectCreator; } }

		private Coroutine deployDescriptionCoroutine = null;

		private const float WaitDeployDescriptionTime = 0.5f;

	    void Start()
	    {
	        DungeonManager.Instance.AddNextFloorEvent(this.OnNextFloor);
	    }

#region IPointerDownHandler implementation

		public void OnPointerDown(PointerEventData eventData)
		{
			this.deployDescriptionCoroutine = StartCoroutine(this.DeployDescription());
		}

#endregion

#region IPointerClickHandler implementation

		public void OnPointerClick(PointerEventData eventData)
		{
			if(this.deployDescriptionCoroutine == null)
			{
				return;
			}

			StopCoroutine(this.deployDescriptionCoroutine);
			this.deployDescriptionCoroutine = null;
			this.Action();
		}

#endregion

#region IPointerEnterHandler implementation

		public void OnPointerEnter(PointerEventData eventData)
		{
			if(HK.Framework.Input.IsTouchDown || !HK.Framework.Input.IsTouch || CellManager.Instance.AnyOccurredEvent || !this.canSwipeAction)
			{
				return;
			}
			this.Action();
		}

#endregion

#region IPointerExitHandler implementation

		public void OnPointerExit(PointerEventData eventData)
		{
			if(this.deployDescriptionCoroutine == null)
			{
				return;
			}

			StopCoroutine(this.deployDescriptionCoroutine);
		}

#endregion

		private IEnumerator DeployDescription()
		{
			yield return new WaitForSecondsRealtime(WaitDeployDescriptionTime);

			this.Description();
			this.deployDescriptionCoroutine = null;
		}


		public void SetCellData(CellData data)
		{
		    this.DestroyBuffDebuffEffect();
			this.SetActiveStatusObject(false);
			this.SetText("");
			this.SetImage(null);
			this.Data = data;
			this.Data.BindEvent(
				this.Infeasible,
				this.ModifiedCanStep,
				this.ModifiedIdentification,
				this.ModifiedLockCount
			);

			this.Data.Setup();
		}

	    public void Miss()
	    {
	        this.damageEffectCreator.CreateMiss(this.transform.position, CanvasManager.Instance.EffectLv1.transform);
	    }

		public void TakeDamage(int damage)
		{
			this.damageEffectCreator.CreateAsDamage(damage, this.transform.position, CanvasManager.Instance.EffectLv1.transform);
			if(this.imageShakeTweener != null)
			{
				this.imageShakeTweener.Kill();
			}
			this.imageShakeTweener = DOTween.Shake(() => this.image.transform.localPosition, x => this.image.transform.localPosition = x, 0.5f, 20, 90).OnKill(() => this.image.transform.localPosition = Vector3.zero);
		}

		public void Recovery(int value)
		{
			this.damageEffectCreator.CreateAsRecovery(value, this.transform.position, CanvasManager.Instance.EffectLv1.transform);
		}

		public void ForceRemoveImageShake()
		{
			this.imageShakeTweener.Kill();
			this.imageShakeTweener = null;
		}

		public void OnAddedAbnormalStatus(AbnormalStatusBase newAbnormalStatus)
		{
			if(GameDefine.IsBuff(newAbnormalStatus.Type))
			{
                this.CreateBuffEffect();
            }
			else
			{
                this.CreateDebuffEffect();
            }
		}

		public void OnRemovedAbnormalStatus(CharacterData characterData)
		{
		    if (!characterData.IsAnyBuff)
		    {
		        Destroy(this.buffEffect);
		    }
		    if (!characterData.IsAnyDebuff)
		    {
		        Destroy(this.debuffEffect);
		    }
		}

		public void CreateBuffEffect()
		{
			if(this.buffEffect != null)
			{
                return;
            }

            this.buffEffect = Object.Instantiate(EffectManager.Instance.prefabBuffEffect.Element, this.transform, false);
		}

		public void CreateDebuffEffect()
		{
			if(this.debuffEffect != null)
			{
                return;
            }

            this.debuffEffect = Object.Instantiate(EffectManager.Instance.prefabDebuffEffect.Element, this.transform, false);
		}

	    public void DestroyBuffDebuffEffect()
	    {
	        Destroy(this.buffEffect);
	        Destroy(this.debuffEffect);
	    }

		public void Action()
		{
			if(this.Data == null)
			{
				Debug.LogWarning("CellDataがありません.");
				return;
			}

			switch(CellManager.Instance.ClickMode)
			{
			case GameDefine.CellClickMode.Step:
				this.OnStepModeAction();
			break;
			case GameDefine.CellClickMode.PutItem:
				this.OnPutItemModeAction();
			break;
			case GameDefine.CellClickMode.ThrowItem:
				this.OnThrowItemModeAction();
			break;
			default:
				Debug.AssertFormat(false, "不正な値です. ClickMode = {0}", CellManager.Instance.ClickMode);
			break;
			}
		}

		private void OnStepModeAction()
		{
			if(PlayerManager.Instance.Data.FindAbnormalStatus(GameDefine.AbnormalStatusType.Confusion) && (!this.Data.IsIdentification || this.Data.CurrentEventType == GameDefine.EventType.Enemy))
			{
				CellManager.Instance.ActionFromConfusion();
			}
			else
			{
				this.Data.Action();
			}
		}

		private void OnPutItemModeAction()
		{
			if(this.Data.CurrentEventType != GameDefine.EventType.None || this.Data.IsLock || !this.Data.IsIdentification || !this.Data.CanStep)
			{
				DescriptionManager.Instance.DeployEmergency("CanNotPutItem");
				return;
			}

			var playerManager = PlayerManager.Instance;
			var inventory = playerManager.Data.Inventory;
			this.Data.BindCellClickAction(new AcquireItemAction(inventory.SelectItem));
			this.Data.BindDeployDescription(new DeployDescriptionOnItem(inventory.SelectItem));
			playerManager.RemoveInventoryItemOrEquipment(inventory.SelectItem);
			inventory.SetSelectItem(null);
			CellManager.Instance.ChangeCellClickMode(GameDefine.CellClickMode.Step);
			playerManager.NotifyCharacterDataObservers();
		}

		private void OnThrowItemModeAction()
		{
			if(this.Data.CurrentEventType != GameDefine.EventType.Enemy || this.Data.IsLock || !this.Data.IsIdentification || !this.Data.CanStep)
			{
				DescriptionManager.Instance.DeployEmergency("CanNotThrowItem");
				return;
			}

			var playerManager = PlayerManager.Instance;
			var inventory = playerManager.Data.Inventory;
			inventory.SelectItem.Use(EnemyManager.Instance.Enemies[this.Data]);
			inventory.SetSelectItem(null);
			CellManager.Instance.ChangeCellClickMode(GameDefine.CellClickMode.Step);
		}

		public void ActionFromConfusion()
		{
			if(this.Data == null)
			{
				Debug.LogWarning("CellDataがありません.");
				return;
			}

			this.Data.Action();
		}

		public void DebugAction()
		{
			this.Data.DebugAction();
		}

		public void Description()
		{
			this.Data.Description();
		}

		public void SetText(string message)
		{
			if(this.text == null)
			{
				return;
			}
			this.text.text = message;
		}

		public void SetImage(Sprite sprite)
		{
			if(this.image == null)
			{
				return;
			}
			this.image.sprite = sprite;
			this.image.enabled = sprite != null;
		}

		public void SetMapChip(Sprite image)
		{
			this.mapChip.sprite = image;
		}

		public void SetActiveStatusObject(bool isActive)
		{
			this.SetActive(this.hitPointObject, isActive);
			this.SetActive(this.armorObject, isActive);
			this.SetActive(this.strengthObject, isActive);
		}

		public void SetCharacterData(CharacterData data)
		{
			this.SetActiveStatusObject(true);
			this.characterDataObserver.ModifiedData(data);
		}

		public void SetItemData(Item item)
		{
			this.itemObserver.ModifiedData(item);
		}

		public void SetDescriptionData(DescriptionData.Element data)
		{
			this.descriptionDataObserver.ModifiedData(data);
		}

		public void SetDescriptionData(string key)
		{
			this.descriptionDataObserver.ModifiedData(key);
		}

		public void CancelDeployDescription()
		{
			if(this.deployDescriptionCoroutine == null)
			{
				return;
			}

			StopCoroutine(this.deployDescriptionCoroutine);
			this.deployDescriptionCoroutine = null;
		}

		private void Infeasible(GameDefine.ActionableType actionableType)
		{
		}

	    private void OnNextFloor()
	    {
	        if (this.damageEffectCreator != null)
	        {
	            this.damageEffectCreator.ForceRemove();
	        }
	    }

		private void ModifiedCanStep(bool canStep)
		{
			this.SetActive(this.notStepObject, !canStep);
		}

		private void ModifiedIdentification(bool isIdentification)
		{
			this.SetActive(this.identificationObject, !isIdentification);
		}

		private void ModifiedLockCount(int lockCount)
		{
			this.SetActive(this.lockObject, lockCount > 0);
		}

		private void SetActive(GameObject gameObject, bool isActive)
		{
			if(gameObject == null)
			{
				return;
			}

			gameObject.SetActive(isActive);
		}
	}
}