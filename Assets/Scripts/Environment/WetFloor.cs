using Unity.VisualScripting;
using UnityEngine;
using System;

public class WetFloor : MonoBehaviour
{
	public static event Action<GameObject> OnWetFloor;
	public static event Action<GameObject> OutOfWetFloor;

	private void OnTriggerEnter(Collider collider)
	{
		IMovable movable = collider.gameObject.GetComponent<IMovable>();
		if (movable != null)
		{
			movable.Speed /= 2;
			OnWetFloor?.Invoke(collider.gameObject);
		}

		var mainHeroReplicasManager = collider.GetComponentInChildren<MainHeroReplicasManager>();
		AudioSource mainHeroReplicasAudioSource = null;
		foreach (var audioSource in collider.GetComponentsInChildren<AudioSource>())
		{
			if (audioSource.name == "ReplicasAudioSource")
			{
				mainHeroReplicasAudioSource = audioSource;
			}
		}
		if (mainHeroReplicasManager != null && !mainHeroReplicasAudioSource.isPlaying)
		{
			mainHeroReplicasManager.PlayOnWetFloorReplica();
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		IMovable movable = collider.gameObject.GetComponent<IMovable>();
		if (movable != null)
		{
			movable.Speed *= 2;
			OutOfWetFloor?.Invoke(collider.gameObject);
		}
	}
}
