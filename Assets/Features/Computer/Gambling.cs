using UnityEngine;

public class Gambling
{
	private int _die1;
	private int _die2;
	private bool _isWin;

	public int Total => _die1 + _die2;
	public bool IsWin => _isWin;

    private int winMultiplier = 6;

	public void Play()
	{
        if (!Player.Instance.TryRemoveMoney(10))
        {
            Debug.Log("Not enough money!");
            return;
        }

		_die1 = Random.Range(1, 7);
		_die2 = Random.Range(1, 7);
		
		_isWin = (Total == 7);
		
		Debug.Log($"Rolled {_die1} and {_die2} (Total: {Total}). Result: {(_isWin ? "WIN" : "LOSE")}");
	}
}
