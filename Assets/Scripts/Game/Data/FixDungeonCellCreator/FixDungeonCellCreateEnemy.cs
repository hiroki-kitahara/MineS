﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	[CreateAssetMenu()]
	public class FixDungeonCellCreateEnemy : FixDungeonCellCreatorBase
	{
		[SerializeField]
		private CharacterMasterData masterData;

		[SerializeField]
		private ConditionScriptableObjectBase condition;

		public override CellData Create(int y, int x, CellController cellController, MapChipCreatorBase mapChipCreator)
		{
			var cellData = new CellData(y, x, mapChipCreator.Get(y, x), cellController);

			if(!this.condition.Condition)
			{
				return cellData;
			}

			cellData.BindCellClickAction(new CreateEnemyAction(this.masterData));

			return cellData;
		}
	}
}