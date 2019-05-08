using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction
{
	public Setting<string> Name = new Setting<string>();
	public Setting<float> Minerals = new Setting<float>();
	public Setting<float> Alloys = new Setting<float>();
	public Setting<float> Food = new Setting<float>();
	public NotifyList<Unit> Units = new NotifyList<Unit>();

	public string Id;
	public Color Color;

	public Faction(StoredFactionData factionData)
	{
		Name.Value = factionData.Name;
		Minerals.Value = factionData.Minerals;
		Alloys.Value = factionData.Alloys;
		Food.Value = factionData.Food;

		Id = factionData.FactionId;
		Color = factionData.Color;
	}
}
