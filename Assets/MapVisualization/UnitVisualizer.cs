using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisualizer : MonoBehaviour
{
	private UnitData unit;

	public void Setup(UnitData unit)
	{
		this.unit = unit;
		unit.CurrentOccupiedRegion.ChangeEvent += CurrentOccupiedRegionOnChangeEvent;
		CurrentOccupiedRegionOnChangeEvent(unit.CurrentOccupiedRegion.Value, null);
	}

	private void CurrentOccupiedRegionOnChangeEvent(RegionData arg1, RegionData arg2)
	{
		Debug.Log("Now in region: " + arg1.Name);
		var unitIndex = arg1.Units.IndexOf(unit);
		var pos = arg1.UnitPositions[unitIndex];

		transform.position = new Vector3(pos.x, 0, pos.y) * MapVisualizer.MapScaler;
	}

	void OnDestroy()
	{
		unit.CurrentOccupiedRegion.ChangeEvent -= CurrentOccupiedRegionOnChangeEvent;
	}
}
