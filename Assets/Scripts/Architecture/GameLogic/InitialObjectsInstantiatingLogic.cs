using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InitialObjectsInstantiatingLogic : MonoBehaviour
{
	private static List<GameObject> _itemPlaces;
	private static List<GameObject> _NPCPlaces;

	private static int _countOfItems;
	private static int _countOfNPCs;
	private static int _countOfCheetSheets;

	private const string _BANANA_PATH = "Items/Prefabs/Banana";
	private const string _PROTEIN_PATH = "Items/Prefabs/Protein";
	private const string _CHEET_SHEET_PATH = "Items/Prefabs/CheetSheet";
	private const string _BOTANIST_PATH = "Characters/Prefabs/NPCs/Botanist";
	private const string _BULLY_PATH = "Characters/Prefabs/NPCs/Bully";

	void Awake()
	{
		// _countOfCheetSheets = / get count from other file/
		_countOfCheetSheets = 3;
	}

	void Start()
	{
		Generate();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.T))
		{
			_countOfCheetSheets = 3;
			DestroyObjects();
			Generate();
		}
	}

	public static void Generate()
	{
		GetFreePlaces();

		GenerateItems();
		GenerateNPCs();
	}

	private static void GetFreePlaces()
	{
		_itemPlaces = GameObject.FindGameObjectsWithTag("ItemPlace").ToList();
		_countOfItems = _itemPlaces.Count / 2;
		_NPCPlaces = GameObject.FindGameObjectsWithTag("NPCPlace").ToList();
		_countOfNPCs = _NPCPlaces.Count / 2;
		Debug.Log($"NPC count: {_NPCPlaces.Count}");
	}

	private static void GenerateItems()
	{
		_countOfItems -= _countOfCheetSheets;

		int countOfBananas = Random.Range(0, _countOfItems + 1);
		_countOfItems -= countOfBananas;
		int countOfProteins = Random.Range(0, _countOfItems + 1);
		_countOfItems -= countOfProteins;

		// Debug.Log($"Count of free places {_itemPlaces.Count}");
		// Debug.Log($"Count of Items left {_countOfItems}");
		// Debug.Log($"Count of CheetSheets {_countOfCheetSheets}");
		// Debug.Log($"Count of Bananas {countOfBananas}");
		// Debug.Log($"Count of Proteins {countOfProteins}");

		GenerateCheetSheets(_countOfCheetSheets);
		GenerateObjects(countOfBananas, _itemPlaces, _BANANA_PATH);
		GenerateObjects(countOfProteins, _itemPlaces, _PROTEIN_PATH);
	}

	private static void GenerateCheetSheets(int count)
	{
		GameObject itemPlace, item;
		int itemIdx;
		while (count > 0 && _itemPlaces.Count > 0)
		{
			itemIdx = Random.Range(0, _itemPlaces.Count);
			itemPlace = _itemPlaces[itemIdx];
			_itemPlaces.RemoveAt(itemIdx);

			item = LoadObject(_CHEET_SHEET_PATH, itemPlace.transform);
			// item.GetComponent<CheetSheet>().hints = /Some load data here/
			--count;
		}
	}

	private static void GenerateObjects(int count, List<GameObject> places, string path)
	{
		GameObject objPlace;
		int objIdx;
		while (count > 0 && places.Count > 0)
		{
			objIdx = Random.Range(0, places.Count);
			objPlace = places[objIdx];
			places.RemoveAt(objIdx);

			LoadObject(path, objPlace.transform);
			--count;
		}
	}

	private static GameObject LoadObject(string path, Transform transform)
	{
		return Instantiate(Resources.Load(path) as GameObject, transform, false);
	}

	private static void GenerateNPCs()
	{
		int countOfBulls, countOfBotanists;
		int minBulls = 3, minBotanists = 2;

		do
		{
			countOfBulls = Random.Range(minBulls, _countOfNPCs + 1 - minBotanists);
			_countOfNPCs -= countOfBulls;
			countOfBotanists = Random.Range(minBotanists, _countOfNPCs + 1);
			_countOfNPCs = _NPCPlaces.Count / 2;
		} while (countOfBulls < countOfBotanists);

		Debug.Log($"Count of free places {_NPCPlaces.Count}");
		Debug.Log($"Count of Bulls {countOfBulls}");
		Debug.Log($"Count of Botanists {countOfBotanists}");

		GenerateObjects(countOfBulls, _NPCPlaces, _BULLY_PATH);
		GenerateObjects(countOfBotanists, _NPCPlaces, _BOTANIST_PATH);
	}


	private static void Remove(GameObject[] list)
	{
		if (list == null)
		{
			return;
		}

		for (int i = 0; i < list.Length; ++i)
		{
			Destroy(list[i].gameObject);
		}
	}

	private static void DestroyObjects()
	{
		Remove(GameObject.FindGameObjectsWithTag("Banana"));
		Remove(GameObject.FindGameObjectsWithTag("Protein"));
		Remove(GameObject.FindGameObjectsWithTag("CheetSheet"));
		Remove(GameObject.FindGameObjectsWithTag("Bully"));
		Remove(GameObject.FindGameObjectsWithTag("Botanist"));
	}
}