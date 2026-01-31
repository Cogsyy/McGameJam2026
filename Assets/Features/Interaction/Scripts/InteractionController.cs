using UnityEngine;
using TMPro;

public class InteractionController : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private float _interactionRange = 3f;
	[SerializeField] private LayerMask _interactableLayer;
	[SerializeField] private KeyCode _interactionKey = KeyCode.F;

	[Header("References")]
	[SerializeField] private Camera _camera;
	[SerializeField] private TMP_Text _interactionPromptUI;

	private IInteractable _currentLookingInteractable;
    private IInteractable _currentInteractable;

	private void Update()
	{
		PerformInteractionCheck();

		if (Input.GetKeyDown(_interactionKey) || Input.GetMouseButtonDown(0))
		{
			if (_currentLookingInteractable != null)
			{
                _currentInteractable = _currentLookingInteractable;
				_currentInteractable.Interact();
			}
		}
	}

	private void PerformInteractionCheck()
	{
		if (_camera == null)
		{
			return;
		}

		Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, _interactionRange, _interactableLayer))
		{
			IInteractable interactable = hit.collider.GetComponent<IInteractable>();
			if (interactable != null)
			{
				if (_currentLookingInteractable != interactable && _currentInteractable != interactable && interactable.CanInteract())
				{
					ClearCurrentLookingInteractable();
					_currentLookingInteractable = interactable;
					_currentLookingInteractable.OnHoverEnter();
					
					if (_interactionPromptUI != null)
					{
						_interactionPromptUI.text = "Press " + _interactionKey.ToString() + " or left mouse to interact";
						_interactionPromptUI.gameObject.SetActive(true);
					}
				}
				
				return;
			}
		}

		ClearCurrentLookingInteractable();
	}

	private void ClearCurrentLookingInteractable()
	{
		if (_currentLookingInteractable != null)
		{
			_currentLookingInteractable.OnHoverExit();
			_currentLookingInteractable = null;
			
			if (_interactionPromptUI != null)
			{
				_interactionPromptUI.gameObject.SetActive(false);
			}
		}
	}
}
