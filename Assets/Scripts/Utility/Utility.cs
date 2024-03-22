using System;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
	public static float GetPercentage(float value, float max)
	{
		return value / max;
	}

	public static GameObject[] FindGameObjectsWithLayer(LayerMask searchLayer)
	{
		GameObject[] goArray = (GameObject[])MonoBehaviour.FindObjectsOfType(typeof(GameObject));
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

	public static void DestroyChildrens(Transform transform)
	{
		foreach (Transform tr in transform)
		{
			Destroy(tr.gameObject);
		}
	}

	public static T ExtractRandomElementFromList<T>(List<T> list) where T : ICloneable
	{
		int idx = UnityEngine.Random.Range(0, list.Count);
		T elem = (T)list[idx].Clone();
		list.RemoveAt(idx);

		return elem;
	}

	public static int ExtractRandomElementFromList(List<int> list)
	{
		int idx = UnityEngine.Random.Range(0, list.Count);
		int elem = list[idx];
		list.RemoveAt(idx);

		return elem;
	}

	public static T ExtractRandomElementFromList<T>(List<T> list, out int idx) where T : ICloneable
	{
		idx = UnityEngine.Random.Range(0, list.Count);
		T elem = (T)list[idx].Clone();
		list.RemoveAt(idx);

		return elem;
	}
}