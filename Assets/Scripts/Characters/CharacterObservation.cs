using UnityEngine;

// Rotate an object towards the target.transform place.
public class CharacterObservation : MonoBehaviour
{
	[SerializeField] private GameObject _objToRotate;
	private GameObject _player;

	void Awake () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		var direction = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z) - _objToRotate.transform.position;
		var rotation = Quaternion.LookRotation(direction);
		rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
		_objToRotate.transform.rotation = rotation;
	}
}