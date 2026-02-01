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
        labels[0].text = "programming level: " + Player.Instance.programmingSkill;
        labels[1].text = "art level: " + Player.Instance.artSkill;
        labels[2].text = "design level: " + Player.Instance.designSkill;
        labels[3].text = "music level: " + Player.Instance.musicSkill;
    }
}
