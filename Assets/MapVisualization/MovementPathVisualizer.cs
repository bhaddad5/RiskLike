using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPathVisualizer : MonoBehaviour
{
	public MovementArrowVisualizer ArrowPrefab;
	
	private List<MovementArrowVisualizer> currentlyRenderedPath = new List<MovementArrowVisualizer>();

	public void VisualizePath(List<Vector3> path)
	{
		foreach (MovementArrowVisualizer movementArrowVisualizer in currentlyRenderedPath)
			GameObject.Destroy(movementArrowVisualizer.gameObject);
		currentlyRenderedPath.Clear();

		for (int i = 0; i < path.Count; i++)
		{
			if (i == 0)
				continue;
			var arrow = GameObject.Instantiate(ArrowPrefab);
			arrow.Setup(path[i-1], path[i]);
			currentlyRenderedPath.Add(arrow);
		}
	}
}
