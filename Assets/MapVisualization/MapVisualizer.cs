using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
	private MapData map;

	public void Setup(MapData m)
	{
		map = m;
		GetComponent<MeshRenderer>().material.mainTexture = map.MapRegions;

	}

	public RegionData GetRegionAtCoordinate(Vector2 uvCoord)
	{
		int x = (int)(uvCoord.x * map.MapRegions.width);
		int y = (int)(uvCoord.y * map.MapRegions.height);
		var color = map.MapRegions.GetPixel(x, y);
		foreach (RegionData region in map.Regions)
		{
			if (color.Equals(region.Color))
				return region;
		}
		throw new Exception("Failed to find region with color: " + color.ToString());
	}
}
