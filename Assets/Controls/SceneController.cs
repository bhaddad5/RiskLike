using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	[SerializeField] private MapVisualizer mapVisPrefab;
	[SerializeField] private MapData map;

    // Start is called before the first frame update
    void Start()
    {
	    var vis = GameObject.Instantiate(mapVisPrefab);
		vis.Setup(map);
    }
}
