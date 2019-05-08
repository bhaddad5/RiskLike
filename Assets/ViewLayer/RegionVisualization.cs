using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegionVisualization : MonoBehaviour
{
	public UnitVisualizer UnitPrefab;

	private Region region;

	public void Setup(Region region)
	{
		this.region = region;
		foreach (Unit unit in region.Units)
		{
			var instantiatedUnit = GameObject.Instantiate(UnitPrefab);
			instantiatedUnit.Setup(unit);
		}

		var pos = region.RegionCenter * MapVisualizer.MapScaler;
		transform.localPosition = new Vector3(pos.x, 0, pos.y);

		region.Name.ChangeEvent += NameOnChangeEvent;
		NameOnChangeEvent(region.Name.Value, null);

		region.Faction.ChangeEvent += FactionOnChangeEvent;
		FactionOnChangeEvent(region.Faction.Value, null);
	}

	private void FactionOnChangeEvent(Faction arg1, Faction arg2)
	{
		GetComponentInChildren<TMP_Text>().outlineColor = arg1?.Color ?? new Color32(0, 0, 0, 1);
	}

	private void NameOnChangeEvent(string arg1, string arg2)
	{
		GetComponentInChildren<TMP_Text>().text = arg1;
	}

	void OnDestroy()
	{
		region.Name.ChangeEvent -= NameOnChangeEvent;
		region.Faction.ChangeEvent -= FactionOnChangeEvent;
	}
}
