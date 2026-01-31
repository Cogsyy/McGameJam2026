using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private float _mouseSensitivity = 100f;
	[SerializeField] private float _minVerticalAngle = -90f;
	[SerializeField] private float _maxVerticalAngle = 90f;

	private float _verticalRotation = 0f;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

		// Horizontal rotation (Yaw)
		transform.Rotate(Vector3.up * mouseX);

		// Vertical rotation (Pitch)
		_verticalRotation -= mouseY;
		_verticalRotation = Mathf.Clamp(_verticalRotation, _minVerticalAngle, _maxVerticalAngle);

		// Applying pitch to the camera object
		transform.localRotation = Quaternion.Euler(_verticalRotation, transform.localEulerAngles.y, 0f);
	
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Cursor.lockState == CursorLockMode.None)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
	}

	public void SetPosition(Vector3 position, Quaternion rotation)
	{
		transform.position = position;
		transform.rotation = rotation;
		
		// Reset internal vertical rotation to match the new orientation
		_verticalRotation = rotation.eulerAngles.x;
		if (_verticalRotation > 180f)
		{
			_verticalRotation -= 360f;
		}
	}
}
