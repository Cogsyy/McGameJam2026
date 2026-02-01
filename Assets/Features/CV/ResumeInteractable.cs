using UnityEngine;

public class ResumeInteractable : FocusInteractable
{
    [SerializeField] private ResumeIntroAnimation _resumeAnimation;
    
    public override void Interact()
    {
        base.Interact();
        _resumeAnimation.PlayAnimationBackwards(OnInteractComplete);
    }

    private void OnInteractComplete()
    {
        
    }
}
