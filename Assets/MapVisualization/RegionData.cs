using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionData
{
	public string Name;
	public Color Color;
	public List<RegionData> BorderingRegions = new List<RegionData>();
	public List<Vector2> UnitPositions;

	public RegionData(StoredRegionData storedRegion, List<Vector2> unitPositions)
	{
		Name = storedRegion.Name;
		Color = storedRegion.Color;
		UnitPositions = unitPositions;
	}

	public void AddBorderRegion(RegionData borderingRegion)
	{
		BorderingRegions.Add(borderingRegion);
	}
}
