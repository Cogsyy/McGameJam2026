using System.Collections.Generic;
using UnityEngine;

public class JobPostingManager : MonoBehaviour
{
    [SerializeField]
    private List<JobPosting> jobPostingObjects;

    [SerializeField]
    private List<JobPostingData> jobPostingData;

    public void InitializeJobPostings(int nbPostings)
    {
        for(int i = 0; i < jobPostingObjects.Count; i++)
        {
            jobPostingObjects[i].gameObject.SetActive(false);
		}

		int nbPostingsToInitialize = Mathf.Min(nbPostings, jobPostingObjects.Count);

        for (int i = 0; i < nbPostingsToInitialize; i++)
        {
            JobPosting postingObject = jobPostingObjects[i];
			JobPostingData data = jobPostingData[Random.Range(0, jobPostingData.Count)];
			postingObject.SetJobPostingData(data);
			postingObject.gameObject.SetActive(true);
		}
	}
}
