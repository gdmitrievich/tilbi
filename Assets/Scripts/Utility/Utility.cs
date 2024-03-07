using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
	public static float GetPercentage(float value, float max)
	{
		return value / max;
	}

	public static GameObject[] FindGameObjectsWithLayer(LayerMask searchLayer)
	{
		GameObject[] goArray = (GameObject[]) MonoBehaviour.FindObjectsOfType(typeof(GameObject));
		var goList = new List<GameObject>();

		for (var i = 0; i < goArray.Length; i++)
		{
			if (goArray[i].layer == searchLayer)
			{
				goList.Add(goArray[i]);
			}
		}

		if (goList.Count == 0)
		{
			return null;
		}

		return goList.ToArray();
	}
}