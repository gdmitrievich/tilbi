using UnityEditor;
using UnityEngine;

public class CameraMovementAnimation : MonoBehaviour
{
	private Camera _playerCam;
	private Vector3 _initPlayerCamPosition;
	private Quaternion _initPlayerCamRotation;

	private Transform _pcTransform;
	[SerializeField] private Vector3 _positionOffset;

	private Vector3 _targetPosition;
	private Quaternion _targetRotation;

	[SerializeField] private float _time;
	private float _currentTime;
	private bool _isMovingTo;
	public bool IsMovingTo
	{
		get => _isMovingTo;
		set => _isMovingTo = value;
	}

	void OnEnable()
	{
		PCInteractionListener.PcInteracted += OnPcInteracted;

		_playerCam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();

		_currentTime = 0;
	}

	void OnDisable()
	{
		PCInteractionListener.PcInteracted -= OnPcInteracted;
	}

	private void OnPcInteracted(GameObject obj)
	{
		_initPlayerCamPosition = _playerCam.transform.position;
		_initPlayerCamRotation = _playerCam.transform.rotation;
		// _playerCam.transform.parent = null;

		_pcTransform = obj.transform;
		Transform destination = _pcTransform.Find("Camera Position");

		_targetPosition = destination.transform.position;
		_targetRotation = destination.transform.rotation;
		// _targetPosition = _pcTransform.position + _positionOffset;
		// _targetRotation = Quaternion.Euler(_pcTransform.rotation.eulerAngles.x, _pcTransform.rotation.eulerAngles.y, _pcTransform.rotation.eulerAngles.z);
		// _targetRotation = _pcTransform.rotation;
	}

	void Update()
	{
		if (_pcTransform == null)
		{
			return;
		}
		if (_isMovingTo)
		{
			MovingTo();
		}
		else
		{
			MovingFrom();
		}
	}

	private void MovingTo()
	{
		if (_currentTime < _time)
		{
			Debug.Log(_currentTime);
			_currentTime += Time.deltaTime;
			_playerCam.transform.position = Vector3.Lerp(_initPlayerCamPosition, _targetPosition, _currentTime / _time);
			_playerCam.transform.rotation = Quaternion.Lerp(_initPlayerCamRotation, _targetRotation, _currentTime / _time);
			return;
		}

		var pCTestPassingLogic = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<PCTestPassingLogic>();
		pCTestPassingLogic.OnPcInteracted(_pcTransform.gameObject);

		_isMovingTo = false;
		_currentTime = 0;
		enabled = false;
	}

	private void MovingFrom()
	{
		if (_currentTime < _time)
		{
			_currentTime += Time.deltaTime;
			_playerCam.transform.position = Vector3.Lerp(_targetPosition, _initPlayerCamPosition, _currentTime / _time);
			_playerCam.transform.rotation = Quaternion.Lerp(_targetRotation, _initPlayerCamRotation, _currentTime / _time);
			return;
		}

		PlayerKeyboardInteractionController.EnableInventorySystem();
		PlayerKeyboardInteractionController.EnableItemInteractionLogic();
		PlayerKeyboardInteractionController.EnableMovement();
		PlayerKeyboardInteractionController.EnableMouseLook();

		StopGameLogic.ResumeGame();

		_isMovingTo = true;
		_currentTime = 0;
		enabled = false;
	}
}