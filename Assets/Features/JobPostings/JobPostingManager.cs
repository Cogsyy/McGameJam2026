using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JobPostingManager : MonoBehaviour
{
    [SerializeField]
    private List<JobPosting> jobPostingObjects;

    [SerializeField]
    private List<JobPostingData> jobPostingData;

    public void InitializeJobPostings(int nbPostings)
    {


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

		// Deactivate all job postings first
		for(int i = 0; i < jobPostingObjects.Count; i++)
		{
			jobPostingObjects[i].gameObject.SetActive(false);
		}

        int[] dataIndices = randomIndexes.ToArray();
		for (int i = 0; i < dataIndices.Length; i++)
        {
            JobPosting postingObject = jobPostingObjects[i];
			JobPostingData data = jobPostingData[dataIndices[i]];
			postingObject.SetJobPostingData(data);
			postingObject.gameObject.SetActive(true);
		}
	}
}
