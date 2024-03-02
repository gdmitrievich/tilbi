using System.Collections.Generic;
using UnityEngine;

public class StopGameLogic : MonoBehaviour
{
	private static Dictionary<IMovable, float> _baseSpeed;
	private static IMovable[] _movableCharacters;

	static StopGameLogic()
	{
		_baseSpeed = new Dictionary<IMovable, float>();
		GameObject[] _characters = Utility.FindGameObjectsWithLayer(LayerMask.NameToLayer("CharacterLayer"));

		_movableCharacters = new IMovable[_characters.Length];
		for (int i = 0; i < _characters.Length; ++i)
		{
			IMovable movableCharacter = _characters[i].GetComponent<IMovable>();
			_movableCharacters[i] = movableCharacter;

			_baseSpeed.Add(_movableCharacters[i], _movableCharacters[i].Speed);
		}
	}

	public static void StopGame()
	{
		foreach (IMovable movableCharacter in _movableCharacters)
		{
			movableCharacter.Speed = 0;
		}
	}

	public static void ResumeGame()
	{
		foreach (IMovable movableCharacter in _movableCharacters)
		{
			movableCharacter.Speed = _baseSpeed[movableCharacter];
		}
	}
}