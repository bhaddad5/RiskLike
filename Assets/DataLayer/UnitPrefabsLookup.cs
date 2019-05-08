using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitPrefab
{
	public string Id;
	public GameObject Prefab;
}

public class UnitPrefabsLookup : MonoBehaviour
{
	private static Dictionary<string, UnitPrefab> prefabsLookup = new Dictionary<string, UnitPrefab>();
	[SerializeField] private List<UnitPrefab> prefabs;

	void Awake()
	{
		foreach (UnitPrefab prefab in prefabs)
		{
			prefabsLookup[prefab.Id] = prefab;
		}
	}

	public static GameObject GetUnitPrefabDataById(string id)
	{
		return prefabsLookup[id].Prefab;
	}
}
