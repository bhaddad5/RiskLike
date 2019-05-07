using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractor : MonoBehaviour
{
	public MovementPathVisualizer PathVis;

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 1000f, LayerMask.GetMask("Units")))
			{
				var unit = hit.transform.GetComponentInParent<UnitVisualizer>();
				Debug.Log(unit.gameObject.name);

				return;
			}

			if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 1000f, LayerMask.GetMask("Map")))
			{
				var point = hit.point / MapVisualizer.MapScaler;
				var region = hit.transform.GetComponentInParent<MapVisualizer>().GetRegionAtCoordinate(new Vector2(point.x, point.z));
				Debug.Log(region.Name);
				foreach (RegionData borderingRegion in region.BorderingRegions)
				{
					PathVis.VisualizePath(new List<Vector3>(){ RegionCenterToWorldPos(region.RegionCenter), RegionCenterToWorldPos(borderingRegion.RegionCenter)});

					//Debug.Log("Borders: " + borderingRegion.Name);
				}

				foreach (Vector2 unitPos in region.UnitPositions)
				{
					//Debug.Log("Unit Pos: " + unitPos);
				}

				return;
			}
		}
	}

	private Vector3 RegionCenterToWorldPos(Vector2 regionCenter)
	{
		return new Vector3(regionCenter.x * MapVisualizer.MapScaler, 0, regionCenter.y * MapVisualizer.MapScaler);
	}
}
