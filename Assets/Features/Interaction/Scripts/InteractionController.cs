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
	private FirstPersonCamera _fpCamera;

	private void Start()
	{
		if (_camera != null)
		{
			_fpCamera = _camera.GetComponent<FirstPersonCamera>();
		}
	}

	private void Update()
	{
		PerformInteractionCheck();

		if (Input.GetKeyDown(_interactionKey) || Input.GetMouseButtonDown(0))
		{
			if (_currentLookingInteractable != null)
			{
				_currentInteractable = _currentLookingInteractable;
				_currentLookingInteractable.OnHoverExit();
				_currentLookingInteractable = null;
				
				_currentInteractable.Interact();
				
				if (_fpCamera != null)
				{
					_fpCamera.SetMouseLookEnabled(false);
				}
				
				if (_interactionPromptUI != null)
				{
					_interactionPromptUI.gameObject.SetActive(false);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (_currentInteractable != null)
			{
				_currentInteractable.OnExitInteractable();
				_currentInteractable = null;
			}
			
			if (_fpCamera != null)
			{
				_fpCamera.SetMouseLookEnabled(true);
			}
		}
	}
	
	private void PerformInteractionCheck()
	{
		if (_camera == null || _currentInteractable != null)
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
				if (_currentLookingInteractable != interactable && interactable.CanInteract())
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
