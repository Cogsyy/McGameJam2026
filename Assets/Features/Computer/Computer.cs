using UnityEngine;

public class Computer : FocusInteractable
{
    [SerializeField] private Canvas _computerCanvas;
    
    public override void Interact()
    {
        base.Interact();
        _computerCanvas.enabled = true;
    }

    public override void OnExitInteractable()
    {
        base.OnExitInteractable();
        _computerCanvas.enabled = false;
    }
}
