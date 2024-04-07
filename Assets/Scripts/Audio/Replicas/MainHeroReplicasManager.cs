using UnityEngine;

public class MainHeroReplicasManager : MonoBehaviour
{
	[SerializeField] private AudioClip[] _onWetFloorReplicas;
	[SerializeField] private AudioClip[] _energyHasRunOutReplicas;
	private AudioSource _audioSource;

	void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayOnWetFloorReplica()
	{
		_audioSource.PlayOneShot(_onWetFloorReplicas[Random.Range(0, _onWetFloorReplicas.Length)]);
	}

	public void PlayEnergyHasRunOutReplicas()
	{
		_audioSource.PlayOneShot(_energyHasRunOutReplicas[Random.Range(0, _energyHasRunOutReplicas.Length)]);
	}
}