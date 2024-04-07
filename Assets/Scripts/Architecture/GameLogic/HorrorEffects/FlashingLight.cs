using UnityEngine;

public class FlashingLight : MonoBehaviour
{
	[SerializeField] private Transform _target;
	[SerializeField] private float _pingPongLightSpeed;
	[SerializeField] private float _blinkingLightIntensity;
	[SerializeField] private float _lightIntensity;
	private float _initialLightIntensity;
	private Light _lightComponent;
	private MainHeroEffectsController _mainHeroEffectsController;
	private Transform _playerTransform;

	private float _distanceToTarget;
	private float _maxDistance;

	void Awake()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		_lightComponent = player.GetComponentInChildren<Light>();
		_mainHeroEffectsController = player.GetComponent<MainHeroEffectsController>();
		_playerTransform = player.transform;
	}

	void OnEnable()
	{
		_maxDistance = Vector3.Distance(_playerTransform.position, _target.position);
		_mainHeroEffectsController.enabled = false;
		_initialLightIntensity = _lightComponent.intensity;
	}

	void OnDisable()
	{
		_mainHeroEffectsController.enabled = true;
		_lightComponent.intensity = _initialLightIntensity;
	}

	void Update()
	{
		_lightComponent.intensity = Mathf.PingPong(Time.time * _pingPongLightSpeed + Time.deltaTime * _blinkingLightIntensity, _lightIntensity);
	}
}
