using System;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private CharacterController _controller;

	[SerializeField] private float _speed = 6f;
	[SerializeField] private float _speedBoost = 2.5f;
	[SerializeField] private float _BASE_SPEED = 6f;
	[SerializeField] private float _boostTime;
	[SerializeField] private float _BOOST_TIME_LIMIT = 6f;
	[SerializeField] private bool _isTired = false;

	private Vector2 _turn;
	private Vector3 _target;

	[SerializeField] private LayerMask _groundLayerMask;
	[SerializeField] private Transform _groundCheck;
	private float _GRAVITY = -9.81f;
	private float _FORSE_TO_MOVE_DOWN = 25f;
	private Vector3 _velocity;
	private bool _isGrounded;
	private float _groundRadius = 0.5f;

	void Update()
	{
		MovementLogic();
		BoostLogic();
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
	}

	void BoostLogic()
	{
		if (Input.GetKey(KeyCode.LeftShift) && !_isTired)
		{
			_speed = Math.Clamp(_speed + _speedBoost * Time.deltaTime, _BASE_SPEED, _BASE_SPEED + _speedBoost);
			_boostTime += Time.deltaTime;

			if (_boostTime > _BOOST_TIME_LIMIT)
			{
				_isTired = true;
			}
		}
		else
		{
			if (_boostTime <= 0)
			{
				_isTired = false;
			}

			_speed = _BASE_SPEED;
			_boostTime -= _boostTime > 0 ? Time.deltaTime * 1.5f : _boostTime;
		}
	}
}
