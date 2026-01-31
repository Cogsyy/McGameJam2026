using TMPro;
using UnityEngine;

public class SkillsDisplay : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI[] labels;
    //public GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplaySkills();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplaySkills()
    {
        // set the level to the one saved in gamecontroller
        labels[0].text = "programming";
        labels[1].text = "art";
        labels[2].text = "design";
        labels[3].text = "music";
    }
}
