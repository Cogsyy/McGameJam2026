using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private ShopPage _shop;
    [SerializeField] private GuruPage _guru;

    public int dayCount = 0;
    public TextMeshProUGUI dayLabel;
    //public GameController gameController;

    void Start()
    {
        AdvanceDay();
    }

    public void AdvanceDay()
    {
        UpdateDayDisplay(dayCount++);

        _shop.Restock();
        _guru.Restock();
	}

    public void UpdateDayDisplay(int currentDay)
    {
        if(!dayLabel) return;
		dayLabel.text = "Days J*bless: " + currentDay;
    }
}