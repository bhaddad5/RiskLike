using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoredFactionData
{
	public string FactionId;
	public string Name;
	public Color Color;

	public bool IsPlayer;

	public float Minerals;
	public float Alloys;
	public float Food;

	public List<string> Allies = new List<string>();
}
