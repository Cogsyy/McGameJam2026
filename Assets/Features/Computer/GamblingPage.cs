using UnityEngine;

public class GamblingPage : MonoBehaviour
{
    private Gambling _gamblingGame;

    private void Start()
    {
        _gamblingGame = new Gambling();
    }

    public void OnClickPlay()
    {
        _gamblingGame.Play();
    }
}
