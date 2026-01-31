using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum JobType
{
    None,
    Artist,
    Programmer,
    Designer,
    Musician
}

[CreateAssetMenu(fileName = "JobPostingData", menuName = "JobPostings/JobPostingData")]
public class JobPostingData : ScriptableObject
{
    [SerializeField] private GUID _jobID;
	[SerializeField] private string _jobTitle;
    [SerializeField] private string _companyName;
    [SerializeField] private string _jobDescription;
    [SerializeField] private string _location;

    [SerializeField, Tooltip("To tag specific data")] private string _dataTag = string.Empty;
    [SerializeField] private JobType _jobType;

    public GUID JobID => _jobID;
    public string JobTitle => _jobTitle;
    public string CompanyName => _companyName;
    public string JobDescription => _jobDescription;
    public string Location => _location;
    public JobType JobType => _jobType;

	void OnEnable()
    {
        if(JobID.Empty())
			_jobID = GUID.Generate();
    }
}
