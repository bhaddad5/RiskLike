using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionData
{
	public Setting<string> Name = new Setting<string>();
	public NotifyList<UnitData> Units = new NotifyList<UnitData>();

	public Color Color;
	public List<RegionData> BorderingRegions = new List<RegionData>();
	public List<Vector2> UnitPositions;
	public Vector2 RegionCenter;

	public RegionData(StoredRegionData storedRegion, List<Vector2> unitPositions, Vector2 regionCenter)
	{
		Name.Value = storedRegion.Name;
		Color = storedRegion.Color;
		UnitPositions = unitPositions;
		RegionCenter = regionCenter;

		foreach (StoredIndividualUnitData storedUnit in storedRegion.ContainedUnits)
			Units.Add(new UnitData(storedUnit, this));
	}

	public void AddBorderRegion(RegionData borderingRegion)
	{
		BorderingRegions.Add(borderingRegion);
	}
}
