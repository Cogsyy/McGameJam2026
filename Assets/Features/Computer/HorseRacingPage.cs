using TMPro;
using UnityEngine;
using System.Collections;

public class HorseRacingPage : MonoBehaviour
{
    private HorseRacing _horseRacingGame;
    [SerializeField] private TMP_Text _resultText;
    private Coroutine _hideResultTextCoroutine;
    private int _horseChoice = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _horseRacingGame = new HorseRacing();
    }

    public void OnClickBetButton(int betAmount)
    {
        if (Player.Instance.money >= betAmount)
        {
            Debug.Log("Betting: " + betAmount);
            bool isWin = _horseRacingGame.Play(betAmount);
            if (isWin)
            {
                Player.Instance.AddMoney(betAmount * 3);
                _resultText.text = "You win " + (betAmount * 3).ToString() + "!";
            }
            else
            {
                _resultText.text = "Your horse: " + _horseChoice + " another horse won.";
            }

            _resultText.enabled = true;
        }
        else
        {
            _resultText.text = "Not enough money!";
            _resultText.enabled = true;
        }

        if (_hideResultTextCoroutine != null)
        {
            StopCoroutine(_hideResultTextCoroutine);
        }
        _hideResultTextCoroutine = StartCoroutine(HideResultText());
    }

    public void OnClickHorseButton(int horseChoiceButton)
    {
        _horseChoice = horseChoiceButton;
        _horseRacingGame.horseChoice = horseChoiceButton;
    }

    private IEnumerator HideResultText()
    {
        yield return new WaitForSeconds(2f);
        _resultText.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
