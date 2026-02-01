using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
{
    public int dayCount = 0;
    public TextMeshProUGUI dayLabel;
    //public GameController gameController;
    public int numberJobPostings = 3;


    void Start()
    {
        AdvanceDay();
    }

    public void AdvanceDay()
    {
        UpdateDayDisplay(dayCount++);

		JobPostingManager jobPostingManager = null;
		if(jobPostingManager = FindAnyObjectByType<JobPostingManager>())
		{
			jobPostingManager.InitializeJobPostings(numberJobPostings);
		}
	}

    public void UpdateDayDisplay(int currentDay)
    {
        if(!dayLabel) return;
		dayLabel.text = "Days J*bless: " + currentDay;
    }
}