using UnityEngine;

[CreateAssetMenu(fileName = "DayJobData", menuName = "JobPostings/Day Job Data", order = 1)]
public class DayJobData : ScriptableObject
{
    public string JobTitle;
    public string CompanyName;
    public string JobDescription;
    public int moneyEarned;
}
