using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region
{
	public Setting<string> Name = new Setting<string>();
	public NotifyList<Unit> Units = new NotifyList<Unit>();
	public Setting<Faction> Faction = new Setting<Faction>();

	public Color Color;
	public List<Region> BorderingRegions = new List<Region>();
	public List<Vector2> UnitPositions;
	public Vector2 RegionCenter;

	public bool Passable = true;
	public float MoveDifficulty;

	public Region(StoredRegionData storedRegion, List<Vector2> unitPositions, Vector2 regionCenter)
	{
		Name.Value = storedRegion.Name;
		Color = storedRegion.Color;
		UnitPositions = unitPositions;
		RegionCenter = regionCenter;
		Passable = storedRegion.Passable;
		MoveDifficulty = storedRegion.MoveDifficulty;
		Faction.Value = FactionsLookup.GetFactionDataById(storedRegion.FactionId);

		foreach (StoredIndividualUnitData storedUnit in storedRegion.ContainedUnits)
			Units.Add(new Unit(storedUnit, this));
	}

	public void AddBorderRegion(Region borderingRegion)
	{
		BorderingRegions.Add(borderingRegion);
	}
}
