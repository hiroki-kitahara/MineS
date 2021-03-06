﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class DeployDescriptionOnCharacterData : DeployDescriptionBase
	{
		private CharacterData characterData;

		public DeployDescriptionOnCharacterData(CharacterData characterData)
		{
			this.characterData = characterData;
		}

		public override void Deploy()
		{
			DescriptionManager.Instance.Deploy(this.characterData);
			//var cellData = EnemyManager.Instance.InEnemyCells[(this.characterData as EnemyData)];
			//Debug.LogFormat("Position[{0}, {1}]", cellData.Position.y, cellData.Position.x);
		}
	}
}