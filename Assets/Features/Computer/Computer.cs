using UnityEngine;

public class Computer : FocusInteractable
{
	[SerializeField] private Canvas _computerCanvas;
	[SerializeField] private GamblingPage _gamblingPage;
    [SerializeField] private HorseRacingPage _horeRacingPage;
	[SerializeField] private ShopPage _shopPage;

	private void Start()
	{
		if (_gamblingPage != null)
		{
			_gamblingPage.gameObject.SetActive(false);
		}
		if (_horeRacingPage != null)
		{
			_horeRacingPage.gameObject.SetActive(false);
		}
		if (_shopPage != null)
		{
			_shopPage.gameObject.SetActive(false);
		}
	}

    protected override void Reset()
	{
		base.Reset();
		cursorVisibleOnInteract = true;
	}

	public override void Interact()
	{
		base.Interact();
		
		if (_computerCanvas != null)
		{
			_computerCanvas.enabled = true;
		}
	}

	public override void OnExitInteractable()
	{
		base.OnExitInteractable();
		
		if (_computerCanvas != null)
		{
			_computerCanvas.enabled = false;
		}
	}

	public void OnClickGambling()
	{
		if (_gamblingPage != null)
		{
			_gamblingPage.gameObject.SetActive(true);
		}
	}

    public void OnClickHorseRacing()
    {
        if (_horeRacingPage != null)
        {
            _horeRacingPage.gameObject.SetActive(true);
        }
    }

	public void OnClickShop()
    {
        if (_shopPage != null)
        {
            _shopPage.gameObject.SetActive(true);
        }
    }
}
