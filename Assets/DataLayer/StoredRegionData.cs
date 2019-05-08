using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoredRegionData
{
	public string Name;
	public Color Color;

	public bool Passable;
	public float MoveDifficulty = 1;

	public List<StoredIndividualUnitData> ContainedUnits;
}
