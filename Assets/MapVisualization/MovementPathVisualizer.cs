using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPathVisualizer : MonoBehaviour
{
	public MovementArrowVisualizer ArrowPrefab;

    // Start is called before the first frame update
    void Start()
    {
	    var arrow1 = GameObject.Instantiate(ArrowPrefab);
		arrow1.Setup(new Vector3(0, 0, 0), new Vector3(3, 0, 5));

	    var arrow2 = GameObject.Instantiate(ArrowPrefab);
	    arrow2.Setup(new Vector3(3, 0, 5), new Vector3(7, 0, 2));
	}

	public void VisualizePath(List<Vector3> path)
	{

	}
}
