using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitVisualizer : MonoBehaviour
{
	public TMP_Text UnitName;
	public Image HealthBar;
	public Image HealthBarBacking;
	public Image UnitSelection;

	public Unit Unit;

	public void Setup(Unit unit)
	{
		Unit = unit;
		Unit.CurrentOccupiedRegion.ChangeEvent += CurrentOccupiedRegionOnChangeEvent;
		CurrentOccupiedRegionOnChangeEvent(unit.CurrentOccupiedRegion.Value, null);

		var instantiatedPrefab = GameObject.Instantiate(UnitPrefabsLookup.GetUnitPrefabDataById(unit.PrefabId), transform, false);

		float baseSize = 10f;
		HealthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(unit.MaxHP * baseSize, baseSize);
		HealthBarBacking.GetComponent<RectTransform>().sizeDelta = new Vector2(unit.MaxHP * baseSize + 2, baseSize + 2);

		Unit.Name.ChangeEvent += NameOnChangeEvent;
		NameOnChangeEvent(Unit.Name.Value, null);

		Unit.HP.ChangeEvent += HpOnChangeEvent;
		HpOnChangeEvent(Unit.HP.Value, 0);

		Unit.Selected.ChangeEvent += SelectedOnChangeEvent;
		SelectedOnChangeEvent(unit.Selected.Value, false);
	}

	private void SelectedOnChangeEvent(bool arg1, bool arg2)
	{
		UnitSelection.gameObject.SetActive(arg1);
	}

	private void HpOnChangeEvent(float arg1, float arg2)
	{
		HealthBar.fillAmount = arg1 / Unit.MaxHP;
	}

	private void NameOnChangeEvent(string arg1, string arg2)
	{
		UnitName.text = arg1;
	}

	private void CurrentOccupiedRegionOnChangeEvent(Region arg1, Region arg2)
	{
		var unitIndex = arg1.Units.IndexOf(Unit);
		var pos = arg1.UnitPositions[unitIndex];

		transform.position = new Vector3(pos.x, 0, pos.y) * MapVisualizer.MapScaler;
	}

	void OnDestroy()
	{
		Unit.CurrentOccupiedRegion.ChangeEvent -= CurrentOccupiedRegionOnChangeEvent;
		Unit.Name.ChangeEvent -= NameOnChangeEvent;
		Unit.HP.ChangeEvent -= HpOnChangeEvent;
		Unit.Selected.ChangeEvent -= SelectedOnChangeEvent;
	}
}
