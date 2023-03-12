using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private const int MAX_AUDIO_SOURCE = 128;

	public AudioClip[] clips;

	private List<SoundContainer> _audios;

	private void Awake()
	{
		_audios = new List<SoundContainer>();
	}

	public void Pause()
	{
		for (int i = 0; i < _audios.Count; i++)
		{
			if (_audios[i].audio.isPlaying)
			{
				_audios[i].isPause = true;
				_audios[i].audio.Pause();
			}
		}
	}

	public void Play()
	{
		for (int i = 0; i < _audios.Count; i++)
		{
			if (_audios[i].isPause)
			{
				_audios[i].isPause = false;
				_audios[i].audio.Play();
			}
		}
	}

	public void Stop()
	{
		StopCoroutine("WaitForFinished");
		for (int i = 0; i < _audios.Count; i++)
		{
			_audios[i].audio.Stop();
		}
	}

	public bool IsPlaying(string name)
	{
		bool result = false;
		if (_audios.Count > 0)
		{
			for (int i = 0; i < _audios.Count; i++)
			{
				if (_audios[i].audio.isPlaying && _audios[i].soundName == name)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public void PlaySoundLoop(string fileName)
	{
		PlaySoundLoop(fileName, 1f);
	}

	public void PlaySoundOneShot(string fileName)
	{
		PlaySoundOneShot(fileName, 1f);
	}

	public void PlaySoundByOrder(string fileNames, bool loopAtLastSong)
	{
		PlaySoundByOrder(fileNames, loopAtLastSong, 1f);
	}

	public void PlaySoundLoop(string fileName, float volume)
	{
		if (!AudioManager.isSoundOFF)
		{
			SoundContainer audioSourceByName = GetAudioSourceByName(fileName, true, Mathf.Clamp01(volume));
			audioSourceByName.audio.Play();
		}
	}

	public void PlaySoundOneShot(string fileName, float volume)
	{
		if (!AudioManager.isSoundOFF)
		{
			SoundContainer audioSourceByName = GetAudioSourceByName(fileName, false, Mathf.Clamp01(volume));
			audioSourceByName.audio.Play();
		}
	}

	public void PlaySoundByOrder(string fileNames, bool loopAtLastSong, float volume)
	{
		if (!AudioManager.isSoundOFF)
		{
			string[] fileNames2 = fileNames.Split(',');
			StartCoroutine(WaitForFinished(fileNames2, 0, loopAtLastSong, volume));
		}
	}

	private IEnumerator WaitForFinished(string[] fileNames, int index, bool loopAtLastSong, float volume)
	{
		if (AudioManager.isSoundOFF)
		{
			yield break;
		}
		int count2 = index;
		while (IsPlaying(fileNames[count2]))
		{
			yield return null;
		}
		if (count2 < fileNames.Length - 1)
		{
			if (!IsPlaying(fileNames[count2]))
			{
				SoundContainer soundContainer2 = GetAudioSourceByName(fileNames[count2], false, Mathf.Clamp01(volume));
				soundContainer2.audio.Play();
				count2++;
				StartCoroutine(WaitForFinished(fileNames, count2, loopAtLastSong, volume));
			}
		}
		else
		{
			SoundContainer soundContainer = GetAudioSourceByName(fileNames[count2], loopAtLastSong, Mathf.Clamp01(volume));
			soundContainer.audio.Play();
		}
	}

	private SoundContainer GetAudioSourceByName(string fileName, bool isLoop, float volume)
	{
		if (_audios.Count > 0)
		{
			for (int i = 0; i < _audios.Count; i++)
			{
				if (_audios[i].soundName == fileName)
				{
					if (_audios[i].audio.isPlaying)
					{
						_audios[i].audio.Stop();
					}
					_audios[i].audio.loop = isLoop;
					_audios[i].audio.volume = Mathf.Clamp01(volume);
					return _audios[i];
				}
			}
		}
		SoundContainer soundContainer = new SoundContainer();
		soundContainer.soundName = fileName;
		AudioClip audioClipByName = GetAudioClipByName(fileName);
		soundContainer.audio = base.gameObject.AddComponent<AudioSource>();
		soundContainer.audio.clip = audioClipByName;
		soundContainer.audio.loop = isLoop;
		soundContainer.audio.volume = Mathf.Clamp01(volume);
		_audios.Add(soundContainer);
		return soundContainer;
	}

	private AudioClip GetAudioClipByName(string name)
	{
		if (clips != null && clips.Length > 0)
		{
			for (int i = 0; i < clips.Length; i++)
			{
				if (clips[i].name == name)
				{
					return clips[i];
				}
			}
		}
		return Resources.Load("Sounds/SFX/" + name) as AudioClip;
	}
}
