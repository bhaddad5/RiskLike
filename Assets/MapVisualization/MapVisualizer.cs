using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapVisualizer : MonoBehaviour
{
	[SerializeField] private RegionVisualization regionPrefab;

	public static float MapScaler = .05f;

	private MapData Map;

	public void Setup(MapData m)
	{
		Map = m;
		GetComponentInChildren<MeshRenderer>().material.mainTexture = Map.MapRegionsTexture;

		transform.localScale = new Vector3(Map.MapRegionsTexture.width, 0, Map.MapRegionsTexture.height) * MapScaler;
		transform.localPosition = new Vector3(transform.localScale.x/2f, 0, transform.localScale.z/2f);
		gameObject.GetComponent<BoxCollider>().size = transform.localScale;
		
		foreach (RegionData region in m.Regions)
		{
			var r = GameObject.Instantiate(regionPrefab);
			r.Setup(region);
		}

	}

	public RegionData GetRegionAtCoordinate(Vector2 pos)
	{
		var color = Map.MapRegionsTexture.GetPixel((int)pos.x, (int)pos.y);

		foreach (RegionData region in Map.Regions)
		{
			if (color.Equals(region.Color))
				return region;
		}
		throw new Exception("Failed to find region with color: " + color.ToString());
	}
}
