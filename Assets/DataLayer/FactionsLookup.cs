using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionsLookup : MonoBehaviour
{
	public static Dictionary<string, Faction> factionLookup = new Dictionary<string, Faction>();
	[SerializeField] private List<StoredFactionData> factions = new List<StoredFactionData>();

	void Awake()
	{
		foreach (StoredFactionData faction in factions)
		{
			factionLookup[faction.FactionId] = new Faction(faction);
		}
	}

	public static Faction GetFactionDataById(string id)
	{
		if (!factionLookup.ContainsKey(id))
			return null;
		return factionLookup[id];
	}
}
