using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
{
    public int dayCount = 0;
    public TextMeshProUGUI dayLabel;
    //public GameController gameController;
    public int numberJobPostings = 3;

    [SerializeField] private ShopPage _shop;
    [SerializeField] private GuruPage _guru;


    void Start()
    {
        AdvanceDay();
    }

    public void AdvanceDay()
    {
        UpdateDayDisplay(dayCount++);

		JobPostingManager jobPostingManager = null;
		if(jobPostingManager = FindAnyObjectByType<JobPostingManager>(FindObjectsInactive.Include))
		{
			jobPostingManager.InitializeJobPostings(numberJobPostings);
		}

        _shop.Restock();
        _guru.Restock();
	}

    public void UpdateDayDisplay(int currentDay)
    {
        if(!dayLabel) return;
		dayLabel.text = "Days J*bless: " + currentDay;
    }
}