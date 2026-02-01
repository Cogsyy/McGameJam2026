using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Wardrobe : MonoBehaviour
{
    public ItemSlot itemSlotPrefab;
    public Transform gridContainer;
    public DressingUp previewGuy;
    public GameStarter gameController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid()
    {
        // Clear existing items
        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }

        // Create slots for unlocked items
        //foreach (ClothingItem item in allClothingItems)
        //{
        //    if (item.isUnlocked)
        //    {
        //        GameObject slot = Instantiate(itemSlotPrefab, gridContainer);
        //        ItemSlot slotScript = slot.GetComponent<ItemSlot>();
        //        slotScript.Setup(item);
        //    }
        //}
        //replace this wo
        int shittyCounter = 0;
        foreach(ClothingItem clothing in gameController.ClothingItems){
            ItemSlot slot = Instantiate(itemSlotPrefab, gridContainer);
            slot.Setup(clothing);
            slot.slotButton.onClick.AddListener(slot.OnSlotClicked);
            slot.previewGuy = this.previewGuy;
            shittyCounter++;
        }
    }
}
