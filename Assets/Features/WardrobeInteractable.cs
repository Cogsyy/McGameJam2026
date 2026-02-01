using UnityEngine;

public class WardrobeInteractable : SimpleInteractable
{
    [SerializeField] private AreaMovement _movement;

    public override void Interact()
    {
        _movement.GoToWardrobe();
    }
}
