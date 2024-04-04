using System;
using TMPro;
using UnityEngine;

public class MainHeroEffectsController : MonoBehaviour {
	[SerializeField] private float _maxHeartBeatSpeed;
	private AudioSource _heartBeatAudioSource;
	private float _initialHeartBeatVolume;

	[SerializeField] private Color _initColor;
	[SerializeField] private Color _targetColor;

	[SerializeField] private float _initLightRange;
	[SerializeField] private float _targetLightRange;

	[SerializeField] private float _maxDistance;
	private float _distance;

	private Light _light;
	private Transform _tilbiTransform;

	void Awake () {
		_heartBeatAudioSource = GetComponentsInChildren<AudioSource>()[1];
		_heartBeatAudioSource.Play();
		_initialHeartBeatVolume = _heartBeatAudioSource.volume;

		_light = GetComponentInChildren<Light>();

		_tilbiTransform = GameObject.FindGameObjectWithTag("Tilbi").transform;
	}

	void Update() {
		_distance = Math.Clamp(Vector3.Distance(transform.position, _tilbiTransform.position), 0, _maxDistance);

		_heartBeatAudioSource.volume = (float) Math.Sqrt(1 - _distance / _maxDistance) * _initialHeartBeatVolume;
		_heartBeatAudioSource.pitch = _maxHeartBeatSpeed * (float) Math.Sqrt(1 - _distance / _maxDistance);

		_light.range = _initLightRange + (_targetLightRange - _initLightRange) * Utility.GetPercentage(_maxDistance - _distance, _maxDistance);
		_light.color = _initColor + (_targetColor - _initColor) * Utility.GetPercentage(_maxDistance - _distance, _maxDistance);
	}
}