using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResumeInteractable : FocusInteractable
{
    [SerializeField] private ResumeIntroAnimation _resumeAnimation;
    [SerializeField] private Canvas _cvCanvas;
    
    private void Start()
    {
        _cvCanvas.enabled = false;
    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void OnExitInteractable()
    {
        base.OnExitInteractable();
        _cvCanvas.enabled = false;
    }

    protected override void OnFocusComplete()
    {
        base.OnFocusComplete();
        _cvCanvas.enabled = true;
    }
}
