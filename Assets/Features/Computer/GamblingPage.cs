using UnityEngine;
using TMPro;
using System.Collections;

public class GamblingPage : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;
    private Gambling _gamblingGame;

    private Coroutine _hideResultTextCoroutine;

    private void Start()
    {
        _gamblingGame = new Gambling();
    }

    public void OnClickBetButton(int betAmount)
    {
        if (Player.Instance.money >= betAmount)
        {
            Debug.Log("Betting: " + betAmount);
            bool isWin = _gamblingGame.Play(betAmount);
            if (isWin)
            {
                Player.Instance.AddMoney(betAmount * 4);
                _resultText.text = "You win " + (betAmount * 4).ToString() + "!";
            }
            else
            {
                _resultText.text = "Try again";
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

    private IEnumerator HideResultText()
    {
        yield return new WaitForSeconds(2f);
        _resultText.enabled = false;
    }
}
