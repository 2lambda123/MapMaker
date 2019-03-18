using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureLibrary : MonoBehaviour
{
	[Header("Miniature Prefabs")]
	[SerializeField]
	private List<GameObject> miniaturePrefabs = default;

	// Retrieve a miniature based off its id value
	public GameObject GetMiniature(int id)
	{
		for (int i = 0; i < miniaturePrefabs.Count; i++)
		{
			if (miniaturePrefabs[i].GetComponent<Miniature>().GetMiniatureId() == id)
			{
				return miniaturePrefabs[i];
			}
		}
		// Return NULL if no miniature was found
		return null;
	}
}
