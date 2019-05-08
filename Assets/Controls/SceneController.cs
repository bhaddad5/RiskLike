using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	public Map Map;

	[SerializeField] private MapVisualizer mapVisPrefab;
	[SerializeField] private StoredMapData storedMap;

    // Start is called before the first frame update
    void Start()
    {
	    var vis = GameObject.Instantiate(mapVisPrefab);
	    Map = new Map(storedMap);
		vis.Setup(Map);
    }
}
