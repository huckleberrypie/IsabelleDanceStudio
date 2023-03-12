using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class AudioManager : MonoBehaviour
{
	public const string AUDIO_KEY = "audio_key";

	public static bool isSoundOFF;

	public static MusicManager Music;

	public static SoundManager Sound;

	private void Awake()
	{
		Sound = GetComponentInChildren<SoundManager>();
		Music = GetComponentInChildren<MusicManager>();
	}
}
