using UnityEngine;

public class FocusInteractable : SimpleInteractable
{
	[Header("Focus Settings")]
	[SerializeField] private Transform _viewPoint;
	[SerializeField] private float _heightOffset = 1.6f;

	public override void Interact()
	{
		base.Interact();

		if (_viewPoint == null)
		{
			return;
		}

		FirstPersonCamera cam = Camera.main.GetComponent<FirstPersonCamera>();
		if (cam != null)
		{
			Vector3 targetPosition = _viewPoint.position + Vector3.up * _heightOffset;
			cam.SetPosition(targetPosition, _viewPoint.rotation);
		}
	}
}
