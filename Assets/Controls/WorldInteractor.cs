using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractor : MonoBehaviour
{
	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 1000f, LayerMask.GetMask("Map")))
			{
				var region = hit.transform.GetComponent<MapVisualizer>().GetRegionAtCoordinate(hit.textureCoord);
				Debug.Log(region.Name);
			}
		}
	}
}
