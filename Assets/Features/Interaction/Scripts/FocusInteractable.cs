using UnityEngine;

public class FocusInteractable : SimpleInteractable
{
	[Header("Focus Settings")]
	[SerializeField] private Transform _viewPoint;
	[SerializeField] private float _heightOffset = 1.6f;
	[SerializeField] private bool _lookAtTarget = true;
	[SerializeField] private float _focusDuration = 0.5f;

	protected override void Reset()
	{
		base.Reset();
		
		if (_viewPoint == null)
		{
			// Check if a child already exists and use that
			_viewPoint = transform.Find("ViewPoint");
			if (_viewPoint == null)
			{
				// Create a new child transform viewpoint as a default
				_viewPoint = new GameObject("ViewPoint").transform;
				_viewPoint.parent = transform;
				
				// Place the viewpoint in front of the object where someone could stand in front of
				_viewPoint.localPosition = Vector3.forward * 2;
				_viewPoint.localRotation = Quaternion.identity;
			}
		}
	}

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
			Quaternion targetRotation;

			// Calculate rotation to look at the object
			if (_lookAtTarget)
			{
				Vector3 directionToTarget = (transform.position - targetPosition).normalized;
				targetRotation = Quaternion.LookRotation(directionToTarget);
			}
			else
			{
				targetRotation = _viewPoint.rotation;
			}

			cam.MoveToPosition(targetPosition, targetRotation, _focusDuration);
		}
	}
}
