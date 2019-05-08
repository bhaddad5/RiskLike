using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoredUnitTypeData
{
	public string Id;
	public string TypeName;

	public float Attack;
	public float Defense;
	public float Movement;

	public float Health;

	public string PrefabId;
}
