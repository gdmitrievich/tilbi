using UnityEngine;

public class FlashingLightTriggerZone : MonoBehaviour
{
	private int _countOfEntries;
	private int _countOfEntriesToInvokeEvent;
	private FlashingLight _flashingLight;
	private FallenClosetEventController _fallenClosetEventController;

	void Start()
	{
		_flashingLight = GetComponent<FlashingLight>();
		_fallenClosetEventController = transform.parent.GetComponentInChildren<FallenClosetEventController>();
		_countOfEntries = 0;
		_countOfEntriesToInvokeEvent = Random.Range(0, 5);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (!collider.CompareTag("Player"))
		{
			return;
		}
		if (_countOfEntries == _countOfEntriesToInvokeEvent)
		{
			_fallenClosetEventController.enabled = true;
		}

		_flashingLight.enabled = true;

		_countOfEntries++;
	}

	void OnTriggerExit(Collider collider)
	{
		if (!collider.CompareTag("Player"))
		{
			return;
		}
		_flashingLight.enabled = false;
	}
}