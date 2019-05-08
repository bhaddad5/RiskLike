using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	[SerializeField] private MapVisualizer mapVisPrefab;
	[SerializeField] private StoredMapData storedMap;

    // Start is called before the first frame update
    void Start()
    {
	    var vis = GameObject.Instantiate(mapVisPrefab);
		vis.Setup(new Map(storedMap));
    }
}
