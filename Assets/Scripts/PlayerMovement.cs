using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private CharacterController _controller;
	[SerializeField] private float _speed = 50f;
	private Vector2 _turn;
	private Vector3 _target;

	[SerializeField] private LayerMask _groundLayerMask;
	[SerializeField] private Transform _groundCheck;
	private float _GRAVITY = -9.81f;
	private float _FORSE_TO_MOVE_DOWN = -25f;
	private Vector3 _velocity;
	private bool _isGrounded;
	private float _groundRadius = 0.5f;

	void Update()
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
}
