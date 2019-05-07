using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegionVisualization : MonoBehaviour
{
	private RegionData region;

	public void Setup(RegionData region)
	{
		this.region = region;
		foreach (UnitData unit in region.Units)
		{
			var instantiatedUnit = GameObject.Instantiate(UnitPrefabsLookup.GetUnitPrefabDataById(unit.PrefabId));
			instantiatedUnit.AddComponent<UnitVisualizer>().Setup(unit);
		}

		var pos = region.RegionCenter * MapVisualizer.MapScaler;
		transform.localPosition = new Vector3(pos.x, 0, pos.y);

		region.Name.ChangeEvent += NameOnChangeEvent;
		NameOnChangeEvent(region.Name.Value, null);
	}

	private void NameOnChangeEvent(string arg1, string arg2)
	{
		GetComponentInChildren<TMP_Text>().text = arg1;
	}

	void OnDestroy()
	{
		region.Name.ChangeEvent -= NameOnChangeEvent;
	}
}
