using UnityEditor;
using UnityEngine;

public class CameraMovementAnimation : MonoBehaviour
{
	private Camera _playerCam;
	private Transform _initPlayerCamTransform;

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
		_initPlayerCamTransform = _playerCam.transform;

		_currentTime = 0;
	}

	void OnDisable()
	{
		PCInteractionListener.PcInteracted -= OnPcInteracted;
	}

	private void OnPcInteracted(GameObject obj)
	{
		_pcTransform = obj.transform;

		_targetPosition = _pcTransform.position + _positionOffset;
		// _targetRotation = Quaternion.Euler(_pcTransform.rotation.eulerAngles.x, _pcTransform.rotation.eulerAngles.y, _pcTransform.rotation.eulerAngles.z);
		_targetRotation = _pcTransform.rotation;
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
			_playerCam.transform.position = Vector3.Lerp(_initPlayerCamTransform.position, _targetPosition, _currentTime / _time);
			_playerCam.transform.rotation = Quaternion.Lerp(_initPlayerCamTransform.rotation, _targetRotation, _currentTime / _time);
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
			_playerCam.transform.position = Vector3.Lerp(_targetPosition, _initPlayerCamTransform.position, _currentTime / _time);
			_playerCam.transform.rotation = Quaternion.Lerp(_targetRotation, _initPlayerCamTransform.rotation, _currentTime / _time);
			return;
		}

		_isMovingTo = true;
		_currentTime = 0;
		enabled = false;
	}
}