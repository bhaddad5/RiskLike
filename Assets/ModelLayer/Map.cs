﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map
{
	public string Name;
	public Texture2D MapRegionsTexture;
	public List<Region> Regions = new List<Region>();

	#region Proccess Map Data into usable form

	private Dictionary<Color, List<Color>> cachedBorders = new Dictionary<Color, List<Color>>();
	private Dictionary<Color, List<Vector2>> cachedUntiPositions = new Dictionary<Color, List<Vector2>>();
	private Dictionary<Color, List<Vector2>> cachedRegionPoints = new Dictionary<Color, List<Vector2>>();

	public Map(StoredMapData storedMap)
	{
		Name = storedMap.Name;
		MapRegionsTexture = new Texture2D(storedMap.MapRegions.width, storedMap.MapRegions.height, TextureFormat.RGB24, false);
		Graphics.CopyTexture(storedMap.MapRegions, MapRegionsTexture);

		foreach (StoredRegionData storedRegionData in storedMap.Regions)
		{
			cachedBorders[storedRegionData.Color] = new List<Color>();
			cachedUntiPositions[storedRegionData.Color] = new List<Vector2>();
			cachedRegionPoints[storedRegionData.Color] = new List<Vector2>();
		}

		for (int i = 0; i < MapRegionsTexture.width; i++)
			for (int j = 0; j < MapRegionsTexture.height; j++)
				StoreAndEraseExtraInfo(i, j);

		for (int i = 0; i < MapRegionsTexture.width; i++)
		for (int j = 0; j < MapRegionsTexture.height; j++)
			CheckBorders(i, j);

		foreach (StoredRegionData regionData in storedMap.Regions)
			Regions.Add(new Region(regionData, cachedUntiPositions[regionData.Color], new Vector2(cachedRegionPoints[regionData.Color].Average(v => v.x), cachedRegionPoints[regionData.Color].Average(v => v.y))));

		Dictionary<Color, Region> colorToRegion = new Dictionary<Color, Region>();
		foreach (Region region in Regions)
			colorToRegion[region.Color] = region;

		foreach (Region region in Regions)
		{
			foreach (Color borderColor in cachedBorders[region.Color])
			{
				if(!colorToRegion.ContainsKey(borderColor))
					Debug.LogError("Cannot find cached region with border color: " + borderColor);
				region.AddBorderRegion(colorToRegion[borderColor]);
			}
		}
	}

	private void StoreAndEraseExtraInfo(int i, int j)
	{
		if (MapRegionsTexture.GetPixel(i, j).Equals(Color.white))
		{
			Color c = Color.black;
			if (i - 1 > 0)
				c = MapRegionsTexture.GetPixel(i - 1, j);
			else if (j - 1 > 0)
				c = MapRegionsTexture.GetPixel(i, j - 1);
			else if (i + 1 < MapRegionsTexture.width)
				c = MapRegionsTexture.GetPixel(i + 1, j);
			else if (j + 1 < MapRegionsTexture.height)
				c = MapRegionsTexture.GetPixel(i, j + 1);

			cachedUntiPositions[c].Add(new Vector2(i, j));

			MapRegionsTexture.SetPixel(i, j, c);
		}
	}

	private void CheckBorders(int i, int j)
	{
		Color c = MapRegionsTexture.GetPixel(i, j);

		cachedRegionPoints[c].Add(new Vector2(i, j));

		if (i - 1 > 0)
			CheckPixelNeighborBorder(c, MapRegionsTexture.GetPixel(i - 1, j));
		if (j - 1 > 0)
			CheckPixelNeighborBorder(c, MapRegionsTexture.GetPixel(i, j - 1));
		if (i + 1 < MapRegionsTexture.width)
			CheckPixelNeighborBorder(c, MapRegionsTexture.GetPixel(i + 1, j));
		if (j + 1 < MapRegionsTexture.height)
			CheckPixelNeighborBorder(c, MapRegionsTexture.GetPixel(i, j + 1));
	}

	private void CheckPixelNeighborBorder(Color region, Color borderPx)
	{
		if (region.Equals(borderPx))
			return;

		if(!cachedBorders.ContainsKey(region))
			Debug.LogError("Cannot find region with color:" + region);

		if (!cachedBorders[region].Contains(borderPx))
			cachedBorders[region].Add(borderPx);
	}
	#endregion
}
