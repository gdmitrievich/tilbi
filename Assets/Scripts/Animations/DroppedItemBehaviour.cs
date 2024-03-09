using Unity.VisualScripting;
using UnityEngine;

public class DroppedItemBehaviour : MonoBehaviour
{
	[SerializeField] private BoxCollider _liftableCollider;
	[SerializeField] private BoxCollider _disposableCollider;

	private Rigidbody _itemRb;
	private Transform _itemTransform;
	private Vector3 _initialVelocity;

	public float reduceTime, elapsedTime;

	public float delayTime, _time;
	private bool _delayTimeIsPassed;
	void Awake()
	{
		_itemRb = GetComponent<Rigidbody>();
		_itemTransform = GetComponent<Transform>();
	}

	void OnEnable()
	{
		_delayTimeIsPassed = false;
		_time = 0;
	}

	void Update()
	{
		if (_time < delayTime)
		{
			_time += Time.deltaTime;
			return;
		}
		else if (!_delayTimeIsPassed)
		{
			_initialVelocity = _itemRb.velocity;
			_delayTimeIsPassed = true;
		}

		if (elapsedTime < reduceTime)
		{
			elapsedTime += Time.deltaTime;
			_itemRb.velocity = Vector3.Lerp(_initialVelocity, Vector3.zero, elapsedTime / reduceTime);
		}
		else
		{
			_liftableCollider.enabled = true;
			_disposableCollider.enabled = false;
			_itemTransform.rotation = Quaternion.Euler(Vector3.zero);
			elapsedTime = 0;
			enabled = false;
		}
	}
}