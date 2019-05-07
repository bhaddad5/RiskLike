using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTypesLookup : MonoBehaviour
{
	private static Dictionary<string, StoredUnitTypeData> unitLookup = new Dictionary<string, StoredUnitTypeData>();
	public List<StoredUnitTypeData> units = new List<StoredUnitTypeData>();

	void Awake()
	{
		foreach (StoredUnitTypeData unit in units)
		{
			unitLookup[unit.Id] = unit;
		}
	}

	public static StoredUnitTypeData GetUnitTypeDataById(string id)
	{
		return unitLookup[id];
	}
}
