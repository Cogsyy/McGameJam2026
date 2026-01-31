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
		// Note: This script assumes it's attached directly to the Camera. 
		// If attached to a parent body, you'd usually rotate a child camera object for pitch.
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
}
