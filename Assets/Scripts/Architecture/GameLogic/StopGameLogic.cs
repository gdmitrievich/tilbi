using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StopGameLogic : MonoBehaviour
{
	private static Dictionary<IMovable, float> _baseSpeed;

	static StopGameLogic()
	{
		_baseSpeed = new Dictionary<IMovable, float>();
	}

	public static void StopGame()
	{
		foreach (GameObject character in FindGameObjectsWithLayer(LayerMask.NameToLayer("CharacterLayer")))
		{
			IMovable movableCharacter = character.GetComponent<IMovable>();
			_baseSpeed.Add(movableCharacter, movableCharacter.Speed);
			movableCharacter.Speed = 0;
		}
	}

	public static void ResumeGame()
	{
		foreach (GameObject character in FindGameObjectsWithLayer(LayerMask.NameToLayer("CharacterLayer")))
		{
			IMovable movableCharacter = character.GetComponent<IMovable>();
			movableCharacter.Speed = _baseSpeed[movableCharacter];
		}
	}

	private static GameObject[] FindGameObjectsWithLayer(LayerMask searchLayer) {
    	GameObject[] goArray = (GameObject[]) FindObjectsOfType(typeof(GameObject));
    	var goList = new List<GameObject>();

    	for (var i = 0; i < goArray.Length; i++) {
			if (goArray[i].layer == searchLayer) {
				goList.Add(goArray[i]);
			}
 		}

		if (goList.Count == 0) {
			return null;
		} else {
			foreach (var c in goList) {
				Debug.Log($"Character name: {c.name}");
			}
		}

		return goList.ToArray();
 	}
}