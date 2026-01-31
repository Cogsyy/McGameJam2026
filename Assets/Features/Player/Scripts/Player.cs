using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

	private static Player _instance;
	public static Player Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindFirstObjectByType<Player>();
				
				if (_instance == null)
				{
					GameObject go = new GameObject("Player");
					_instance = go.AddComponent<Player>();
				}
			}
			return _instance;
		}
	}

	[Header("Stats")]
	[SerializeField] public float money = 100f;

	public float Money => money;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
		}
	}

    private void Start()
    {
        _moneyText.text = "Money: $" + money.ToString();
    }

	public void AddMoney(float amount)
	{
		money += amount;
		_moneyText.text = "Money: $" + money.ToString();
	}

	public bool TryRemoveMoney(float amount)
	{
		if (money >= amount)
		{
			money -= amount;
			_moneyText.text = "Money: $" + money.ToString();
			return true;
		}
		
		Debug.LogWarning("Insufficient funds!");
		return false;
	}
}
