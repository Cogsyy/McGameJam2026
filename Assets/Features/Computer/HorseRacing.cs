using UnityEngine;

public class HorseRacing
{
	private int _horseSpeed1;
    private int _horseSpeed2;
    private int _horseSpeed3;
    private bool _isWin;
	private int _winner;
    public int horseChoice = 1;
	public bool IsWin => _isWin;

	public bool Play(int betAmount)
	{
        Player.Instance.TryRemoveMoney(betAmount);

        _horseSpeed1 = Random.Range(1, 10);
        _horseSpeed2 = Random.Range(1, 10);
		_horseSpeed3 = Random.Range(1, 10);

		int horseProgression1 = 0;
        int horseProgression2 = 0;
        int horseProgression3 = 0;

        while (horseProgression1/100 < 1 && horseProgression2 / 100 < 1 && horseProgression3 / 100 < 1)
		{
			horseProgression1 += _horseSpeed1;
            horseProgression2 += _horseSpeed2;
            horseProgression3 += _horseSpeed3;
            _horseSpeed1 = Random.Range(1, 10);
            _horseSpeed2 = Random.Range(1, 10);
            _horseSpeed3 = Random.Range(1, 10);
        }
        if(horseProgression1 >= 100)
        {
            _winner = 1;
        }
        if (horseProgression2 >= 100)
        {
            _winner = 2;
        }
        if (horseProgression3 >= 100)
        {
            _winner = 3;
        }

        _isWin = (_winner == horseChoice);
		
		Debug.Log($"Speeds: {_horseSpeed1} and {_horseSpeed2} and {_horseSpeed3} (winner: {_winner}). Result: {(_isWin ? "WIN" : "LOSE")}");
		return _isWin;
	}
}
