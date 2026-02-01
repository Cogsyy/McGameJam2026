using UnityEngine;
using TMPro;
using System.Collections;

public class GamblingPage : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;
    [Header("SFX")]
    [SerializeField] private AudioClip _rollingSFX;
    [SerializeField] private AudioClip _winSFX;
    [SerializeField] private AudioClip _loseSFX;

    private Gambling _gamblingGame;

    private Coroutine _hideResultTextCoroutine;
    private Coroutine _betCoroutine;

    private void Start()
    {
        _gamblingGame = new Gambling();
    }

    public void OnClickBetButton(int betAmount)
    {
        if (_betCoroutine != null) return;
        _betCoroutine = StartCoroutine(HandleBet(betAmount));
    }

    private IEnumerator HandleBet(int betAmount)
    {
        if (Player.Instance.money >= betAmount)
        {
            if (_rollingSFX != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySFX(_rollingSFX);
            }

            _resultText.text = "Rolling...";
            _resultText.enabled = true;

            yield return new WaitForSeconds(2f);

            Debug.Log("Betting: " + betAmount);
            bool isWin = _gamblingGame.Play(betAmount);
            if (isWin)
            {
                if (_winSFX != null && AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(_winSFX);
                }
                Player.Instance.AddMoney(betAmount * 4);
                _resultText.text = "You win " + (betAmount * 4).ToString() + "!";
            }
            else
            {
                if (_loseSFX != null && AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(_loseSFX);
                }
                _resultText.text = "Try again";
            }
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
        _betCoroutine = null;
    }

    private IEnumerator HideResultText()
    {
        yield return new WaitForSeconds(2f);
        _resultText.enabled = false;
    }
}
