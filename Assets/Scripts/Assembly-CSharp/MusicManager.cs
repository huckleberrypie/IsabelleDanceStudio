using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
	public AudioClip[] clips;

	private AudioSource _audio;

	public string _currentMusicPlaying;

	private bool isPause;

	private void Awake()
	{
		_audio = GetComponent<AudioSource>();
	}

	public void Pause()
	{
		if (_audio.isPlaying)
		{
			isPause = true;
			_audio.Pause();
		}
	}

	public void Play()
	{
		if (isPause)
		{
			isPause = false;
			_audio.Play();
		}
	}

	public void Stop()
	{
		StopCoroutine("WaitForFinished");
		_audio.Stop();
	}

	public bool IsPlaying(string name)
	{
		return (_currentMusicPlaying == name) ? true : false;
	}

	public void PlayMusicLoop(string fileName)
	{
		PlayMusicLoop(fileName, 1f);
	}

	public void PlayMusicOneShot(string fileName)
	{
		PlayMusicOneShot(fileName, 1f);
	}

	public void PlayMusicByOrder(string fileNames, bool loopAtLastSong)
	{
		PlayMusicByOrder(fileNames, loopAtLastSong, 1f);
	}

	public void PlayMusicLoop(string fileName, float volume)
	{
		Stop();
		_currentMusicPlaying = fileName;
		AudioClip audioClipByName = GetAudioClipByName(fileName);
		_audio.loop = true;
		_audio.volume = Mathf.Clamp01(volume);
		_audio.clip = audioClipByName;
		if (!AudioManager.isSoundOFF)
		{
			_audio.Play();
		}
	}

	public void PlayMusicOneShot(string fileName, float volume)
	{
		Stop();
		_currentMusicPlaying = fileName;
		AudioClip audioClipByName = GetAudioClipByName(fileName);
		_audio.loop = false;
		_audio.volume = Mathf.Clamp01(volume);
		_audio.clip = audioClipByName;
		if (!AudioManager.isSoundOFF)
		{
			_audio.Play();
		}
	}

	public void PlayMusicByOrder(string fileNames, bool loopAtLastSong, float volume)
	{
		Stop();
		string[] fileNames2 = fileNames.Split(',');
		StartCoroutine(WaitForFinished(fileNames2, 0, loopAtLastSong, volume));
	}

	private IEnumerator WaitForFinished(string[] fileNames, int index, bool loopAtLastSong, float volume)
	{
		int count2 = index;
		while (_audio.isPlaying)
		{
			yield return null;
		}
		if (count2 < fileNames.Length - 1)
		{
			if (!_audio.isPlaying)
			{
				_currentMusicPlaying = fileNames[count2];
				AudioClip clip2 = GetAudioClipByName(fileNames[count2]);
				_audio.loop = false;
				_audio.volume = Mathf.Clamp01(volume);
				_audio.clip = clip2;
				if (!AudioManager.isSoundOFF)
				{
					_audio.Play();
				}
				count2++;
				StartCoroutine(WaitForFinished(fileNames, count2, loopAtLastSong, volume));
			}
		}
		else
		{
			_currentMusicPlaying = fileNames[count2];
			AudioClip clip = GetAudioClipByName(fileNames[count2]);
			if (loopAtLastSong)
			{
				_audio.loop = true;
			}
			else
			{
				_audio.loop = false;
			}
			_audio.volume = Mathf.Clamp01(volume);
			_audio.clip = clip;
			if (!AudioManager.isSoundOFF)
			{
				_audio.Play();
			}
		}
	}

	public AudioClip GetAudioClipByName(string name)
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
		return Resources.Load("Sounds/Music/" + name) as AudioClip;
	}
}
