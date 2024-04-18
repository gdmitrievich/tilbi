using UnityEngine;

public class FallenClosetEventController : MonoBehaviour
{
	[SerializeField] private GameObject[] _objectsToBetTransformed;
	[SerializeField] private Transform[] _targetTransforms;
	private GameObject _uiStatisticObj;

	private Light _playerLight;
	private FlashingLightTriggerZone _flashingLightTriggerZone;
	private FlashingLight _flashingLight;

	private AudioSource _audioSource;

	private bool _isEntered;

	void Awake()
	{
		_uiStatisticObj = GameObject.Find("/UI/Statistic");

		_playerLight = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Light>();
		_flashingLightTriggerZone = transform.parent.GetComponentInChildren<FlashingLightTriggerZone>();
		_flashingLight = transform.parent.GetComponentInChildren<FlashingLight>();


		_audioSource = GetComponent<AudioSource>();

		_isEntered = false;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (!collider.CompareTag("Player") || !isActiveAndEnabled)
		{
			return;
		}
		_uiStatisticObj.SetActive(false);

		_flashingLightTriggerZone.enabled = false;
		_flashingLight.enabled = false;
		_playerLight.intensity = 0;

		_audioSource.Play();

		PlayerKeyboardInteractionController.DisableInventorySystem();
		PlayerKeyboardInteractionController.DisableItemInteractionLogic();
		PlayerKeyboardInteractionController.DisableMouseLook();
		PlayerKeyboardInteractionController.DisableMovement();

		StopGameLogic.StopGame();

		for (int i = 0; i < _objectsToBetTransformed.Length; ++i)
		{
			_objectsToBetTransformed[i].transform.localPosition = _targetTransforms[i].localPosition;
			_objectsToBetTransformed[i].transform.localRotation = _targetTransforms[i].localRotation;
			_objectsToBetTransformed[i].transform.localScale = _targetTransforms[i].localScale;
		}

		_isEntered = true;
	}

	void Update()
	{
		if (!_isEntered || _audioSource.isPlaying)
		{
			return;
		}
		_uiStatisticObj.SetActive(true);

		_playerLight.intensity = 1;

		PlayerKeyboardInteractionController.EnableInventorySystem();
		PlayerKeyboardInteractionController.EnableItemInteractionLogic();
		PlayerKeyboardInteractionController.EnableMouseLook();
		PlayerKeyboardInteractionController.EnableMovement();

		StopGameLogic.ResumeGame();

		_flashingLightTriggerZone.gameObject.SetActive(false);
		transform.gameObject.SetActive(false);
	}
}