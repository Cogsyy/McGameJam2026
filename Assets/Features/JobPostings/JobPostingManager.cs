using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JobPostingManager : MonoBehaviour
{
    [SerializeField]
    private List<JobPosting> jobPostingObjects;

    [SerializeField]
    private List<JobPostingData> jobPostingData;

    [SerializeField] private List<DayJobData> dayJobsData;
    [SerializeField] private List<DayJob> dayJobObjects;

    public void InitializeJobPostings(int nbPostings)
    {
        foreach(var jobPosting in jobPostingObjects)
        {
            jobPosting.gameObject.SetActive(false);
		}
        foreach(var dayJob in dayJobObjects)
        {
            dayJob.gameObject.SetActive(false);
		}


        bool hasDayJob = Random.Range(0, 1) < 0.5f;

        nbPostings -= 1;

		int nbPostingsToInitialize = Mathf.Min(nbPostings, jobPostingObjects.Count);

        // Get Unique random indexes
        HashSet<int> randomIndexes = new HashSet<int>();
        for(int i = 0; i < 100; ++i) // Avoid infinite loops
        {
            if(randomIndexes.Count >= nbPostingsToInitialize)
            {
                break;
            }
            int randomIndex = Random.Range(0, jobPostingData.Count);
            randomIndexes.Add(randomIndex);
        }

        int[] dataIndices = randomIndexes.ToArray();
		for (int i = 0; i < dataIndices.Length; i++)
        {
            JobPosting postingObject = jobPostingObjects[i];
			JobPostingData data = jobPostingData[dataIndices[i]];
			postingObject.SetJobPostingData(data);
			postingObject.gameObject.SetActive(true);
		}

        if (hasDayJob)
        {
            if(dayJobObjects.Count == 0 || dayJobsData.Count == 0)
            {
                Debug.LogWarning("No Day Jobs available to initialize.");
            }
            else
            {
                int randomDayJobIndex = Random.Range(0, dayJobsData.Count);
                DayJobData dayJobData = dayJobsData[randomDayJobIndex];
                DayJob dayJobObject = dayJobObjects[0];
                dayJobObject.SetDayJobData(dayJobData);
                dayJobObject.gameObject.SetActive(true);
            }
		}
	}
}
