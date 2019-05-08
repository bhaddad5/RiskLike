using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractor : MonoBehaviour
{
	public SceneController SceneController;
	public MovementPathVisualizer PathVis;

	private Region currPointedRegion = null;
	private Unit currSelectedUnit = null;

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
			currSelectedUnit = null;
			
			if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 1000f, LayerMask.GetMask("Units")))
			{
				var unit = hit.transform.GetComponentInParent<UnitVisualizer>();
				currSelectedUnit = unit.Unit;
			}
		}

		if (currSelectedUnit != null && currPointedRegion != null &&
		    currSelectedUnit.CurrentOccupiedRegion.Value != currPointedRegion &&
		    currSelectedUnit.CheckValidMovement(currPointedRegion))
		{
			PathVis.VisualizePath(new List<Vector3>()
			{
				RegionCenterToWorldPos(currSelectedUnit.CurrentOccupiedRegion.Value.RegionCenter),
				RegionCenterToWorldPos(currPointedRegion.RegionCenter)
			});
		}
		else
		{
			PathVis.VisualizePath(new List<Vector3>());
		}

		if (Input.GetMouseButtonUp(1))
		{
			if (currSelectedUnit != null && currPointedRegion != null &&
			    currSelectedUnit.CurrentOccupiedRegion.Value != currPointedRegion &&
			    currSelectedUnit.CheckValidMovement(currPointedRegion))
			{
				currSelectedUnit.SetRegion(currPointedRegion);
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
}
