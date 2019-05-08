using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractor : MonoBehaviour
{
	public SceneController SceneController;
	public MovementPathVisualizer PathVis;

	private Region currPointedRegion = null;
	private Setting<Unit> currSelectedUnit = new Setting<Unit>();

	void Start()
	{
		currSelectedUnit.ChangeEvent += CurrSelectedUnitOnChangeEvent;
		CurrSelectedUnitOnChangeEvent(currSelectedUnit.Value, null);
	}

	private void CurrSelectedUnitOnChangeEvent(Unit arg1, Unit arg2)
	{
		if(arg1 != null)
			arg1.Selected.Value = true;
		if(arg2 != null)
			arg2.Selected.Value = false;
	}

	void Update()
	{
		currPointedRegion = null;
		RaycastHit hit;
		if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 1000f, LayerMask.GetMask("Map")))
		{
			var point = hit.point / MapVisualizer.MapScaler;
			var region = hit.transform.GetComponentInParent<MapVisualizer>().GetRegionAtCoordinate(new Vector2(point.x, point.z));
			currPointedRegion = region;
		}

		if (Input.GetMouseButtonUp(0))
		{
			currSelectedUnit.Value = null;
			
			if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 1000f, LayerMask.GetMask("Units")))
			{
				var unit = hit.transform.GetComponentInParent<UnitVisualizer>();
				currSelectedUnit.Value = unit.Unit;
			}
		}

		if (currSelectedUnit.Value != null && currPointedRegion != null &&
		    currSelectedUnit.Value.CurrentOccupiedRegion.Value != currPointedRegion &&
		    currSelectedUnit.Value.CheckValidMovement(currPointedRegion))
		{
			PathVis.VisualizePath(new List<Vector3>()
			{
				RegionCenterToWorldPos(currSelectedUnit.Value.CurrentOccupiedRegion.Value.RegionCenter),
				RegionCenterToWorldPos(currPointedRegion.RegionCenter)
			},  currSelectedUnit.Value.Faction.Value != currPointedRegion.Faction.Value);
		}
		else
		{
			PathVis.VisualizePath(new List<Vector3>(), false);
		}

		if (Input.GetMouseButtonUp(1))
		{
			if (currSelectedUnit.Value != null && currPointedRegion != null &&
			    currSelectedUnit.Value.CurrentOccupiedRegion.Value != currPointedRegion &&
			    currSelectedUnit.Value.CheckValidMovement(currPointedRegion))
			{
				currSelectedUnit.Value.SetRegion(currPointedRegion);
			}
		}
	}

	private Vector3 RegionCenterToWorldPos(Vector2 regionCenter)
	{
		return new Vector3(regionCenter.x * MapVisualizer.MapScaler, 0, regionCenter.y * MapVisualizer.MapScaler);
	}

	public void HandleEndTurn()
	{
		foreach (var faction in FactionsLookup.factionLookup.Values)
		{
			foreach (Unit unit in faction.Units)
			{
				unit.Refresh();
			}
		}
	}

	void OnDestroy()
	{
		currSelectedUnit.ChangeEvent -= CurrSelectedUnitOnChangeEvent;

	}
}
