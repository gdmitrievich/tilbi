using System;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
	[SerializeField] private CharacterController _controller;

	[SerializeField] private float _speed = 10f;
	[SerializeField] private float _speedBoost = 5f;
	[SerializeField] private float _baseSpeed = 10f;
	[SerializeField] private float _energy = 6f;
	[SerializeField] private float _ENERGY_LIMIT = 6f;
	[SerializeField] private bool _isTired = false;
	private bool _onWetFloor { get; set; }

	public float Energy
	{
		get => _energy;
	}

	public float EnergyLimit
	{
		get => _ENERGY_LIMIT;
	}

	public float Speed
	{
		get => _speed;
		set
		{
			if (value >= 0)
			{
				_speed = value;
			}
		}
	}

	public float BaseSpeed {
		get => _speed;
	}
	// public float BaseSpeed {
	// 	get => _baseSpeed;
	// 	set {
	// 		if (value > 0) {
	// 			_baseSpeed = value;
	// 		}
	// 	}
	// }

	private Vector3 _previousFramePosition;

	private Vector2 _turn;
	private Vector3 _target;

	[SerializeField] private LayerMask _groundLayerMask;
	[SerializeField] private Transform _groundCheck;
	private float _GRAVITY = -9.81f;
	private float _FORSE_TO_MOVE_DOWN = 25f;
	private Vector3 _velocity;
	private bool _isGrounded;
	private float _groundRadius = 0.5f;

	void Awake()
	{
		_previousFramePosition = transform.position;
	}

	void OnEnable()
	{
		InventorySystem.ItemUsed += OnItemUsed;
		WetFloor.OnWetFloor += OnPlayerOnWetFloor;
		WetFloor.OutOfWetFloor += OnPlayerOutOfWetFloor;
	}
	void OnDisable()
	{
		InventorySystem.ItemUsed -= OnItemUsed;
		WetFloor.OnWetFloor -= OnPlayerOnWetFloor;
		WetFloor.OutOfWetFloor -= OnPlayerOutOfWetFloor;
	}

	private void OnItemUsed(GameObject item)
	{
		if (item.GetComponent<ItemData>() is Boost boost)
		{
			_energy = Math.Clamp(_energy + boost.energy, 0, _ENERGY_LIMIT);
		}
	}

	private void OnPlayerOnWetFloor(GameObject obj)
	{
		if (obj.CompareTag("Player"))
		{
			_baseSpeed /= 2;
			_onWetFloor = true;
		}
	}

	private void OnPlayerOutOfWetFloor(GameObject obj)
	{
		if (obj.CompareTag("Player"))
		{
			_baseSpeed *= 2;
			_onWetFloor = false;
		}
	}

	void Update()
	{
		BoostLogic();
		MovementLogic();
	}

	void MovementLogic()
	{
		_isGrounded = Physics.CheckSphere(_groundCheck.position, _groundRadius, _groundLayerMask);

		if (_isGrounded && _velocity.y < 0)
		{
			_velocity.y = -2;
		}

		_turn.x = Input.GetAxis("Horizontal");
		_turn.y = Input.GetAxis("Vertical");

		_target = transform.right * _turn.x + transform.forward * _turn.y;
		// transform.position = Vector3.Lerp(transform.position, transform.position + _target, Time.deltaTime);

		_velocity.y += _GRAVITY * _FORSE_TO_MOVE_DOWN * Time.deltaTime;

		_controller.Move(_target * _speed * Time.deltaTime);
		_controller.Move(_velocity * Time.deltaTime);

		if (_turn.x == 0 && _turn.y == 0) {
			_speed = 0;
		}
	}

	void BoostLogic()
	{
		if (transform.position == _previousFramePosition && _energy < _ENERGY_LIMIT)
		{
			_energy += Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.LeftShift) && !_isTired && !_onWetFloor)
		{
			_speed = Math.Clamp(_speed + _speedBoost * Time.deltaTime, _baseSpeed, _baseSpeed + _speedBoost);
			_energy -= Time.deltaTime;

			if (_energy <= 0)
			{
				_isTired = true;
				_energy = 0;
			}
		}
		else
		{
			if (_energy > 0)
			{
				_isTired = false;
			}

			_speed = _baseSpeed;
		}

		_previousFramePosition = transform.position;
	}
}
