using UnityEngine;

public class LocalizedAudioSource : MonoBehaviour
{
	public string localizedKey = "INSERT_KEY_HERE";

	public AudioClip thisAudioClip;

	private AudioSource thisAudioSource;

	private void Start()
	{
		LanguageManager instance = LanguageManager.Instance;
		instance.OnChangeLanguage += OnChangeLanguage;
		thisAudioSource = base.GetComponent<AudioSource>();
		OnChangeLanguage(instance);
	}

	private void OnChangeLanguage(LanguageManager thisLanguageManager)
	{
		thisAudioClip = thisLanguageManager.GetAudioClip(localizedKey);
		if (thisAudioSource != null)
		{
			thisAudioSource.clip = thisAudioClip;
		}
	}
}
