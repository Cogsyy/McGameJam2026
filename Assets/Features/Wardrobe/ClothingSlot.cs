using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image itemIcon;
    public GameObject equippedIndicator; // Checkmark or outline
    public Button slotButton;

    private ClothingItem clothingItem;
    //private WardrobeManager wardrobeManager;

    public void Setup(ClothingItem item)
    {
        clothingItem = item;

        itemIcon.sprite = item.itemIcon;
        equippedIndicator.SetActive(item.isEquipped);

        slotButton.onClick.AddListener(() => OnSlotClicked());
    }

    private void OnSlotClicked()
    {
        //if (clothingItem.bodyPart.Equals("head"))
        //{
        //    if(currentHead == null)
        //    {
        //        clothingItem.isEquipped = true;
        //        currentHead = clothingItem;
        //    }
        //    else
        //    {
        //        currentHead.isEquipped = false;
        //        clothingItem.isEquipped = true;
        //        currentHead = clothingItem;
        //    }
        //}else if (clothingItem.bodyPart.Equals("body"))
        //{
        //    if (currentBody == null)
        //    {
        //        clothingItem.isEquipped = true;
        //        currentBody = clothingItem;
        //    }
        //    else
        //    {
        //        currentBody.isEquipped = false;
        //        clothingItem.isEquipped = true;
        //        currentBody = clothingItem;
        //    }
        //}
        //else if (clothingItem.bodyPart.Equals("lower"))
        //{
        //    if (currentLower == null)
        //    {
        //        clothingItem.isEquipped = true;
        //        currentLower = clothingItem;
        //    }
        //    else
        //    {
        //        currentLower.isEquipped = false;
        //        clothingItem.isEquipped = true;
        //        currentLower = clothingItem;
        //    }
        //}
        //equip item through game controller
    }
}