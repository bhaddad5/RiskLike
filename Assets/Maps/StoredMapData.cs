using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoredMapData
{
	public string Name;
	public Texture2D MapRegions;
	public List<StoredRegionData> Regions;
}
