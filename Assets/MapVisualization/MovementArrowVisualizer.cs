using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementArrowVisualizer : MonoBehaviour
{
	public GameObject Arrow;
	public GameObject Line;

	public void Setup(Vector3 from, Vector3 to)
	{
		Line.transform.position = from + new Vector3(0, .05f, 0);
		Line.transform.up = (to - from);
		Line.transform.localScale = new Vector3(1, (to - from).magnitude, 1);
		Line.transform.eulerAngles = new Vector3(90f, Line.transform.eulerAngles.y, Line.transform.eulerAngles.z);
		Arrow.transform.position = to + new Vector3(0, .05f, 0);
		Arrow.transform.up = (to - from);
		Arrow.transform.eulerAngles = new Vector3(90f, Arrow.transform.eulerAngles.y, Arrow.transform.eulerAngles.z);

	}
}
