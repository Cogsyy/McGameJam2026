using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	private static AudioManager _instance;

	[Header("Mixer")]
	[SerializeField] private AudioMixer _mixer;
	[SerializeField] private string _masterVolumeParam = "MasterVolume";
	[SerializeField] private string _musicVolumeParam = "MusicVolume";
	[SerializeField] private string _sfxVolumeParam = "SFXVolume";

	[Header("Sources")]
	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioSource _sfxSource;

	private bool _isMuted;
	private float _prevMasterVolume;

	public static AudioManager Instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}

		_instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void PlayMusic(AudioClip clip, bool loop = true)
	{
		if (_musicSource == null)
		{
            Debug.LogError("AudioManager: No music source found!");
			return;
		}

		_musicSource.clip = clip;
		_musicSource.loop = loop;
		_musicSource.Play();
	}

	public void PlaySFX(AudioClip clip)
	{
		if (_sfxSource == null)
		{
            Debug.LogError("AudioManager: No SFX source found!");
			return;
		}

		_sfxSource.PlayOneShot(clip);
	}

	public void PlaySFXAtPosition(AudioClip clip, Vector3 position)
	{
		AudioSource.PlayClipAtPoint(clip, position);
	}

	public void SetMasterVolume(float volume)
	{
		SetMixerVolume(_masterVolumeParam, volume);
	}

	public void SetMusicVolume(float volume)
	{
		SetMixerVolume(_musicVolumeParam, volume);
	}

	public void SetSFXVolume(float volume)
	{
		SetMixerVolume(_sfxVolumeParam, volume);
	}

	public void ToggleMute()
	{
		_isMuted = !_isMuted;
		
		if (_isMuted)
		{
			_mixer.GetFloat(_masterVolumeParam, out _prevMasterVolume);
			_mixer.SetFloat(_masterVolumeParam, -80f);
		}
		else
		{
			_mixer.SetFloat(_masterVolumeParam, _prevMasterVolume);
		}
	}

	private void SetMixerVolume(string parameter, float volume)
	{
		if (_mixer == null)
		{
			return;
		}

		// Convert 0.0-1.0 to Logarithmic Decibels (-80dB to 0dB)
		float db = volume > 0.0001f ? Mathf.Log10(volume) * 20f : -80f;
		_mixer.SetFloat(parameter, db);
	}
}
