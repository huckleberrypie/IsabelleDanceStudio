using UnityEngine;

public class LocalizedGUITexture : MonoBehaviour
{
	public string localizedKey = "INSERT_KEY_HERE";

	private void Start()
	{
		LanguageManager instance = LanguageManager.Instance;
		instance.OnChangeLanguage += OnChangeLanguage;
		OnChangeLanguage(instance);
	}

	private void OnChangeLanguage(LanguageManager thisLanguageManager)
	{
		base.GetComponent<GUITexture>().texture = LanguageManager.Instance.GetTexture(localizedKey);
	}
}
