using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
{
    public TextMeshProUGUI dayLabel;
    //public GameControlelr gameController;

    void Start()
    {
        UpdateDayDisplay(1);
    }

    public void UpdateDayDisplay(int currentDay)
    {
        dayLabel.text = "Days J*bless: " + currentDay;
    }
}