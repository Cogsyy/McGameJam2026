using UnityEngine;

public class Gambling
{
	private int _die1;
	private int _die2;
	private bool _isWin;

	public int Total => _die1 + _die2;
	public bool IsWin => _isWin;

	public bool Play(int betAmount)
	{
        Player.Instance.TryRemoveMoney(betAmount);

		_die1 = Random.Range(1, 7);
		_die2 = Random.Range(1, 7);
		
		_isWin = (Total == 7);
		
		Debug.Log($"Rolled {_die1} and {_die2} (Total: {Total}). Result: {(_isWin ? "WIN" : "LOSE")}");
		return _isWin;
	}
}
