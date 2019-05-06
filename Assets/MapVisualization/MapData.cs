using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
	public string Name;
	public Texture2D MapRegions;
	public List<RegionData> Regions;
}
