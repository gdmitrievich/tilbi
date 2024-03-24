using UnityEngine;

public class TilbiObservation : MonoBehaviour
{
	private GameObject _player;

	void Awake()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		var direction = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z) - transform.position;
		var rotation = Quaternion.LookRotation(direction);
		rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
		transform.rotation = rotation;
	}
}