using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform gridContainer;
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
        while(shittyCounter < 10){
            GameObject slot = Instantiate(itemSlotPrefab, gridContainer);
            //ItemSlot slotScript = slot.GetComponent<ItemSlot>();
            //slotScript.Setup(item);
            shittyCounter++;
        }
    }
}
