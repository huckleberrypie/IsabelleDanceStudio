using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class LoadAllLanguages : MonoBehaviour
{
	private Dictionary<string, string> currentLanguageValues;

	private LanguageManager thisLanguageManager;

	private Vector2 valuesScrollPosition = Vector2.zero;

	private Vector2 languagesScrollPosition = Vector2.zero;

	private void Start()
	{
		thisLanguageManager = LanguageManager.Instance;
		string systemLanguage = thisLanguageManager.GetSystemLanguage();
		if (thisLanguageManager.IsLanguageSupported(systemLanguage))
		{
			thisLanguageManager.ChangeLanguage(systemLanguage);
		}
		if (thisLanguageManager.AvailableLanguages.Count > 0)
		{
			currentLanguageValues = thisLanguageManager.GetTextDataBase();
		}
		else
		{
			Debug.LogError("No languages are created!, Open the Smart Localization plugin at Window->Smart Localization and create your language!");
		}
	}

	private void OnGUI()
	{
		if (!thisLanguageManager.IsInitialized)
		{
			return;
		}
		GUILayout.Label("Current Language:" + thisLanguageManager.language);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Keys:", GUILayout.Width(460f));
		GUILayout.Label("Values:", GUILayout.Width(460f));
		GUILayout.EndHorizontal();
		valuesScrollPosition = GUILayout.BeginScrollView(valuesScrollPosition);
		foreach (KeyValuePair<string, string> currentLanguageValue in currentLanguageValues)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(currentLanguageValue.Key, GUILayout.Width(460f));
			GUILayout.Label(currentLanguageValue.Value, GUILayout.Width(460f));
			GUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView();
		languagesScrollPosition = GUILayout.BeginScrollView(languagesScrollPosition);
		foreach (CultureInfo item in thisLanguageManager.AvailableLanguagesCultureInfo)
		{
			if (GUILayout.Button(item.NativeName, GUILayout.Width(960f)))
			{
				thisLanguageManager.ChangeLanguage(item.Name);
				currentLanguageValues = thisLanguageManager.GetTextDataBase();
			}
		}
		GUILayout.EndScrollView();
	}
}
