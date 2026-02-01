using TMPro;
using UnityEngine;
using System.Collections;

public class HorseRacingPage : MonoBehaviour
{
    private HorseRacing _horseRacingGame;
    [SerializeField] private TMP_Text _resultText;
    [Header("SFX")]
    [SerializeField] private AudioClip _rollingSFX;
    [SerializeField] private AudioClip _winSFX;
    [SerializeField] private AudioClip _loseSFX;

    private Coroutine _hideResultTextCoroutine;
    private Coroutine _betCoroutine;
    private int _horseChoice = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _horseRacingGame = new HorseRacing();
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

            _resultText.text = "Racing...";
            _resultText.enabled = true;

            yield return new WaitForSeconds(1f);

            Debug.Log("Betting: " + betAmount);
            bool isWin = _horseRacingGame.Play(betAmount);
            if (isWin)
            {
                if (_winSFX != null && AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(_winSFX);
                }
                Player.Instance.AddMoney(betAmount * 3);
                _resultText.text = "You win " + (betAmount * 3).ToString() + "!";
            }
            else
            {
                if (_loseSFX != null && AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(_loseSFX);
                }
                _resultText.text = "Your horse: " + _horseChoice + " another horse won.";
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
