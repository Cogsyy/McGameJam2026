using UnityEngine;

public class StatTest : MonoBehaviour
{

    public int daysPassed = 0;
    public DayCounter dayCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            daysPassed++;
            dayCounter.UpdateDayDisplay(daysPassed);
        }
    }
}
