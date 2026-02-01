using UnityEngine;

public class Computer : FocusInteractable
{
	[SerializeField] private Canvas _computerCanvas;
	[SerializeField] private GamblingPage _gamblingPage;
    [SerializeField] private HorseRacingPage _horeRacingPage;
	[SerializeField] private ShopPage _shopPage;
	[SerializeField] private GuruPage _guruPage;
	[SerializeField] private AudioClip _computerSound;
	[SerializeField] private AudioClip _computerMusic;

	protected override void Start()
	{
		base.Start();
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
		AudioManager.Instance.PlaySFX(_computerSound);
		AudioManager.Instance.PlayMusic(_computerMusic, true);
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
		if (_guruPage != null)
		{
			_guruPage.gameObject.SetActive(false);
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

	public void OnClickGuru()
	{
		if (_guruPage != null)
		{
			_guruPage.gameObject.SetActive(true);
		}
	}
}
