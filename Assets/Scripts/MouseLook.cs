using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	[SerializeField] private Transform _playerBody;
	[SerializeField] private float _sensitivity = 650f;
	private Vector2 _turn;
	private float _xRotation;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		MouseLookingLogic();
		LookAroundLogic();
	}

	void MouseLookingLogic()
	{
		_turn.x = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
		_turn.y = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

		_xRotation -= _turn.y;
		_xRotation = Math.Clamp(_xRotation, -90, 90);

		transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
		_playerBody.Rotate(Vector3.up * _turn.x);
	}

	void LookAroundLogic()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			transform.localRotation = Quaternion.Euler(_xRotation, 180f, 0f);
		}
	}
}
