using UnityEngine;
using System.Collections;

public class FirstPersonCamera : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private float _mouseSensitivity = 100f;
	[SerializeField] private float _minVerticalAngle = -90f;
	[SerializeField] private float _maxVerticalAngle = 90f;

	private float _rotationX = 0f;
	private float _rotationY = 0f;
	private bool _isMouseLookEnabled = true;
	public bool IsMouseLookEnabled => _isMouseLookEnabled;
	private bool _skipFrame = false;
	private Coroutine _movementCoroutine;

	private Vector3 _previousPosition;
	private Quaternion _previousRotation;
	private bool _isFocused = false;
	private float _currentFocusDuration = 0.5f;

	private void Start()
	{
		SyncRotationState();
	}

	private void Update()
	{
		if (_isMouseLookEnabled)
		{
			if (!_skipFrame)
			{
				float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
				float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

				_rotationY += mouseX;
				_rotationX -= mouseY;
				_rotationX = Mathf.Clamp(_rotationX, _minVerticalAngle, _maxVerticalAngle);

				transform.localRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
			}
			else
			{
				_skipFrame = false;
			}
		}

		if (_isFocused && Input.GetKeyDown(KeyCode.Escape))
		{
			ReturnToPreviousPosition();
		}
	}

	public void SetPosition(Vector3 position, Quaternion rotation)
	{
		StopActiveTransition();

		transform.position = position;
		transform.rotation = rotation;

		SyncRotationState();
	}

	public void MoveToPosition(Vector3 targetPosition, Quaternion targetRotation, float duration)
	{
		StopActiveTransition();

		if (!_isFocused)
		{
			_previousPosition = transform.position;
			_previousRotation = transform.rotation;
			_isFocused = true;
			_isMouseLookEnabled = false;
		}

		_currentFocusDuration = duration;
		_movementCoroutine = StartCoroutine(MoveToPositionCoroutine(targetPosition, targetRotation, duration));
	}

	private void ReturnToPreviousPosition()
	{
		if (!_isFocused)
		{
			return;
		}

		StopActiveTransition();
		_movementCoroutine = StartCoroutine(ReturnToPreviousCoroutine(_currentFocusDuration));
	}

	private IEnumerator ReturnToPreviousCoroutine(float duration)
	{
		yield return StartCoroutine(MoveToPositionCoroutine(_previousPosition, _previousRotation, duration));
		
		_isFocused = false;
		SetMouseLookEnabled(true);
	}

	private IEnumerator MoveToPositionCoroutine(Vector3 targetPosition, Quaternion targetRotation, float duration)
	{
		Vector3 startPosition = transform.position;
		Quaternion startRotation = transform.rotation;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			float t = elapsed / duration;
			
			// Use smooth step for better feel
			t = t * t * (3f - 2f * t);

			transform.position = Vector3.Lerp(startPosition, targetPosition, t);
			transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

			yield return null;
		}
		transform.position = targetPosition;
		transform.rotation = targetRotation;
		
		SyncRotationState();
		_movementCoroutine = null;
	}

	private void StopActiveTransition()
	{
		if (_movementCoroutine != null)
		{
			StopCoroutine(_movementCoroutine);
			_movementCoroutine = null;
		}
	}

	private void SyncRotationState()
	{
		Vector3 euler = transform.localEulerAngles;
		
		_rotationX = euler.x;
		if (_rotationX > 180f) _rotationX -= 360f;
		
		_rotationY = euler.y;
		if (_rotationY > 180f) _rotationY -= 360f;
	}

	public void SetMouseLookEnabled(bool enabled)
	{
		_isMouseLookEnabled = enabled;
		if (enabled)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			_skipFrame = true;
			SyncRotationState();
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
