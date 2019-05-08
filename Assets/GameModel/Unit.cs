using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
	public Setting<string> Name = new Setting<string>();
	public Setting<float> HP = new Setting<float>();
	public Setting<float> Movement = new Setting<float>();

	public string TypeName;

	public float MaxHP;
	public float Attack;
	public float Defense;
	public float MaxMovement;

	public string PrefabId;

	public Setting<Region> CurrentOccupiedRegion = new Setting<Region>();

	public Unit(StoredIndividualUnitData unitData, Region region)
	{
		Name.Value = unitData.UnitName;
		var storedUnitType = UnitTypesLookup.GetUnitTypeDataById(unitData.TypeId);

		TypeName = storedUnitType.TypeName;
		MaxHP = storedUnitType.Health;
		Attack = storedUnitType.Attack;
		Defense = storedUnitType.Defense;
		Movement.Value = storedUnitType.Movement;
		HP.Value = unitData.HealthPercent * storedUnitType.Health;

		PrefabId = storedUnitType.PrefabId;

		CurrentOccupiedRegion.Value = region;
	}

	public bool CheckValidMovement(Region regionToMoveTo)
	{
		return GetAllPossibleMoves().ContainsKey(regionToMoveTo);
	}
	
	public Dictionary<Region, float> GetAllPossibleMoves()
	{
		Dictionary<Region, float> possibleMoves = new Dictionary<Region, float>();

		SortedDupList<Region> frontierTiles = new SortedDupList<Region>();
		frontierTiles.Insert(CurrentOccupiedRegion.Value, Movement.Value);
		while (frontierTiles.Count > 0)
		{
			possibleMoves[frontierTiles.TopValue()] = frontierTiles.TopKey();
			float currDifficulty = frontierTiles.TopKey();
			Region currRegion = frontierTiles.Pop();

			foreach (Region borderingRegion in currRegion.BorderingRegions)
			{
				float newMovePoints = currDifficulty - borderingRegion.MoveDifficulty;
				if (!borderingRegion.Passable || newMovePoints < 0)
					continue;

				if (possibleMoves.ContainsKey(borderingRegion) && possibleMoves[borderingRegion] > newMovePoints)
					continue;

				frontierTiles.Insert(borderingRegion, newMovePoints);
			}
		}
		return possibleMoves;
	}

	public void SetRegion(Region region)
	{
		Movement.Value = GetAllPossibleMoves()[region];

		CurrentOccupiedRegion.Value?.Units?.Remove(this);
		region.Units.Add(this);
		CurrentOccupiedRegion.Value = region;
	}
}
