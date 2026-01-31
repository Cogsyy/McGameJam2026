using UnityEngine;

public class FocusInteractable : SimpleInteractable
{
	[Header("Focus Settings")]
	[SerializeField] private Transform _viewPoint;
	[SerializeField] private float _heightOffset = 1.6f;

    protected override void OnValidate()
    {
        base.OnValidate();
        if (_viewPoint == null)
        {
            //Create a new child transform viewpoint as a default
            _viewPoint = new GameObject("ViewPoint").transform;
            _viewPoint.parent = transform;
            //Place the viewpoint in front of the object where someone could stand in front of
            _viewPoint.localPosition = Vector3.forward * 2;
            _viewPoint.localRotation = Quaternion.identity;
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
			cam.SetPosition(targetPosition, _viewPoint.rotation);
		}
	}
}
