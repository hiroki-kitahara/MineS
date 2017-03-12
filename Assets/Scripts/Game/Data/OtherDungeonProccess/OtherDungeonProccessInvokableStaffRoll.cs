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
	public class OtherDungeonProccessInvokableStaffRoll : OtherDungeonProccessBase
	{
		public override void Invoke()
		{
            StaffRollManager.Instance.Invokable();
        }
	}
}