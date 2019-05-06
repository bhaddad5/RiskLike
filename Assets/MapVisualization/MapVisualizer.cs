using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
	private MapData Map;

	public void Setup(MapData m)
	{
		Map = m;
		GetComponent<MeshRenderer>().material.mainTexture = Map.MapRegionsTexture;

	}

	public RegionData GetRegionAtCoordinate(Vector2 uvCoord)
	{
		int x = (int)(uvCoord.x * Map.MapRegionsTexture.width);
		int y = (int)(uvCoord.y * Map.MapRegionsTexture.height);
		var color = Map.MapRegionsTexture.GetPixel(x, y);
		foreach (RegionData region in Map.Regions)
		{
			if (color.Equals(region.Color))
				return region;
		}
		throw new Exception("Failed to find region with color: " + color.ToString());
	}
}
