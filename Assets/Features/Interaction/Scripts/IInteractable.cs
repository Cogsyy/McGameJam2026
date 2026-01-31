public interface IInteractable
{
	bool CanInteract();
	void Interact();
	void OnExitInteractable();
	void OnHoverEnter();
	void OnHoverExit();
}
