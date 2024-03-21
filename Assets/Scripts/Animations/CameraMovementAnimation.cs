using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraMovementAnimation : MonoBehaviour
{
	private Camera _playerCam;
	private Vector3 _initPlayerCamPosition;
	private Quaternion _initPlayerCamRotation;

	private Transform _pcTransform;
	private Transform _tilbiTransform;

	private Vector3 _targetPosition;
	private Quaternion _targetRotation;

	// When script is enabled, _isPC became true, when the camera moved from PC - false.
	private bool _isPC;

	[SerializeField] private float _PCTime;
	[SerializeField] private float _TilbiTime;
	private float _time;
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
		PlayerCollisionListener.PlayerCatched += OnPlayerCatched;

		_playerCam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();

		_currentTime = 0;
	}

	void OnDisable()
	{
		PCInteractionListener.PcInteracted -= OnPcInteracted;
		PlayerCollisionListener.PlayerCatched -= OnPlayerCatched;
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
		_isPC = true;
		_time = _PCTime;
	}

	private void OnPlayerCatched()
	{
		_initPlayerCamPosition = _playerCam.transform.position;
		_initPlayerCamRotation = _playerCam.transform.rotation;

		_tilbiTransform = GameObject.FindGameObjectWithTag("Tilbi").transform.Find("Baldi/Player Camera Position");
		_targetPosition = _tilbiTransform.position;
		_targetRotation = _tilbiTransform.rotation;

		_targetPosition.y = _initPlayerCamPosition.y;

		_time = _TilbiTime;
	}

	void Update()
	{
		if (_pcTransform == null && _tilbiTransform == null)
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

		OnCameraMovedTo();
	}

	private void OnCameraMovedTo()
	{
		if (_isPC)
		{
			var pCTestPassingLogic = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<PCTestPassingLogic>();
			pCTestPassingLogic.OnPcInteracted(_pcTransform.gameObject);
		}

		_isMovingTo = false;
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

		OnCameraMovedFrom();
	}

	private void OnCameraMovedFrom()
	{
		if (_isPC)
		{
			PlayerKeyboardInteractionController.EnableInventorySystem();
			PlayerKeyboardInteractionController.EnableItemInteractionLogic();
			PlayerKeyboardInteractionController.EnableMovement();
			PlayerKeyboardInteractionController.EnableMouseLook();

			StopGameLogic.ResumeGame();
			_isPC = false;
		}

		_isMovingTo = true;
		enabled = false;
	}
}