using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image itemIcon;
    public GameObject equippedIndicator; // Checkmark or outline
    public Button slotButton;
    [SerializeField] public DressingUp previewGuy;

    private ClothingItem clothingItem;
    //private WardrobeManager wardrobeManager;

    public void Setup(ClothingItem item)
    {
        clothingItem = item;

        itemIcon.sprite = item.itemIcon;
        equippedIndicator.SetActive(item.isEquipped);

        //slotButton.onClick.AddListener(() => OnSlotClicked());
    }

    public void OnSlotClicked()
    {
        previewGuy.ChangeClothing(clothingItem);
    }
}