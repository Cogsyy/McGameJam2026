using UnityEngine;

public class Computer : FocusInteractable
{
	[SerializeField] private Canvas _computerCanvas;
	[SerializeField] private GamblingPage _gamblingPage;

	protected override void Reset()
	{
		base.Reset();
		cursorVisibleOnInteract = true;
	}

	public override void Interact()
	{
		base.Interact();
		
		if (_computerCanvas != null)
		{
			_computerCanvas.enabled = true;
		}
	}

	public override void OnExitInteractable()
	{
		base.OnExitInteractable();
		
		if (_computerCanvas != null)
		{
			_computerCanvas.enabled = false;
		}
	}

	public void OnClickGambling()
	{
		if (_gamblingPage != null)
		{
			_gamblingPage.gameObject.SetActive(true);
		}
	}
}
