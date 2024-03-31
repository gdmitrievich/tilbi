using System.Collections.Generic;
using UnityEngine;

public class StopGameLogic
{
	private static Dictionary<IMovable, float> _baseSpeed;
	private static IMovable[] _movableCharacters;

	static StopGameLogic()
	{
		_baseSpeed = new Dictionary<IMovable, float>();
	}

	public static void LoadObjects()
	{
		GameObject[] _characters = Utility.FindGameObjectsWithLayer(LayerMask.NameToLayer("CharacterLayer"));

		_movableCharacters = new IMovable[_characters.Length];
		for (int i = 0; i < _characters.Length; ++i)
		{
			IMovable movableCharacter = _characters[i].GetComponent<IMovable>();
			_movableCharacters[i] = movableCharacter;
		}
	}

	public static void StopGame()
	{
		if (_baseSpeed.Count != 0)
		{
			_baseSpeed.Clear();
		}

		foreach (IMovable movableCharacter in _movableCharacters)
		{
			_baseSpeed.Add(movableCharacter, movableCharacter.Speed);
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

	public static void ChangeSpeedValue(IMovable movableObj, float speedReductionValue)
	{
		Dictionary<IMovable, float> copyBaseSpeed = new Dictionary<IMovable, float>();

		foreach (var movable in _baseSpeed)
		{
			copyBaseSpeed.Add(movable.Key, movable.Value);
		}

		_baseSpeed.Clear();
		foreach (var movable in copyBaseSpeed)
		{
			if (movable.Key == movableObj)
			{
				_baseSpeed.Add(movable.Key, movable.Value + speedReductionValue);
			} else {
				_baseSpeed.Add(movable.Key, movable.Value);
			}
		}
	}
}