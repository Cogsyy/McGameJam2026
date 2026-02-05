using System.Collections;
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
	[SerializeField] private AudioSource _musicSourceSecondary;
	[SerializeField] private AudioSource _sfxSource;

	[Header("Music Crossfade")]
	[SerializeField] [Min(0f)] private float _musicCrossfadeDuration = 1f;

	private bool _isMuted;
	private float _prevMasterVolume;
	private Coroutine _musicFadeCoroutine;
	private AudioSource _activeMusicSource;
	private AudioSource _inactiveMusicSource;

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
		EnsureSecondaryMusicSource();
	}

	public void PlayMusic(AudioClip clip, bool loop = true)
	{
		if (_musicSource == null)
		{
			Debug.LogError("AudioManager: No music source found!");
			return;
		}

		EnsureSecondaryMusicSource();

		if (_musicSourceSecondary == null)
		{
			PlayMusicImmediate(_musicSource, clip, loop);
			return;
		}

		if (_musicFadeCoroutine != null)
		{
			StopCoroutine(_musicFadeCoroutine);
			_musicFadeCoroutine = null;
		}

		AudioSource fromSource = GetLoudestMusicSource();
		AudioSource toSource = GetOtherMusicSource(fromSource);

		if (fromSource != null && fromSource.clip == clip && fromSource.isPlaying)
		{
			fromSource.loop = loop;
			fromSource.volume = 1f;
			if (toSource != null)
			{
				toSource.Stop();
				toSource.volume = 1f;
			}
			return;
		}

		if (_musicCrossfadeDuration <= 0f || fromSource == null || !fromSource.isPlaying)
		{
			if (fromSource != null)
			{
				fromSource.Stop();
				fromSource.volume = 1f;
			}

			PlayMusicImmediate(toSource ?? _musicSource, clip, loop);
			_activeMusicSource = toSource ?? _musicSource;
			_inactiveMusicSource = GetOtherMusicSource(_activeMusicSource);
			return;
		}

		_musicFadeCoroutine = StartCoroutine(CrossfadeRoutine(fromSource, toSource, clip, loop));
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

	private void EnsureSecondaryMusicSource()
	{
		if (_musicSource == null)
		{
			return;
		}

		if (_musicSourceSecondary == null)
		{
			GameObject clone = Instantiate(_musicSource.gameObject, transform);
			clone.name = "MusicSourceSecondary";
			clone.transform.localPosition = Vector3.zero;
			clone.transform.localRotation = Quaternion.identity;
			clone.transform.localScale = Vector3.one;

			_musicSourceSecondary = clone.GetComponent<AudioSource>();
			_musicSourceSecondary.playOnAwake = false;
			_musicSourceSecondary.clip = null;
			_musicSourceSecondary.Stop();
		}

		if (_activeMusicSource == null)
		{
			_activeMusicSource = _musicSource;
		}

		if (_inactiveMusicSource == null && _musicSourceSecondary != null)
		{
			_inactiveMusicSource = _musicSourceSecondary;
		}
	}

	private void PlayMusicImmediate(AudioSource source, AudioClip clip, bool loop)
	{
		if (source == null)
		{
			return;
		}

		source.clip = clip;
		source.loop = loop;
		source.volume = 1f;
		source.Play();
	}

	private AudioSource GetLoudestMusicSource()
	{
		if (_musicSourceSecondary == null)
		{
			return _musicSource;
		}

		if (_musicSource == null)
		{
			return _musicSourceSecondary;
		}

		bool primaryPlaying = _musicSource.isPlaying;
		bool secondaryPlaying = _musicSourceSecondary.isPlaying;

		if (primaryPlaying && !secondaryPlaying)
		{
			return _musicSource;
		}

		if (!primaryPlaying && secondaryPlaying)
		{
			return _musicSourceSecondary;
		}

		return _musicSource.volume >= _musicSourceSecondary.volume ? _musicSource : _musicSourceSecondary;
	}

	private AudioSource GetOtherMusicSource(AudioSource source)
	{
		if (source == null)
		{
			return _musicSource;
		}

		if (_musicSourceSecondary == null)
		{
			return _musicSource;
		}

		return source == _musicSource ? _musicSourceSecondary : _musicSource;
	}

	private IEnumerator CrossfadeRoutine(AudioSource fromSource, AudioSource toSource, AudioClip clip, bool loop)
	{
		if (toSource == null)
		{
			PlayMusicImmediate(fromSource, clip, loop);
			_musicFadeCoroutine = null;
			yield break;
		}

		float duration = Mathf.Max(0.01f, _musicCrossfadeDuration);
		float fromStartVolume = fromSource != null ? fromSource.volume : 0f;

		toSource.clip = clip;
		toSource.loop = loop;
		toSource.volume = 0f;
		toSource.Play();

		float time = 0f;
		while (time < duration)
		{
			float t = time / duration;
			toSource.volume = Mathf.Lerp(0f, 1f, t);
			if (fromSource != null)
			{
				fromSource.volume = Mathf.Lerp(fromStartVolume, 0f, t);
			}

			time += Time.unscaledDeltaTime;
			yield return null;
		}

		toSource.volume = 1f;
		if (fromSource != null)
		{
			fromSource.Stop();
			fromSource.volume = 1f;
		}

		_activeMusicSource = toSource;
		_inactiveMusicSource = fromSource;
		_musicFadeCoroutine = null;
	}
}
