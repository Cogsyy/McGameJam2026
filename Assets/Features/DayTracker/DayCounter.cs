using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
{
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
	}

    public void UpdateDayDisplay(int currentDay)
    {
        if(!dayLabel) return;
		dayLabel.text = "Days J*bless: " + currentDay;
    }
}