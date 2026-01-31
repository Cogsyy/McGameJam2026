using UnityEngine;

public class JobPosting : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _jobTitleText;
    [SerializeField] private TMPro.TMP_Text _companyNameText;
    [SerializeField] private TMPro.TMP_Text _jobDescriptionText;
    [SerializeField] private TMPro.TMP_Text _locationText;

    JobPostingData _jobPostingData;

	public void SetJobPostingData(JobPostingData data)
    {
        if(!data) return;
		_jobPostingData = data;
        _jobTitleText.text = data.JobTitle;
        _companyNameText.text = data.CompanyName;
        _jobDescriptionText.text = data.JobDescription;
        _locationText.text = data.Location;
	}

    public void OnChosenJob()
    {
        if(_jobPostingData == null) return;
		FindAnyObjectByType<DialogueManager>().SetJobInterviewPostingData(_jobPostingData);
    }
}
