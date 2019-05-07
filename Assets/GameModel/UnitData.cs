using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData
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

	public Setting<RegionData> CurrentOccupiedRegion = new Setting<RegionData>();

	public UnitData(StoredIndividualUnitData unitData, RegionData region)
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
}
