using System.Globalization;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SimpleInteractable>().OnInteract += () =>
        {
            FindAnyObjectByType<DayCounter>().AdvanceDay();
            FindAnyObjectByType<AreaMovement>().GoToJobBoard();
        };
	}
}
