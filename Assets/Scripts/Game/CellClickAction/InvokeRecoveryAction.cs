﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace MineS
{
	/// <summary>
	/// .
	/// </summary>
	public class InvokeRecoveryAction : CellClickActionBase
	{
		public override void Invoke(CellData data)
		{
			data.Controller.SetDebugText("");
			data.BindRidingObjectAction(null);
			Debug.Log("Invoke Recovery!");
		}
	}
}