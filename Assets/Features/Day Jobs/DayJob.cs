using UnityEngine;


public class DayJob : MonoBehaviour
{
	[SerializeField] private TMPro.TMP_Text _jobTitleText;
	[SerializeField] private TMPro.TMP_Text _companyNameText;
	[SerializeField] private TMPro.TMP_Text _jobDescriptionText;
	[SerializeField] private TMPro.TMP_Text _moneyText;

	DayJobData dayJobData;

	public void SetDayJobData(DayJobData data)
	{
		if(!data)
			return;
		dayJobData = data;
		_jobTitleText.text = data.JobTitle;
		_companyNameText.text = data.CompanyName;
		_jobDescriptionText.text = data.JobDescription;
		_moneyText.text = data.moneyEarned.ToString();
	}

	public void OnChosenJob()
	{
		if(dayJobData == null)
		{
			Debug.LogWarning("JobPosting: No job posting data assigned!");
			return;
		}
		FindAnyObjectByType<Player>().AddMoney(dayJobData.moneyEarned);
		FindAnyObjectByType<AreaMovement>().GoHome();
	}
}
