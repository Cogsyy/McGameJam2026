using System;
using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable
{
	[Header("Settings")]
	[SerializeField] private string _message = "Interacted!";
	[SerializeField] private Material _highlightMaterial;
	[SerializeField] protected bool cursorVisibleOnInteract = false;

	[Header("References")]
	[SerializeField] private Renderer _renderer;

	private Material _originalMaterial;

	public Action OnInteract;

	protected virtual void Reset()
	{
		if (_renderer == null)
		{
			_renderer = GetComponent<Renderer>();
			
			// Look for default highlight material in the project
			_highlightMaterial = Resources.Load<Material>("Highlighted");
		}
	}

	protected virtual void Start()
	{
		if (_renderer == null)
		{
			_renderer = GetComponent<Renderer>();
		}

		if (_renderer != null)
		{
			_originalMaterial = _renderer.sharedMaterial;
		}
	}

	public virtual bool CanInteract()
	{
		return true;
	}

	public virtual void Interact()
	{
		Debug.Log($"{gameObject.name}: {_message}");
		
		if (cursorVisibleOnInteract)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		OnInteract?.Invoke();
	}

	public virtual void OnHoverEnter()
	{
		if (_renderer != null && _highlightMaterial != null && _renderer.sharedMaterial != _highlightMaterial)
		{
			_renderer.sharedMaterial = _highlightMaterial;
		}
	}

	public virtual void OnHoverExit()
	{
		if (_renderer != null && _originalMaterial != null && _renderer.sharedMaterial != _originalMaterial)
		{
			_renderer.sharedMaterial = _originalMaterial;
		}
	}

	public virtual void OnExitInteractable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
