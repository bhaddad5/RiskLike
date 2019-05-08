using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementPathVisualizer : MonoBehaviour
{
	public MovementArrowVisualizer ArrowPrefab;
	
	private List<MovementArrowVisualizer> currentlyRenderedPath = new List<MovementArrowVisualizer>();

	public void VisualizePath(List<Vector3> path, bool attack)
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
			if (attack)
			{
				foreach (Image image in arrow.GetComponentsInChildren<Image>())
				{
					image.color = Color.red;
				}
			}
			currentlyRenderedPath.Add(arrow);
		}
	}
}
