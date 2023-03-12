using UnityEngine;

public class LocalizedGUIText : MonoBehaviour
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
		base.GetComponent<GUIText>().text = LanguageManager.Instance.GetTextValue(localizedKey);
	}
}
