using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialObjectsInstantiatingLogic : MonoBehaviour
{
	private static List<GameObject> _cheetSheetPlaces;
	private static List<GameObject> _eatPlaces;
	private static List<GameObject> _NPCPlaces;

	private static int _countOfNPCs;
	private static int _countOfEatItems;
	private static int _countOfCheetSheets;

	private const string _BANANA_PATH = "Items/Prefabs/Banana";
	private const string _PROTEIN_PATH = "Items/Prefabs/Protein";
	private const string _CHEET_SHEET_PATH = "Items/Prefabs/CheetSheet";
	private const string _BOTANIST_PATH = "Characters/Prefabs/NPCs/Botanists/Botanist";
	private const string _BULLY_PATH = "Characters/Prefabs/NPCs/Bulls/Bully";

	void Awake()
	{
		_countOfCheetSheets = CheetSheetsLoader.count;
	}

	void Start()
	{
		Generate();
	}

	public static void Generate()
	{
		TestsLoader.Load((SceneManagerLogic.Scene)SceneManager.GetActiveScene().buildIndex);

		if (SceneManager.GetActiveScene().buildIndex == (int)SceneManagerLogic.Scene.Horror)
		{
			GetFreePlaces();

			GenerateItems();
			GenerateNPCs();

			CheetSheetsLoader.Load();
		}
	}

	private static void GetFreePlaces()
	{
		_eatPlaces = GameObject.FindGameObjectsWithTag("EatPlace").ToList();
		_countOfEatItems = _eatPlaces.Count / 2;
		_cheetSheetPlaces = GameObject.FindGameObjectsWithTag("CheetSheetPlace").ToList();
		_countOfCheetSheets = _cheetSheetPlaces.Count / 2;
		_NPCPlaces = GameObject.FindGameObjectsWithTag("NPCPlace").ToList();
		_countOfNPCs = _NPCPlaces.Count / 2;
		// Debug.Log($"Eat count: {_eatPlaces.Count}");
		// Debug.Log($"CheetSheet count: {_cheetSheetPlaces.Count}");
		// Debug.Log($"NPC count: {_NPCPlaces.Count}");
	}

	private static void GenerateItems()
	{
		int countOfBananas = Random.Range(0, _countOfEatItems + 1);
		_countOfEatItems -= countOfBananas;
		int countOfProteins = _countOfEatItems;

		// Debug.Log($"Count of CheetSheets {_countOfCheetSheets}");
		// Debug.Log($"Count of Bananas {countOfBananas}");
		// Debug.Log($"Count of Proteins {countOfProteins}");

		GenerateObjects(_countOfCheetSheets, _cheetSheetPlaces, _CHEET_SHEET_PATH);
		GenerateObjects(countOfBananas, _eatPlaces, _BANANA_PATH);
		GenerateObjects(countOfProteins, _eatPlaces, _PROTEIN_PATH);
	}

	private static void GenerateObjects(int count, List<GameObject> places, string path)
	{
		GameObject objPlace;
		while (count > 0 && places.Count > 0)
		{
			objPlace = Utility.ExtractRandomElementFromList(places);

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

		// Debug.Log($"Count of Bulls {countOfBulls}");
		// Debug.Log($"Count of Botanists {countOfBotanists}");

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
}